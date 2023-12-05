using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
	[SerializeField] private GameProgressContainer gameProgress;
	[SerializeField] private EntryPoint entryPoint;
	[SerializeField] private TutorialController tutorialController;
	[SerializeField] private CountingScreen countingScreen;
	[SerializeField] private DestroyZone destroyZone;
	[SerializeField] private TimeCounter timeCounter;
	[SerializeField] private LoseScreen loseScreen;
	[SerializeField] private int pointsToAdd;
	[SerializeField] private int pointsToRemove;
	
	private int currentPoints;
	private int maxPoints => (int)(-30f * Mathf.Exp(-SerializedData.CurrentLevelsScore * SerializedData.CurrentLevelsScore / 100f) + 70f);
	private int maxReward => (int)(-30f * Mathf.Exp(-SerializedData.CurrentLevelsScore * SerializedData.CurrentLevelsScore / 100f) + 70f);
	private int maxTime => (int)(30f * Mathf.Exp(-SerializedData.CurrentLevelsScore * SerializedData.CurrentLevelsScore * SerializedData.CurrentLevelsScore * SerializedData.CurrentLevelsScore / 1000f) + 20f) + SerializedData.ExtraTime * 10;
	
	private void Start()
	{
		SetGame();
		destroyZone.ColorMatch += OnColorMatch;
	}
	
	private void OnColorMatch(bool isMatch)
	{
		if (isMatch)
		{
			currentPoints += pointsToAdd;
		}
		else
		{
			currentPoints -= pointsToRemove;
			if (currentPoints <= 0)
			{
				currentPoints = 0;
			}
		}
		
		if (currentPoints >= maxPoints)
		{
			currentPoints = maxPoints;
			timeCounter.Time -= OnTimerExpired;
			loseScreen.StartLoseScreen(true);
			SerializedData.CurrentLevelsScore++;
			SerializedData.Coins += maxReward;
			entryPoint.PauseAll();
		}
		
		gameProgress.RefreshSlider((float)currentPoints / (float)maxPoints);
	}
	
	private void OnTimerExpired()
	{
		timeCounter.Time -= OnTimerExpired;
		loseScreen.StartLoseScreen(false);
		entryPoint.PauseAll();
	}
	
	public void SetGame()
	{
		entryPoint.ResetAll();
		currentPoints = 0;
		gameProgress.SetDefaults();
		destroyZone.SetRandomColor();
		
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
		timeCounter.Time += OnTimerExpired;
		timeCounter.StartTimer(maxTime);
	}
}
