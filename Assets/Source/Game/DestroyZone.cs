using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyZone : MonoBehaviour
{
	[Range(0, 1f)]
	[SerializeField] private float zonePosition;
	[SerializeField] private ParticleSystem glowCircleTop;
	[SerializeField] private ParticleSystem glowCircleBottom;
	[SerializeField] private ParticleSystem topParticles;
	[SerializeField] private ParticleSystem bottomParticles;
	[SerializeField] private AllColorSO allColors; 
	private Vector2 screenSize;
	public Color CurrentColor { get; set; }
	public Action<bool> ColorMatch;
	
	private void Start()
	{
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		var topScale = glowCircleTop.transform.localScale;
		topScale.x = screenSize.x;
		glowCircleTop.transform.localScale = topScale;
		
		var bottomScale = glowCircleTop.transform.localScale;
		bottomScale.x = screenSize.x;
		glowCircleBottom.transform.localScale = bottomScale;
		
		var topShape = topParticles.shape;
		var topShapeScale = topShape.scale;
		topShapeScale.x = screenSize.x;
		topShape.scale = topShapeScale;
		
		var bottomShape = bottomParticles.shape;
		var bottomShapeScale = bottomShape.scale;
		bottomShapeScale.x = screenSize.x;
		bottomShape.scale = bottomShapeScale;
		
		transform.position = new Vector2(0, screenSize.y * 2 * zonePosition - screenSize.y);
		
		SetRandomColor();
	}
	
	private void SetColor(Color color)
	{
		CurrentColor = color;
		var topDustMain = topParticles.main;
		topDustMain.startColor = color;
		
		var bottomDustMain = bottomParticles.main;
		bottomDustMain.startColor = color;
		color.a = 0.5f;
		
		var topCircleMain = glowCircleTop.main;
		topCircleMain.startColor = color;
		
		var bottomCircleMain = glowCircleBottom.main;
		bottomCircleMain.startColor = color;
		
		topParticles.Stop();
		topParticles.Play();
		
		bottomParticles.Stop();
		bottomParticles.Play();
		
		glowCircleTop.Stop();
		glowCircleTop.Play();
		
		glowCircleBottom.Stop();
		glowCircleBottom.Play();
	}
	
	private void SetRandomColor()
	{
		SetColor(allColors.Colors[Random.Range(0, allColors.Colors.Length)]);
	}
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<JugglingBall>(out JugglingBall ball))
		{
			if (ball.BallColor == CurrentColor)
			{
				ColorMatch?.Invoke(true);
			}
			else
			{
				ColorMatch?.Invoke(false);
			}
			
			SetRandomColor();
			ball.Destroy();
		}
	}
}
