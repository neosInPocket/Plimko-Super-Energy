using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
	private Color color;
	public Color CurrentColor
	{
		get => color;
		set => color = value;
	}
}
