using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidTriggerZoneController : MonoBehaviour
{
	[SerializeField] private PyramidTriggerZoneRenderer zoneRenderer;
	
	public void Clear(Vector2 position, Vector2 size)
	{
		zoneRenderer.Clear(position, size);
	}
	
	public void Enable(bool value)
	{
		zoneRenderer.EnableVisibility(value);
	}
	
	public void Toggle()
	{
		zoneRenderer.ToggleVisibility();
	}
}
