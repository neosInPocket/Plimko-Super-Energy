using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressContainer : MonoBehaviour
{
	[SerializeField] private Image sliderFill;
	[SerializeField] private TMP_Text levelAmount;
	
	public void RefreshSlider(float value)
	{
		sliderFill.fillAmount = value;
	}
	
	public void SetLevelText()
	{
		levelAmount.text = SerializedData.CurrentLevelsScore.ToString();
	}

	public void SetDefaults()
	{
		RefreshSlider(0);
		SetLevelText();
	}
}
