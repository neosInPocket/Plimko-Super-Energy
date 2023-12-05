using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Pausable
{
	private Color color;
	private Dictionary<Color, int> currentColors;
	public Color CurrentColor
	{
		get => color;
		set => color = value;
	}
	
	private void Awake()
	{
		currentColors = new Dictionary<Color, int>();
	}
	
	public bool UseColor(Color color)
	{
		if (currentColors.ContainsKey(color))
		{
			if (currentColors[color] < 1)
			{
				currentColors[color]++;
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			currentColors.Add(color, 0);
			currentColors[color]++;
			return true;
		}
	}
	
	public void RemoveColor(Color color)
	{
		color.a = 1f;
		currentColors[color]--;
		
		if (currentColors[color] <= 0)
		{
			currentColors.Remove(color);
		}
	}
	
	public int CheckUsages(Color color)
	{
		if (currentColors.ContainsKey(color))
		{
			return currentColors[color];
		}
		else
		{
			return 0;
		}
	}

    public override void Reset()
    {
        currentColors.Clear();
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
