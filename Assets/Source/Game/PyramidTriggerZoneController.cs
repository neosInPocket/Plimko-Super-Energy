using UnityEngine;

public class PyramidTriggerZoneController : MonoBehaviour
{
	[SerializeField] private PyramidTriggerZoneRenderer zoneRenderer;
	public bool IsEnabled => zoneRenderer.IsEnabled;
	
	public Color CurrentColor
	{
		get => zoneRenderer.CurrentColor;
		set => zoneRenderer.CurrentColor = value;
	}
	
	public void Clear(Vector2 position, Vector2 size)
	{
		zoneRenderer.Clear(position, size);
	}
	
	public void Enable(bool value)
	{
		zoneRenderer.EnableVisibility(value);
	}
}
