using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigateButtonsController : MonoBehaviour
{
	[SerializeField] private Button[] buttons;
	[SerializeField] private Transform[] gameScreens; 
	private Button currentButton;
	private Transform currentScreen;
	
	private void Start()
	{
		currentButton = buttons[0];
		currentScreen = gameScreens[0];
	}
	
	public void OnButtonClick(int index)
	{
		if (index == 0 && currentScreen == gameScreens[0])
		{
			LoadGame();
		}
		else
		{
			currentButton = buttons[index];
			currentScreen = gameScreens[index];
		}
	}
	
	private void LoadGame()
	{
		SceneManager.LoadScene("PyramidGameScene");
	}
}
