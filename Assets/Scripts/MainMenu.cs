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

	//public TextAsset traitsFile;
//	public TextAsset personalGoalFile;
//	public TextAsset sentenceFile;
	//public TextAsset characterFile;

	public List<CharacterData> charactersList = new List<CharacterData>();

	[SerializeField]
	Text new_game_button_text;
	[SerializeField]
	GameObject StartGamePanel;

	//public List<float> tierCost = new List<float>();
	//public List<int> tierDescLocale = new List<int>();
	//public List<Sprite> CharacterImageList = new List<Sprite>();
	public Sprite sound_on;
	public Sprite sound_off;
	public Sprite char_coming_soon;

	//GameObject restart_game_button;

	GameObject NewGamePanel;
	GameObject NewGameMain;
	int page = 0;
	//Text CharacterPointsText;
	List<GameObject> tierSelectionPanelList = new List<GameObject>();
	List<Text> tierSelectionTextList = new List<Text>();
	List<Text> tierSelectionCharPointReqList = new List<Text>();
	List<Image> tierSelectionImageList = new List<Image>();
	//List<Text> tierSelectionCharNumberList = new List<Text>();
	List<GameObject> tierSelectionButtonList = new List<GameObject>();
	List<GameObject> tierSelectionZoomList = new List<GameObject> ();
	List<GameObject> tierSelectionLockList = new List<GameObject> ();
	GameObject ArrowF;
	GameObject ArrowB;

	[SerializeField]
	CharSelectPanelController CharacterSelectionPanel;

	GameObject SettingsPanel;
	Text SettingsText;
	Text SoundSettingsText;
	Text ExitButtonText;
	Text ChangeLanguageButtonText;

	GameObject LanguagePanel;

//	float save_version = 0.01f;

