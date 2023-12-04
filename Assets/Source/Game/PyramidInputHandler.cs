using UnityEngine;

public class PyramidInputHandler : MonoBehaviour
{
	[SerializeField] private InputHandler inputHandler;
	//[SerializeField] private
	
	private void Awake()
	{
		inputHandler.RaycastTouch += OnRaycast;
	}
	
	private void OnRaycast(GameObject obj)
	{
		if (obj.transform.parent.TryGetComponent<PyramidTriggerZoneController>(out PyramidTriggerZoneController controller))
		{
			controller.Toggle();
		}
		
	}
	
	private void OnDestroy()
	{
		inputHandler.RaycastTouch -= OnRaycast;
	}
}
