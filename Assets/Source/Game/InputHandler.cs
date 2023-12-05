using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputHandler : Pausable
{
	public Action<GameObject> RaycastTouch;
	private bool paused;
	
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
		if (paused) return;
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

	public override void Reset()
	{
		paused = false;
	}

	public override void Enable()
	{
		paused = false;
	}

	public override void Disable()
	{
		paused = true;
	}

	public override void Pause()
	{
		paused = true;
	}

	public override void UnPause()
	{
		paused = false;
	}
}