//	void Awake()
//	{
//		if(!SaveLoadXML.LoadGameDataXML())
//		{
//			GameData.current = new GameData();
//			string lang = Application.systemLanguage.ToString();
//			switch(lang)
//			{
//			case "Russian":
//				GameData.current.lang = "ru";
//				break;
//			default:
//				GameData.current.lang = "en";
//				break;
//			}
//			SaveLoadXML.SaveGameDataXML();
//		}
//		//start();
//	}

	void Start()
	{
		//StartGamePanel = GameObject.Find("StartGamePanel");
		//restart_game_button = GameObject.Find("restart_game_button");


		if(GameData.current.mute) 
			GameObject.Find("mute_sound_button").GetComponent<Image>().sprite = sound_off;
		else 
			GameObject.Find("mute_sound_button").GetComponent<Image>().sprite = sound_on;

		new_game_button_text.text = Localization.Instance.GetLocale(75);
		ExitButtonText = GameObject.Find ("exit_button_text").GetComponent<Text> ();
		ChangeLanguageButtonText = GameObject.Find ("change_language_button_text").GetComponent<Text> ();

		GameObject.Find("switch_ru_text").GetComponent<Text>().text = Localization.Instance.GetLocale(929);
		GameObject.Find("switch_en_text").GetComponent<Text>().text = Localization.Instance.GetLocale(928);

		NewGamePanel = GameObject.Find("NewGamePanel");
		NewGameMain = GameObject.Find("NewGameMain");
		LanguagePanel = GameObject.Find("LanguagePanel");
		//CharacterPointsText = GameObject.Find("CharacterPointsText").GetComponent<Text>();

		SettingsPanel = GameObject.Find ("SettingsPanel");
		SettingsText = GameObject.Find ("SettingsText").GetComponent<Text> ();
		SoundSettingsText = GameObject.Find ("SoundSettingsText").GetComponent<Text> ();

		SettingsPanel.SetActive (false);

		foreach(Transform _object in NewGameMain.transform)
		{
			if(_object.name == "CharacterTier")
			{
				tierSelectionPanelList.Add(_object.gameObject);
				foreach(Transform __object in _object.transform)
				{
					switch(__object.name)
					{
					case "DescriptionDisplay":
						tierSelectionTextList.Add(__object.GetComponent<Text>());
						break;
					case "CharPointReqText":
						tierSelectionCharPointReqList.Add(__object.GetComponent<Text>());
						break;
					case "CharacterImage":
						tierSelectionImageList.Add(__object.GetComponent<Image>());
						break;
//					case "CharacterNumber":
//						tierSelectionCharNumberList.Add(__object.GetComponent<Text>());
//						break;
					case "SelectTierButton":
						tierSelectionButtonList.Add(__object.gameObject);
						break;
					case "CharacterZoomIcon":
						tierSelectionZoomList.Add (__object.gameObject);
						break;
					case "CharacterLockIcon":
						tierSelectionLockList.Add (__object.gameObject);
						break;
					}
				}
			}
			else if(_object.name == "BackArrow") ArrowB = _object.gameObject;
			else if(_object.name == "ForwardArrow") ArrowF = _object.gameObject;

		}

		NewGamePanel.SetActive(false);
		CharacterSelectionPanel.gameObject.SetActive(false);
		LanguagePanel.SetActive(false);

		//Fixing screen resolution issues
		ApplyResolutionFix();

		//CharacterGenerator.LoadXmlData ();

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

	}

	public void NewGame()
	{
		StartGamePanel.SetActive(false);
		NewGamePanel.SetActive(true);

		//CharacterPointsText.text = GameData.current.characterPoints.ToString();

		for(int i = 0;i < 3;i++)
		{
			if (charactersList [i + (page * 3)].image_id >= 0) 
			{
				tierSelectionImageList [i].sprite = SpriteManager.Instatce.GetSprite("Pers/pers_" + (i + (page * 3)));//CharacterImageList [charactersList [i + (page * 3)].image_id];
				//tierSelectionCharNumberList [i].gameObject.SetActive (true);
			} 
			else 
			{
				tierSelectionImageList [i].sprite = char_coming_soon;
				//tierSelectionCharNumberList[i].text = tierSelectionButtonList[i].GetComponent<MenuClick>().data;
				//tierSelectionCharNumberList [i].gameObject.SetActive (false);
			}

			tierSelectionButtonList[i].GetComponent<MenuClick>().id = i + (page*3);
			//tierSelectionButtonList[i].GetComponent<MenuClick>().data = CharacterGenerator.GenerateCharacterNumber();
			tierSelectionZoomList[i].GetComponent<MenuClick>().id = i + (page*3);
			//tierSelectionZoomList[i].GetComponent<MenuClick>().data = CharacterGenerator.GenerateCharacterNumber();
			//tierSelectionCharNumberList[i].text = tierSelectionButtonList[i].GetComponent<MenuClick>().data;
			tierSelectionTextList[i].text = Localization.Instance.GetLocale(charactersList[i + (page*3)].tier_desc_id);

			//if ((i + page*3) == 0 || charComplited.Exists(j => (j - 1) == (i + page*3)))//(charactersList [i + (page * 3)].cost >= 0) 
			if ((i + page*3) == 0 || GameData.current.HasCharacterCompleted((i + page*3)-1))//(charactersList [i + (page * 3)].cost >= 0) 
			{
				tierSelectionButtonList [i].SetActive (true);
				tierSelectionZoomList [i].SetActive (false);
				tierSelectionLockList [i].SetActive (false);
				foreach (Transform text in tierSelectionButtonList[i].transform) 
				{
					if (text.name == "SelectButtonText")
					{
						//print("---------" + SaveLoadXML.HasKey("PLAYER_INFO"));
						text.GetComponent<Text> ().text = (SaveLoadXML.HasKey("PLAYER_INFO")) ? Localization.Instance.GetLocale(32) : Localization.Instance.GetLocale(645); //822
					}
				}
			} 
			else if (charactersList [i + (page * 3)].image_id >= 0) 
			{
				tierSelectionButtonList [i].SetActive (false);
				tierSelectionZoomList [i].SetActive (true);
				tierSelectionLockList [i].SetActive (false);
			} 
			else 
			{
				tierSelectionButtonList [i].SetActive (false);
				tierSelectionZoomList [i].SetActive (false);
				tierSelectionLockList [i].SetActive (true);
			}
		}

		if(page == 0) 
			ArrowB.SetActive(false);
		else 
			ArrowB.SetActive(true);

//		if(page >= Mathf.FloorToInt(tierCost.Count/3))
//		{
//			page = Mathf.FloorToInt(tierCost.Count/3);
//			ArrowF.SetActive(false);
//		}
//		else ArrowF.SetActive(true);

		if(page >= 1)
		{
			page = 1;
			ArrowF.SetActive(false);
		}
		else 
			ArrowF.SetActive(true);


		if(!GameData.current.HasCharacterCompleted(0) && !GameData.current.isTutorialCompleted)
			ScreenManager.Instance.CreateScreen("TutorialMainMenu");
	}

	public void ChangeNewGamePage(int value)
	{
		page += value;
		if(page < 0) page = 0;
	}

	public void CharacterGeneration(int id,bool reset_character)
	{
		NewGamePanel.SetActive(false);
		CharacterSelectionPanel.gameObject.SetActive(true);

		CharacterSelectionPanel.ShowCharacterData (id, reset_character); 
			
	}

	public void StartGame()
	{
		//print(PlayerInfo.Instance.charImageID + " " + CharacterSelectionPanel.overwritingAlert);
		if (CharacterSelectionPanel.overwritingAlert || (GameData.current.HasCharacterCompleted(PlayerInfo.Instance.charImageID) && SaveLoadXML.HasKey("PLAYER_INFO")))
		{
			CharacterSelectionPanel.ShowAlert(0);
		} 
		else if(PlayerInfo.Instance.charImageID == 1)
		{
			ScreenManager.Instance.CreateScreen("HintPanel");
			ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(Localization.Instance.GetLocale(768));
		}
		else 
		{
			SceneManager.LoadScene(2);
		}
	}

	public void RestartGame()
	{
		CharacterSelectionPanel.ShowAlert(1);
	}

	public void ConfirmStartGame(int id)
	{
		SaveLoadXML.DeleteGameXML();
		//Destroy(PlayerInfo.Instance.gameObject);
		//GameObject.DontDestroyOnLoad(Instantiate(playerInfoPrefab, Vector3.zero, Quaternion.identity));
		PlayerInfo.Instance.Reset();
		CharacterGeneration (id, true);
	}


	void ApplyResolutionFix()
	{
//		float hight = Screen.height;
//		float width = Screen.width;
//		float res = hight / width;
//
//		if(res == 4f / 3f)
//		{
//			GameObject GlobalScale = GameObject.Find("GlobalScale");
//
//			GlobalScale.transform.localScale = new Vector3(0.8333f,0.8333f,1);
//			GameObject.Find("FillerPanel").transform.localScale = new Vector3(0.8333f,0.8333f,1);
//
//			Vector3 new_pos = new Vector3(0,0,0);
//			GlobalScale.transform.localPosition = new_pos;
//		}
	}

	public void ShowLanguageWindow()
	{
		StartGamePanel.SetActive(false);
		LanguagePanel.SetActive(true);
	}

	public void ReloadLanguage()
	{
		GameStrings.LoadData();
		LanguagePanel.SetActive(false);
		ShowSettingsPanel ();
		StartGamePanel.SetActive(true);
		GameObject.Find("new_game_button_text").GetComponent<Text>().text = Localization.Instance.GetLocale(75);
	}

	public void ReturnToMenu()
	{
		page = 0;
		NewGamePanel.SetActive (false);
		CharacterSelectionPanel.gameObject.SetActive (false);
		StartGamePanel.SetActive (true);
	}

	public void ReturnToNewGame()
	{
		page = 0;
		CharacterSelectionPanel.gameObject.SetActive (false);
		NewGame();
		//NewGamePanel.SetActive (true);
	}

	public void ShowSettingsPanel()
	{
		if (!SettingsPanel.activeSelf) {
			SettingsPanel.SetActive (true);
			ExitButtonText.text = Localization.Instance.GetLocale(76);
			ChangeLanguageButtonText.text = Localization.Instance.GetLocale(927);
			SettingsText.text = Localization.Instance.GetLocale (955);
			SoundSettingsText.text = Localization.Instance.GetLocale (956);
		} else {
			SettingsPanel.SetActive (false);
		}
	}

}