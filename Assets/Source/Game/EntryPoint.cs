using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
	[SerializeField] private PauseController pauseController;
	
	private void Start()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Reset();
		}
	}
	
	public void StartGame()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Enable();
		}
	}
}
