using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PyramidTriggerZoneRenderer : MonoBehaviour
{
	[SerializeField] private ParticleSystem topDust;
	[SerializeField] private ParticleSystem bottomDust;
	[SerializeField] private SpriteRenderer zone;
	[Range(0, 1f)]
	[SerializeField] private float alpha = 0.5f;
	private Color color;
	public Color CurrentColor
	{
		get => color;
		set
		{
			value.a = alpha;
			color = value;
			zone.color = color;
			var topMain = topDust.main;
			var bottomMain = bottomDust.main;
			
			topMain.startColor = value;
			bottomMain.startColor = value;
		}
	}
	
	public bool IsEnabled => zone.enabled;
	
	public void Clear(Vector2 position, Vector2 size)
	{
		topDust.transform.position = new Vector2(position.x, position.y + size.y / 2);
		bottomDust.transform.position = new Vector2(position.x, position.y - size.y / 2);
		
		var topShape = topDust.shape;
		topShape.scale = size;
		var bottomShape = bottomDust.shape;
		bottomShape.scale = size;
		
		zone.size = size;
	}
	
	public void EnableVisibility(bool value)
	{
		zone.enabled = value;
		
		if (!value)
		{
			bottomDust.Stop();
			topDust.Stop();
		}
		else
		{
			bottomDust.Play();
			topDust.Play();
		}
		
	}
}
