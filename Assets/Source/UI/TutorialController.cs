using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TutorialController : MonoBehaviour
{
	[SerializeField] private TMP_Text currentTMPText;
	[SerializeField] private Animator animator;
	[SerializeField] private string[] texts;
	private int currentCounter;
	private Action completedAction;
	
	public void StartTutorial(Action onCompletedAction)
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		
		gameObject.SetActive(true);
		completedAction = onCompletedAction;
		currentCounter = 1;
		currentTMPText.text = texts[0];
		Touch.onFingerDown += ChangeReplica;
	}
	
	private void ChangeReplica(Finger finger)
	{
		if (currentCounter == texts.Length)
		{
			animator.SetTrigger("hide");
			Touch.onFingerDown -= ChangeReplica;
			return;
		}
		
		currentTMPText.text = texts[currentCounter];
		currentCounter++;
	}
	
	public void OnComplete()
	{
		completedAction();
		gameObject.SetActive(false);
	}
}
