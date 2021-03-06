﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour 
{

	private static MainMenu _instance;
	public static MainMenu Instance { get { return _instance ?? (_instance = FindObjectOfType<MainMenu>()); } }
	protected MainMenu() { _instance = null; }

	public List<CharacterData> charactersList = new List<CharacterData>();

	[SerializeField]
	Text new_game_button_text;

	[SerializeField]
	GameObject StartGamePanel;

	[SerializeField]
	NewGamePanel newGamePanel;

	[SerializeField]
	CharSelectPanelController CharacterSelectionPanel;

	//const string newGamePanelOpened = "NEW_GAME_PANEL_OPENED";

	void Start()
	{

		new_game_button_text.text = Localization.Instance.GetLocale(75);

		newGamePanel.gameObject.SetActive(false);
		CharacterSelectionPanel.gameObject.SetActive(false);

		string gameData = Path.Combine(DataStorage.Instance.GetPath(), "gameData.gd");
		string convicted_save = Path.Combine(DataStorage.Instance.GetPath(), "convicted_save.gd");
		if(File.Exists(gameData) || File.Exists(convicted_save)) 
		{
			if(File.Exists(gameData)) 
				File.Delete(gameData);
			if(File.Exists(convicted_save)) 
				File.Delete(convicted_save);
			ScreenManager.Instance.CreateScreen("FirstStartPanel");
		}

		if(!GameData.current.isFirstOpened)
			NewGame();
	}

	public void NewGame()
	{
		GameData.current.isFirstOpened = false;
		StartGamePanel.SetActive(false);
		newGamePanel.gameObject.SetActive(true);
		newGamePanel.NewGame();

//		if(!GameData.current.HasCharacterCompleted(0) && !GameData.current.isTutorialCompleted)
//			ScreenManager.Instance.CreateScreen("TutorialMainMenu");
	}
		
	public void CharacterGeneration(int id,bool reset_character)
	{
		newGamePanel.gameObject.SetActive(false);
		CharacterSelectionPanel.gameObject.SetActive(true);

		CharacterSelectionPanel.ShowCharacterData(id, reset_character); 
	}

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape) && !PlayerInfo.Instance.isTutorial)
		{
			AudioManager.Instance.Play(1);
			if(ScreenManager.Instance.current != null)
				Destroy(ScreenManager.Instance.current);
			else
			{
				if(StartGamePanel.activeSelf)
				{
					ScreenManager.Instance.CreateScreen("HintPanel");
					ScreenManager.Instance.current.GetComponent<Hint>().ShowDialog(Localization.Instance.GetLocale(65), Localization.Instance.GetLocale(76), (confirm)=>
							{
								if(confirm)
									Application.Quit();
							});
				}
				else
					ReturnToMenu();
			}
		}
	}
		
	public void ReloadLanguage()
	{
		new_game_button_text.text = Localization.Instance.GetLocale(75);
	}

	public void ReturnToMenu()
	{
		newGamePanel.gameObject.SetActive (false);
		CharacterSelectionPanel.gameObject.SetActive (false);
		StartGamePanel.SetActive (true);
	}
}
