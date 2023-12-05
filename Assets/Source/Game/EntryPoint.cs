using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
	[SerializeField] private PauseController pauseController;
	
	private void Start()
	{
		ResetAll();
	}
	
	public void StartGame()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Enable();
		}
	}
	
	public void ResetAll()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Reset();
		}
	}
	
	public void PauseAll()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.Pause();
		}
	}
	
	public void UnPauseAll()
	{
		foreach (var pausable in pauseController.Pausables)
		{
			pausable.UnPause();
		}
	}
	
	public void LoadMenu()
	{
		SceneManager.LoadScene("PlayMenuScene");
	}
}
