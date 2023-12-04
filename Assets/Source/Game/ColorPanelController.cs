using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPanelController : MonoBehaviour
{
	[SerializeField] private AllColorSO allColor;
	[SerializeField] private ColorController colorController;
	[SerializeField] private ColorButton[] buttons;
	
	private void Start()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].ButtonColor = allColor.Colors[i];
			buttons[i].Enable(false);
			buttons[i].SetUsageAmount(0);
		}
	}
	
	
	public void SetColor(Color color)
	{
		colorController.CurrentColor = color;
	}
}
