using UnityEngine;

public class JugglingBall : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private AllColorSO allColors;
	[SerializeField] private DestroyEffect destroyEffect;
	[SerializeField] private Rigidbody2D rigid2D;
	[SerializeField] private TrailRenderer trailRenderer;
	private Vector2 speed;
	private PyramidTriggerZoneController triggerZone;
	
	public Color BallColor => spriteRenderer.color;
	
	private void Start()
	{
		spriteRenderer.color = allColors.Colors[Random.Range(0, allColors.Colors.Length)];
		var color = spriteRenderer.color;
		trailRenderer.startColor = color;
		
		color.a = 0;
		trailRenderer.endColor = color;
		
		float gravityScale = 0;
		Debug.LogError("Remove!!!!");
		SerializedData.Gravity = 3;
		
		switch (SerializedData.Gravity)
		{
			case 0:
			gravityScale = 0.2f;
			break;
			
			case 1:
			gravityScale = 0.15f;
			break;
			
			case 2:
			gravityScale = 0.1f;
			break;
			
			case 3:
			gravityScale = 0.05f;
			break;
		}
		
		rigid2D.gravityScale = gravityScale;
		
	}
	
	public void Destroy()
	{
		rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
		circleCollider2D.enabled = false;
		spriteRenderer.enabled = false;
		var effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
		effect.EffectEnd += DestroyEffectEnded;
	}
	
	private void DestroyEffectEnded(DestroyEffect effect)
	{
		effect.EffectEnd -= DestroyEffectEnded;
		Destroy(gameObject);
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.transform.parent.TryGetComponent<PyramidTriggerZoneController>(out PyramidTriggerZoneController controller))
		{
			triggerZone = controller;
		}
	}
	
	private void OnTriggerStay2D(Collider2D collider)
	{
		if (triggerZone.IsEnabled && ColorComparator.Compare(BallColor, triggerZone.CurrentColor))
		{
			rigid2D.velocity -= rigid2D.velocity / 2;
		}
	}
	
	public void Freeze()
	{
		speed = rigid2D.velocity;
		rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
	}
	
	public void UnFreeze()
	{
		rigid2D.constraints = RigidbodyConstraints2D.None;
		rigid2D.velocity = speed;
	}
}
