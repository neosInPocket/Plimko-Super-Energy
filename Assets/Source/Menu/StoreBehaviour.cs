using TMPro;
using UnityEngine;

public class StoreBehaviour : MonoBehaviour
{
	[SerializeField] private StoreUpgradeContainer[] containers;
	[SerializeField] private TMP_Text playerCoins;
	
	private void Start()
	{
		foreach (var container in containers)
		{
			container.ContainerRefresh += OnContainerRefresh;
		}
		
		OnContainerRefresh();
	}
	
	private void OnContainerRefresh()
	{
		foreach (var container in containers)
		{
			container.Refresh();
		}
		
		playerCoins.text = SerializedData.Coins.ToString();
	}
}
