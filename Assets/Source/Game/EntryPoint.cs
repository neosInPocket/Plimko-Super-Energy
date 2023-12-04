using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
	[SerializeField] private PauseController pauseController;
	
	private void Start()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Reset();
		}
	}
	
	public void StartGame()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Enable();
		}
	}
	
	public void PauseAll()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Pause();
		}
	}
	
	public void LoadMenu()
	{
		SceneManager.LoadScene("PlayMenuScene");
	}
}
