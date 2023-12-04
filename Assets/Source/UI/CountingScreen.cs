using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingScreen : MonoBehaviour
{
	public Action Completed;
	private Action completedAction;
	
	public void StartCounting(Action onCompletedAction)
	{
		gameObject.SetActive(true);
		completedAction = onCompletedAction;
	}
	
	public void OnComplete()
	{
		completedAction();
		gameObject.SetActive(false);
	}
}
