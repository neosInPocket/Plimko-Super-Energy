using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : Pausable
{
    [SerializeField] private Image timerFill;
	[SerializeField] private TMP_Text timerText;
	private bool isPaused;
	public Action Time;
	
	public void StartTimer(float seconds)
	{
		StartCoroutine(StartTimerRoutine(seconds));
	}
	
	public override void Disable()
	{
		StopAllCoroutines();
	}
	
	public override void Reset()
	{
		timerText.text = "0,00";
		timerFill.fillAmount = 1f;
	}
	
	public override void Pause()
	{
		isPaused = true;
	}
	
	public override void UnPause()
	{
		isPaused = false;
	}
	
	private IEnumerator StartTimerRoutine(float time)
	{
		var currentTime = time;
		
		while (currentTime > 0)
		{
			if (!isPaused)
			{
				currentTime -= UnityEngine.Time.fixedDeltaTime;
				timerFill.fillAmount = currentTime / time;
				timerText.text = currentTime.ToString("F2");
				yield return new WaitForFixedUpdate();
			}
			else
			{
				yield return null;
			}
		}
		
		timerFill.fillAmount = 0;
		timerText.text = "0,00";
		Time?.Invoke();
	}

	public override void Enable() { }
}
