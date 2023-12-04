using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglingBall : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private AllColorSO allColors;
	[SerializeField] private DestroyEffect destroyEffect;
	[SerializeField] private Rigidbody2D rigid2D;
	private Vector2 speed;
	private PyramidTriggerZoneController triggerZone;
	
	public Color BallColor => spriteRenderer.color;
	
	private void Start()
	{
		spriteRenderer.color = allColors.Colors[Random.Range(0, allColors.Colors.Length)];
		float gravityScale = 0;
		SerializedData.Gravity = 3;
		
		switch (SerializedData.Gravity)
		{
			case 0:
			gravityScale = 0.3f;
			break;
			
			case 1:
			gravityScale = 0.25f;
			break;
			
			case 2:
			gravityScale = 0.2f;
			break;
			
			case 3:
			gravityScale = 0.15f;
			break;
		}
		
		rigid2D.gravityScale = gravityScale;
		
	}
	
	public void Destroy()
	{
		circleCollider2D.enabled = false;
		var effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
		effect.EffectEnd += DestroyEffectEnded;
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
		if (triggerZone != null && ColorComparator.Compare(BallColor, triggerZone.CurrentColor))
		{
			rigid2D.velocity -= rigid2D.velocity / 2;
		}
	}
	
	private void DestroyEffectEnded(DestroyEffect effect)
	{
		effect.EffectEnd -= DestroyEffectEnded;
		Destroy(gameObject);
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
