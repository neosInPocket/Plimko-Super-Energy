using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSliderEffectsValue : MonoBehaviour
{
	[SerializeField] private Slider slider;
	
	private void Start()
	{
		slider.value = SerializedData.Effects;
	}
}
