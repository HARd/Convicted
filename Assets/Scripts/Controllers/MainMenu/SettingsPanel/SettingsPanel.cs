using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SettingsPanel : ScreenBase 
{

	[SerializeField]
	Image mute_sound_button;

	[SerializeField]
	Sprite sound_on;

	[SerializeField]
	Sprite sound_off;

//	[SerializeField]
//	Text exitButtonText;

	[SerializeField]
	Text SoundSettingsText;

	[SerializeField]
	string[] changeLanguages;

	[SerializeField]
	Text changeLanguageText;
	
	[SerializeField]
	Text SettingsText;

	[SerializeField]
	Button ForwardArrow;

	[SerializeField]
	Button BackArrow;

	int currentLanguage = 0;

	// Use this for initialization
	void Start () 
	{
		if(GameData.current.mute) 
			mute_sound_button.sprite = sound_off;
		else 
			mute_sound_button.sprite = sound_on;


		currentLanguage = Array.FindIndex(changeLanguages, x => x == GameData.current.lang);
		CheckButton();
		Show();
	}

	public void Show()
	{
		changeLanguageText.text = Localization.Instance.GetLocale(928);
		//exitButtonText.text = Localization.Instance.GetLocale(76);
		SettingsText.text = Localization.Instance.GetLocale (955);
		SoundSettingsText.text = Localization.Instance.GetLocale (956);
	}


	public void MuteSound()
	{
		if(GameData.current.mute)
		{
			mute_sound_button.sprite = sound_on;
			GameData.current.mute = false;
			AudioManager.Instance.Play(0);
		} 
		else
		{
			mute_sound_button.sprite = sound_off;
			GameData.current.mute = true;
		} 
		SaveLoadXML.SaveGameDataXML();
	}

	public void OnClickForwardArrow()
	{
		currentLanguage++;
		CheckButton();
		SwitchLanguage();

	}
	public void OnClickBackArrow()
	{
		currentLanguage--;
		CheckButton();
		SwitchLanguage();
	}
		
	void SwitchLanguage()
	{
		AudioManager.Instance.Play(0);
		GameData.current.lang = changeLanguages[currentLanguage];
		SaveLoadXML.SaveGameDataXML();
		GameStrings.LoadData();
		MainMenu.Instance.ReloadLanguage();
		Show();
	}

	void CheckButton()
	{
		ForwardArrow.interactable = currentLanguage < changeLanguages.Length - 1;
		BackArrow.interactable = currentLanguage > 0;
	}
}
