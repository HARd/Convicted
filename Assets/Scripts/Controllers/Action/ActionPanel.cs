﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActionPanel : ActionPanelController 
{
	public Text BodyText;
	public Text BodyValueText;
	public Image BodyChangeIcon;
	public Text CharismaText;
	public Text CharismaValueText;
	public Image CharismaChangeIcon;
	public Text MindText;
	public Text MindValueText;
	public Image MindChangeIcon;
	public Text InmateRepText;
	public Text InmaterepValueText;
	public Image InmaterepChangeIcon;
	public Text GuardRepText;
	public Text GuardrepValueText;
	public Image GuardrepChangeIcon;
	public GameObject CashDisplayPanel;
	public Text cash_s;
	public Text EnergyText;
	public Text HealthText;

	public Image CharacterImage;
	public Text CharacterImageText;
	public Text CharacterNameText;

	public Text displayTime;
	public Image PeriodImage;

	string body_locale;
	string charisma_locale;
	string mind_locale;
	string inmaterep_locale;
	string guardrep_locale;

	public bool cashBlocker = false;

	void Awake () 
	{
		body_locale = Localization.Instance.GetLocale(57);
		mind_locale = Localization.Instance.GetLocale(59);
		charisma_locale = Localization.Instance.GetLocale(58);
		inmaterep_locale = Localization.Instance.GetLocale(61);
		guardrep_locale = Localization.Instance.GetLocale(60);

		BodyChangeIcon.gameObject.SetActive (false);
		CharismaChangeIcon.gameObject.SetActive (false);
		MindChangeIcon.gameObject.SetActive (false);
		InmaterepChangeIcon.gameObject.SetActive (false);
		GuardrepChangeIcon.gameObject.SetActive (false);

		CharacterImage.sprite = SpriteManager.Instatce.GetSprite("Pers/pers_" + GameData.current.currentCharacterID);
		CharacterNameText.text = Localization.Instance.GetLocale(815 + GameData.current.currentCharacterID);
	}

	void Update()
	{
		UpdateStats();
	}

	void UpdateStats()
	{
		BodyText.text = body_locale + ":";
		BodyValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.body) + "/" + PlayerInfo.Instance.max_str;
		MindText.text = mind_locale + ":";
		MindValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.mind) + "/" + PlayerInfo.Instance.max_int;
		CharismaText.text = charisma_locale + ":";
		CharismaValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.charisma) + "/" + PlayerInfo.Instance.max_dex;
		InmateRepText.text = inmaterep_locale + ":";
		InmaterepValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.inmate_rep) + "/200";
		GuardRepText.text = guardrep_locale + ":";
		GuardrepValueText.text = Mathf.FloorToInt(PlayerInfo.Instance.guard_rep) + "/200";
		EnergyText.text = Mathf.FloorToInt(PlayerInfo.Instance.energy).ToString();
		HealthText.text = Mathf.FloorToInt(PlayerInfo.Instance.health).ToString();
		if(!cashBlocker && !PanelManager.Instance.EventPanel.gameObject.activeSelf)
			cash_s.text = Mathf.FloorToInt(PlayerInfo.Instance.cash).ToString();

	}

	public void ActivateChangeIcon(float value, Image icon)
	{
		icon.gameObject.SetActive(true);
		icon.transform.localRotation = Quaternion.identity;
		icon.GetComponent<GUIEffect> ().timer = 2;

		if (value > 0) 
		{
			icon.color = new Color32 (0,255,0,255);
		} 
		else 
		{
			icon.color = new Color32 (255,0,0,255);
			icon.transform.Rotate (0,0,180);
		}
	}

	public void OpenHelpScreen()
	{
		ScreenManager.Instance.CreateScreen("HelpPanel");
	}

	public void OpenCharacterScreen()
	{
		ScreenManager.Instance.CreateScreen("CharacterPanel");
	}

	public void ShowTimeTable()
	{
		ScreenManager.Instance.CreateScreen ("TimeTablePanel");
	}
}
