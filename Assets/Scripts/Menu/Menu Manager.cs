using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] GameObject infoPanel;

	const string TOWN_TEXT = "Town";

	public void StartPlaying()
	{
		SceneManager.LoadScene(TOWN_TEXT);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void ToggleInfo()
	{
		infoPanel.SetActive(!infoPanel.activeSelf);
	}
}
