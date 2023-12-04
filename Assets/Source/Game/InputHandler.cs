using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputHandler : MonoBehaviour
{
	public Action<GameObject> RaycastTouch;
	
	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		
		Enable(true);
	}
	
	public void Enable(bool enabled)
	{
		if (enabled)
		{
			Touch.onFingerDown += OnFingerDown;
		}
		else
		{
			Touch.onFingerDown -= OnFingerDown;
		}
	}
	
	public void OnFingerDown(Finger finger)
	{
		var fingerPosition = Camera.main.ScreenToWorldPoint(finger.screenPosition);
		RaycastHit2D raycast = Physics2D.Raycast(fingerPosition, Vector3.forward);
		
		if (raycast.collider != null)
		{
			RaycastTouch?.Invoke(raycast.collider.gameObject);
		}
	}
	
	private void OnDestroy()
	{
		Enable(false);
	}
}
