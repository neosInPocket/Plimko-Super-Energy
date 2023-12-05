using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PyramidSpawner : Pausable
{
	[SerializeField] private Vector2 yBorders;
	[SerializeField] private Vector2 xBorders;
	[SerializeField] private int rowsAmount;
	[SerializeField] private PyramidBall pyramidBall;
	[SerializeField] private PyramidTriggerZoneController triggerZone;
	private List<PyramidBall> pyramidBalls; 
	private List<PyramidTriggerZoneController> triggerZones;
	
	private Vector2 screenSize;
	
	private void Start()
	{
		pyramidBalls = new List<PyramidBall>();
		triggerZones = new List<PyramidTriggerZoneController>();
		
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		CreatePyramid();
	}
	
	private void CreatePyramid()
	{
		Vector2 verticalSize = new Vector2(2 * screenSize.y * yBorders.x - screenSize.y, 2 * screenSize.y * yBorders.y - screenSize.y);
		Vector2 horizontalSize = new Vector2(2 * screenSize.x * xBorders.x - screenSize.x, 2 * screenSize.x * xBorders.y - screenSize.x);
		
		float verticalStep = (verticalSize.y - verticalSize.x) / (rowsAmount - 1);
		float horizontalStep = (horizontalSize.y - horizontalSize.x) / (rowsAmount - 1);
		
		Vector2 ballPos = Vector2.zero;
		ballPos.x = horizontalSize.x;
		
		for (int i = 0; i < rowsAmount; i++)
		{
			ballPos.y = verticalSize.x + verticalStep * i;
			
			for (int j = rowsAmount - i; j > 0; j--)
			{
				pyramidBalls.Add(Instantiate(pyramidBall, ballPos, Quaternion.identity, transform));
				ballPos.x += horizontalStep;
			}
			
			ballPos.x = horizontalSize.x + (i + 1) * horizontalStep / 2;
			
			if (i == rowsAmount - 1)
			{
				continue;
			}
			
			Vector2 triggerPosition = new Vector2(0,  ballPos.y + verticalStep / 2);
			var zone = Instantiate(triggerZone, triggerPosition, Quaternion.identity, transform);
			zone.Clear(triggerPosition, new Vector2(2 * screenSize.x, verticalStep));
			triggerZones.Add(zone);
			zone.Enable(false);
		}
	}

    public override void Reset()
    {
        foreach (var zone in triggerZones)
		{
			zone.Enable(false);
		}
    }

    public override void Enable()
    {
        
    }

    public override void Disable()
    {
        
    }

    public override void Pause()
    {
        
    }

    public override void UnPause()
    {
        
    }
}
