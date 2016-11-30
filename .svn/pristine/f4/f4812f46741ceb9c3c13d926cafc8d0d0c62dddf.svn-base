using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour
{
	[SerializeField]
	Button new_game_button;

	[SerializeField]
	Button settings_button;

	void Start () 
	{
	
	}

	public void NewGame()
	{
		AudioManager.Instance.Play(1);
		MainMenu.Instance.NewGame();
	}

	public void ShowSettingsPanel()
	{
		ScreenManager.Instance.CreateScreen("SettingsPanel");
	}
}
