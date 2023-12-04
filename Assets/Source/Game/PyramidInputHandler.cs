using UnityEngine;

public class PyramidInputHandler : MonoBehaviour
{
	[SerializeField] private InputHandler inputHandler;
	[SerializeField] private ColorController colorController;
	[SerializeField] private ColorPanelController panelController;
	
	private void Awake()
	{
		inputHandler.RaycastTouch += OnRaycast;
	}
	
	private void OnRaycast(GameObject obj)
	{
		if (obj.transform.parent.TryGetComponent<PyramidTriggerZoneController>(out PyramidTriggerZoneController controller))
		{	
			if (controller.IsEnabled && ColorComparator.Compare(controller.CurrentColor, panelController.CurrentColor))
			{
				colorController.RemoveColor(controller.CurrentColor);
				controller.Enable(false);
				
				panelController.CheckUsages();
				return;
			}
			
			if(colorController.UseColor(panelController.CurrentColor))
			{
				if (controller.IsEnabled)
				{
					colorController.RemoveColor(controller.CurrentColor);
				}
				
				controller.CurrentColor = panelController.CurrentColor;
				controller.Enable(true);
				
				panelController.CheckUsages();
				return;
			}
		}
		
	}
	
	private void OnDestroy()
	{
		inputHandler.RaycastTouch -= OnRaycast;
	}
}
