using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private Image colorContainer;
	[SerializeField] private TMP_Text usageText;
	
	private Color color;
	
	public Color ButtonColor
	{
		get => color;
		set
		{
			color = value;
			colorContainer.color = value;
		}
	}
	
	public void Enable(bool value)
	{
		button.interactable = value;
	}
	
	public void CheckUsageAmount(int usages)
	{
		if (usages == 0)
		{
			usageText.enabled = false;
		}
		else
		{
			usageText.enabled = true;
		}
		
		usageText.text = usages.ToString();
	}
}
