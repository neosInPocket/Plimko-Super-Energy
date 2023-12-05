
using TMPro;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
	[SerializeField] private TMP_Text result;
	[SerializeField] private UIHandler uIHandler; 
	[SerializeField] private EntryPoint entryPoint; 
	
	public void StartLoseScreen(bool isWin)
	{
		gameObject.SetActive(true);
		
		if (isWin)
		{
			result.text = "YOU WIN!";
		}
		else
		{
			result.text = "YOU LOSE!";
		}
	}
	
	public void OnHide()
	{
		uIHandler.SetGame();
		gameObject.SetActive(false);
	}
	
	public void OnHideNExit()
	{
		entryPoint.LoadMenu();
	}
}
