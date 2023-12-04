using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColorPanelController : MonoBehaviour
{
	[SerializeField] private AllColorSO allColor;
	[SerializeField] private ColorController colorController;
	[SerializeField] private ColorButton[] buttons;
	[SerializeField] private Image preview;
	private Color currentColor;
	public Color CurrentColor
	{
		get => currentColor;
		set => currentColor = value;
	}
	
	private void Start()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].ButtonColor = allColor.Colors[i];
			buttons[i].Enable(false);
			buttons[i].CheckUsageAmount(0);
		}
		
		Enable(true);
		SetColor(allColor.Colors[1]);
	}
	
	public void CheckUsages()
	{
		foreach (var button in buttons)
		{
			int usages = colorController.CheckUsages(button.ButtonColor);
			button.CheckUsageAmount(usages);
		}
	}
	
	public void Enable(bool value)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].Enable(value);
		}
	}
	
	private void SetColor(Color color)
	{
		CurrentColor = color;
		colorController.CurrentColor = color;
		preview.color = color;
	}
	
	public void OnButtonClick(ColorButton button)
	{
		SetColor(button.ButtonColor);
	}
}
