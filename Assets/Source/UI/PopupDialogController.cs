using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupDialogController : MonoBehaviour
{
	[SerializeField] private Button confirmButton;
	[SerializeField] private Button rejectButton;
	[SerializeField] private TMP_Text dialogText;
	[SerializeField] private Animator animator;
	[SerializeField] private PauseController pauseController;
	[SerializeField] private EntryPoint entryPoint;
	[SerializeField] private UIHandler uIHandler;
	[SerializeField] private CountingScreen countingScreen;
	
	public void PopupReplay()
	{
		dialogText.text = "DO YOU WANT TO TAKE THIS LEVEL AGAIN?";
		confirmButton.onClick.RemoveAllListeners();
		confirmButton.onClick.AddListener(uIHandler.SetGame);
		confirmButton.onClick.AddListener(Hide);
		Popup();
	}
		
	public void PopupMenu()
	{
		dialogText.text = "DO YOU WANT TO EXIT?";
		confirmButton.onClick.RemoveAllListeners();
		confirmButton.onClick.AddListener(entryPoint.LoadMenu);
		confirmButton.onClick.AddListener(Hide);
		Popup();
	}
	
	private void Popup()
	{
		gameObject.SetActive(true);
		pauseController.Pause();
	}
	
	private void Hide()
	{
		countingScreen.StartCounting(entryPoint.UnPauseAll);
		gameObject.SetActive(false);
	}
}
