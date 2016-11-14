using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuClick : MonoBehaviour {

	public int id;
	public string data;



	void Start () 
	{
		
	}

	public void ReturnToMenu(){
		AudioManager.Instance.Play (0);
		MainMenu.Instance.ReturnToMenu ();
	}

	public void ReturnToNewGame(){
		AudioManager.Instance.Play (0);
		MainMenu.Instance.ReturnToNewGame();
	}

	public void ContinueGame()
	{
		AudioManager.Instance.Play(1);
		SaveLoadXML.LoadGameDataXML();
		SceneManager.LoadScene(2);
	}

	public void NewGame()
	{
		AudioManager.Instance.Play(1);
		MainMenu.Instance.NewGame();
	}

	public void CharacterGeneration()
	{
		AudioManager.Instance.Play(1);
		MainMenu.Instance.CharacterGeneration(id,false);
	}

	public void ExitGame()
	{
		AudioManager.Instance.Play(0);
		Application.Quit();
	}

	public void StartGame()
	{
		AudioManager.Instance.Play(1);
		MainMenu.Instance.StartGame();
	}

	public void ArrowForward()
	{
		AudioManager.Instance.Play(0);
		MainMenu.Instance.ChangeNewGamePage(1);
		MainMenu.Instance.NewGame();
	}

	public void ArrowBack()
	{
		AudioManager.Instance.Play(0);
		MainMenu.Instance.ChangeNewGamePage(-1);
		MainMenu.Instance.NewGame();
	}

	public void MuteSound()
	{
		if(GameData.current.mute)
		{
			gameObject.GetComponent<Image>().sprite = MainMenu.Instance.sound_on;
			GameData.current.mute = false;
			AudioManager.Instance.Play(0);
		} 
		else
		{
			gameObject.GetComponent<Image>().sprite = MainMenu.Instance.sound_off;
			GameData.current.mute = true;
		} 
		SaveLoadXML.SaveGameDataXML();
	}

	public void ShowChangeLanguageWindow()
	{
		AudioManager.Instance.Play(0);
		AudioManager.Instance.Play(1);
		MainMenu.Instance.ShowLanguageWindow();
	}

	public void SwitchLanguage()
	{
		AudioManager.Instance.Play(0);
		GameData.current.lang = data;
		SaveLoadXML.SaveGameDataXML();
		MainMenu.Instance.ReloadLanguage();
	}

	public void ShowSettingsPanel(){
		MainMenu.Instance.ShowSettingsPanel ();
	}

//	public void ConfirmStartGame()
//	{
//		MainMenu.Instance.ConfirmStartGame (GetComponent<ActionClick>().id, GetComponent<ActionClick>().item_name);
//	}

	public void RestartGame()
	{
		MainMenu.Instance.RestartGame ();
	}
}
