using UnityEngine;

public static class ColorComparator
{
	public static bool Compare(Color color1, Color color2)
	{
		color1.a = 1f;
		color2.a = 1f;
		return color1 == color2;
	}
}
