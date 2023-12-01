using UnityEngine;

public class PyramidSpawner : MonoBehaviour
{
	[SerializeField] private Vector2 yBorders;
	[SerializeField] private Vector2 xBorders;
	[SerializeField] private int rowsAmount;
	[SerializeField] private GameObject pyramidBall;
	
	private Vector2 screenSize;
	
	private void Start()
	{
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		CreatePyramid();
	}
	
	private void CreatePyramid()
	{
		Vector2 verticalSize = new Vector2(2 * screenSize.y * yBorders.x - screenSize.y, 2 * screenSize.y * yBorders.y - screenSize.y);
		Vector2 horizontalSize = new Vector2(2 * screenSize.x * xBorders.x - screenSize.x, 2 * screenSize.x * xBorders.y - screenSize.x);
		
		float verticalStep = (verticalSize.y - verticalSize.x) / (rowsAmount - 1);
		float horizontalStep = (horizontalSize.y - horizontalSize.x) / (rowsAmount - 1);
		
		Vector2 ballPos = Vector2.zero;
		ballPos.x = horizontalSize.x;
		
		for (int i = 0; i < rowsAmount; i++)
		{
			ballPos.y = verticalSize.x + verticalStep * i;
			
			for (int j = rowsAmount - i; j > 0; j--)
			{
				Instantiate(pyramidBall, ballPos, Quaternion.identity, transform);
				ballPos.x += horizontalStep;
			}
			
			ballPos.x = horizontalSize.x + (i + 1) * horizontalStep / 2;
		}
	}
}
