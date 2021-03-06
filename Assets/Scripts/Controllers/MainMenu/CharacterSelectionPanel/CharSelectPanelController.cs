﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System.IO;

public class CharSelectPanelController : MonoBehaviour
{

	[SerializeField]
	Image SelectedCharacterImage;

//	[SerializeField]
//	Text SelectedCharacterNumber;

	[SerializeField]
	Text BodyText;

	[SerializeField]
	Text BodyValueText;

	[SerializeField]
	Text CharismaText;

	[SerializeField]
	Text CharismaValueText;

	[SerializeField]
	Text MindText;

	[SerializeField]
	Text MindValueText;

	[SerializeField]
	Text InmateRepText;

	[SerializeField]
	Text InmateRepValueText;

	[SerializeField]
	Text GuardRepText;

	[SerializeField]
	Text GuardrRepValueText;

	[SerializeField]
	Text TraitsText;

	[SerializeField]
	Text StoryText;

	[SerializeField]
	Text StoryLabel;

	[SerializeField]
	Text CharacterLabelText;

	[SerializeField]
	Text CharacterNameText;

	[SerializeField]
	Text StoryLabelText;

	[SerializeField]
	Text SentenceText;

	[SerializeField]
	GameObject StartGameButton;

	[SerializeField]
	Text StartGameButtonText;

	[SerializeField]
	GameObject RestartGameButton;

	public bool overwritingAlert {set; get;}
	int character_id;

	// Use this for initialization
	void Start () 
	{
		StartGameButtonText.text = (SaveLoadXML.HasKey("PLAYER_INFO") && character_id == GameData.current.currentCharacterID) ? Localization.Instance.GetLocale(32) : Localization.Instance.GetLocale(645);
	}

	public void ShowCharacterData(int id, bool reset_character)
	{
		//print("--ShowCharacterData " + id + " " + reset_character + " " + PlayerInfo.Instance.charImageID);
		character_id = id;

		overwritingAlert = false;
		StartGameButtonText.text = (SaveLoadXML.HasKey("PLAYER_INFO") && character_id == GameData.current.currentCharacterID) ? Localization.Instance.GetLocale(32) : Localization.Instance.GetLocale(645);

		//if (MainMenu.Instance.charactersList[id].cost > GameData.current.characterPoints) 
		if (character_id == 0 || GameData.current.HasCharacterCompleted(id-1)) 
		{
			StartGameButton.SetActive (true);
			
			if (SaveLoadXML.HasKey("PLAYER_INFO") && !reset_character) 
			{
				PlayerInfo.Instance.LoadGame();
				if (character_id == GameData.current.currentCharacterID) 
				{
					//print(0);
					RestartGameButton.SetActive (true);
				} 
				else 
				{
					//print(1);
					overwritingAlert = true;
					RestartGameButton.SetActive (false);
					CharacterGenerator.GenerateCharacter(id);
				}
			} 
			else 
			{
				//print(2);
				RestartGameButton.SetActive (false);
				CharacterGenerator.GenerateCharacter(id);
			}
			
		}
		else 
		{
			//print(3);
			StartGameButton.SetActive (false);
			RestartGameButton.SetActive (false);

			CharacterGenerator.GenerateCharacter(id);
		} 


		SelectedCharacterImage.sprite = SpriteManager.Instatce.GetSprite("Pers/pers_" + character_id);//MainMenu.Instance.CharacterImageList[id];
//		SelectedCharacterNumber.text = PlayerInfo.Instance.characterNumber; 

		BodyText.text = Localization.Instance.GetLocale (57) + ": ";
		BodyValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.body) + "/" + PlayerInfo.Instance.max_str;

		CharismaText.text = Localization.Instance.GetLocale (58) + ": ";
		CharismaValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.charisma) + "/" + PlayerInfo.Instance.max_dex;

		MindText.text = Localization.Instance.GetLocale(59) + ": ";
		MindValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.mind) + "/" + PlayerInfo.Instance.max_int;

		InmateRepText.text = Localization.Instance.GetLocale (61) + ": ";
		InmateRepValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.inmate_rep) + "/200";

		GuardRepText.text = Localization.Instance.GetLocale (60) + ": ";
		GuardrRepValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.guard_rep) + "/200";

		CharacterNameText.text = Localization.Instance.GetLocale(MainMenu.Instance.charactersList [id].name_id);
		StoryLabel.text = Localization.Instance.GetLocale(MainMenu.Instance.charactersList [id].story_name_id); //882
		//StoryLabelText.text = "<color=#147832>" + Localization.Instance.GetLocale(957) + "</color>" + ":";
		StoryText.text = Localization.Instance.GetLocale(MainMenu.Instance.charactersList [id].story_id); //883
		SentenceText.text = Localization.Instance.GetLocale(883) + " " + MainMenu.Instance.charactersList [id].sentence + " " + Localization.Instance.GetLocale(884) + ".";


		TraitsText.text = Localization.Instance.GetLocale(56) + ":\n"; // "<color=blue>" + "</color>" + 

		List<Trait> traitList = PlayerInfo.Instance.traitList.FindAll(t => t.status);
		if(traitList.Count > 0)
		{
			int i = 0;
			foreach(Trait trait in traitList)
			{
				TraitsText.text += Localization.Instance.GetLocale(trait.text_id);
				if(i++ < traitList.Count-1) TraitsText.text += ", ";
			}

			TraitsText.text += ".";
		}
		else 
			TraitsText.text += Localization.Instance.GetLocale(69);
	}

	public void ShowAlert(int type)
	{
		//HintPanel.SetActive (true);
		int locale = 971 + type;
		ScreenManager.Instance.CreateScreen("HintPanel");
		ScreenManager.Instance.current.GetComponent<Hint>().ShowDialog(Localization.Instance.GetLocale(locale), Localization.Instance.GetLocale (973), (confirm) => {
			if(confirm)
			{
				SaveLoadXML.DeleteGameXML();
				PlayerInfo.Instance.Reset();
				ShowCharacterData(character_id,true);
				if(type == 0)
					LoadScene(2);
			}
		});
	}

	public void OpenCharacterScreen()
	{
		ScreenManager.Instance.CreateScreen("CharacterPanel").GetComponent<CharacterScreen>().SetCharacterImage(character_id);
	}

	public void ReturnToNewGame()
	{
		AudioManager.Instance.Play (0);
		gameObject.SetActive (false);
		MainMenu.Instance.NewGame();
	}

	public void RestartGame()
	{
		ShowAlert(1);
	}

	public void StartGame()
	{
		AudioManager.Instance.Play(1);

		//if(!File.Exists("Assets/Resources/Story/Story/character" + character_id + ".prefab"))
		if(!MainMenu.Instance.charactersList[character_id].available)
		{
			ScreenManager.Instance.CreateScreen("HintPanel");
			ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(Localization.Instance.GetLocale(768));
			return;
		}

		if (overwritingAlert || (GameData.current.HasCharacterCompleted(character_id) && character_id != GameData.current.currentCharacterID && SaveLoadXML.HasKey("PLAYER_INFO")))
		{
			ShowAlert(0);
		} 
		else 
			LoadScene(2);
	}

	void LoadScene(int number)
	{
		GameData.current.currentCharacterID = character_id;
		SaveLoadXML.SaveGameDataXML();
		SceneManager.LoadScene(number);
	}
}
