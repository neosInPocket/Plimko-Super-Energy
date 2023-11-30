using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonClick : MonoBehaviour
{
	[SerializeField] private Image icon;
	[SerializeField] private Button button;
	[SerializeField] private float rotationValue;
	[SerializeField] private int rotationAmount;
	[SerializeField] private AnimationCurve magnitudeEvolution;
	[SerializeField] private AnimationCurve scaleEvolution;
	
	private void Start()
	{
		var image = GetComponent<Image>();
		
		image.alphaHitTestMinimumThreshold = 1f;
	}
	
	public void Rotate()
	{
		button.interactable = false;
		StartCoroutine(StartRotation());
	}
	
	private IEnumerator StartRotation()
	{
		float currentRotation = 0;
		float magnitude = 0;
		float deltaRotation = 0;
		float scale = 0;
		Vector3 currentScale = transform.localScale;
		
		while (currentRotation < 360 * rotationAmount)
		{
			magnitude = magnitudeEvolution.Evaluate(currentRotation / (360 * rotationAmount));
			scale = scaleEvolution.Evaluate(currentRotation / (360 * rotationAmount));
			deltaRotation = rotationValue * Time.deltaTime * magnitude;
			currentRotation += deltaRotation;
			
			transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + deltaRotation);
			icon.transform.rotation = Quaternion.Euler(0, 0, icon.transform.rotation.eulerAngles.z - deltaRotation);
			var color = icon.color;
			color.a = scale;
			icon.color = color;
			
			currentScale.x = scale;
			currentScale.y = scale;
			transform.localScale = currentScale;
			
			yield return new WaitForEndOfFrame();
		}
		
		transform.rotation = Quaternion.identity;
		button.interactable = true;
	}
}

