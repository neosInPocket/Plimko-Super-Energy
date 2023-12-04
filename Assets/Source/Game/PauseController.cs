using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	[SerializeField] private Pausable[] pausables;
	public Pausable[] Pausables => pausables;
	
	public void Pause()
	{
		foreach (var pause in pausables)
		{
			pause.Pause();
		}
	}
	
}
