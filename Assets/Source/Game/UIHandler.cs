using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
	[SerializeField] private GameProgressContainer gameProgress;
	[SerializeField] private EntryPoint entryPoint;
	[SerializeField] private TutorialController tutorialController;
	[SerializeField] private CountingScreen countingScreen;
	
	private void Start()
	{
		SetGame();
	}
	
	public void SetGame()
	{
		gameProgress.SetDefaults();
		
		if (SerializedData.JustRun == 1)
		{
			SerializedData.JustRun = 0;
			tutorialController.StartTutorial(OnTutorialCompleted);
		}
		else
		{
			countingScreen.StartCounting(OnCountComplete);
		}
	}
	
	private void OnTutorialCompleted()
	{
		countingScreen.StartCounting(OnCountComplete);
	}
	
	private void OnCountComplete()
	{
		entryPoint.StartGame();
	}
}
