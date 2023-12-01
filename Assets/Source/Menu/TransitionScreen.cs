using System.Collections;
using Coffee.UIEffects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
	[SerializeField] private TMP_Text[] fadeTexts;
	[SerializeField] private CustomUIDissolve[] dissolvePanels;
	[SerializeField] private bool activeOnStart; 
	[SerializeField] private float duration;
	public float Duration => duration;
	
	private void Start()
	{
		foreach (var panel in dissolvePanels)
		{
			panel.effectPlayer.duration = duration;
			
			if (activeOnStart)
			{
				panel.effectFactor = 0;
				panel.GetComponent<Image>().raycastTarget = true;
			}
			else
			{
				panel.effectFactor = 1;
				panel.GetComponent<Image>().raycastTarget = false;
			}
			
		}
		
		foreach (var text in fadeTexts)
		{
			var color = text.color;
			text.raycastTarget = false;
			
			if (activeOnStart)
			{
				color.a = 1;
			}
			else
			{
				color.a = 0;
			}
			
			text.color = color;
		}
	}
	
	public void ToggleRaycastTarget(bool enabled)
	{
		foreach (var panel in dissolvePanels)
		{
			panel.GetComponent<Image>().raycastTarget = enabled;
		}
	}
	
	public void Transition(bool reverse)
	{
		foreach (var panel in dissolvePanels)
		{
			panel.Reverse = reverse;
			panel.Play();
			StartCoroutine(TextFade(reverse));
		}
	}
	
	private IEnumerator TextFade(bool reverse)
	{
		if (reverse)
		{
			while (dissolvePanels[0].effectFactor > 0)
			{
				foreach (var text in fadeTexts)
				{
					var color = text.color;
					color.a = 1 - dissolvePanels[0].effectFactor;
					text.color = color;
				}
				
				yield return new WaitForEndOfFrame();
			}
		}
		else
		{
			while (dissolvePanels[0].effectFactor < 1)
			{
				foreach (var text in fadeTexts)
				{
					var color = text.color;
					color.a = 1 - dissolvePanels[0].effectFactor;
					text.color = color;
				}
				
				yield return new WaitForEndOfFrame();
			}
		}
		
	}
}
