using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreUpgradeContainer : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private Image fill;
	[SerializeField] private int cost;
	[SerializeField] private float[] fillValues;
	[SerializeField] private UpgradeType upgradeType;
	public Action ContainerRefresh;
		
	private void Start()
	{
		Refresh();
	}	
	
	public void Refresh()
	{
		int currentUpgradeAmount = upgradeType == UpgradeType.BallBreakSpeed ? SerializedData.ExtraTime : SerializedData.Gravity;
		
		if (SerializedData.Coins - cost >= 0 && currentUpgradeAmount < 4)
		{
			button.interactable = true;
		}
		else
		{
			button.interactable = false;
		}
		
		fill.fillAmount = fillValues[currentUpgradeAmount];
	}
	
	public void Buy()
	{
		SerializedData.Coins -= 50;
		
		if (upgradeType == UpgradeType.BallBreakSpeed)
		{
			SerializedData.ExtraTime++;
		}
		else
		{
			SerializedData.Gravity++;
		}
		
		ContainerRefresh?.Invoke();
	}	
}

public enum UpgradeType
{
	Gravity,
	BallBreakSpeed
}
