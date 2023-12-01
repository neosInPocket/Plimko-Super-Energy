using System.Collections;
using UnityEngine;

public class TransitionScreenController : MonoBehaviour
{
	[SerializeField] private TransitionScreen[] transitionScreens;
	private int currentScreenIndex;
	private TransitionScreen currentScreen => transitionScreens[currentScreenIndex];
	
	private void Start()
	{
		currentScreenIndex = 0;
	}
	
	public void Transition(int index)
	{
		if (index == currentScreenIndex) return;
		
		StartCoroutine(WaitForPanelDissolve(index));
	}
	
	private IEnumerator WaitForPanelDissolve(int index)
	{
		var dissolveTime = currentScreen.Duration;
		currentScreen.Transition(false);
		currentScreen.ToggleRaycastTarget(false);
		yield return new WaitForSeconds(dissolveTime);
		currentScreenIndex = index;
		currentScreen.Transition(true); 
		currentScreen.ToggleRaycastTarget(true);
	}
}
