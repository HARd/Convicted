﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HelpPanelController : ScreenBase 
{
	[SerializeField]
	Text HelpText;	

	[SerializeField]
	Image HelpIcon;

	[SerializeField]
	GameObject HelpArrowForward;

	[SerializeField]
	GameObject HelpArrowBack;

	public List<HelpData> helpList = new List<HelpData> ();
	int currentHelp;

	public void Start()
	{
		HelpText.text = Localization.Instance.GetLocale (helpList[currentHelp].text_id);
	}

	public void onStatPanelClick()
	{

		if (PlayerInfo.Instance.isTutorial)
			return;

		//gameObject.SetActive (true);
		HelpText.text = Localization.Instance.GetLocale (helpList[currentHelp].text_id);
		HelpIcon.sprite = helpList [currentHelp].image;

		if (currentHelp == 0) 
		{
			HelpArrowBack.SetActive (false);
		} 
		else 
		{
			HelpArrowBack.SetActive (true);
		}

		if (currentHelp == helpList.Count - 1) 
		{
			HelpArrowForward.SetActive (false);
		}
		else 
		{
			HelpArrowForward.SetActive (true);
		}

		//GUIController.Instance.RefreshCurrentPanel();
	}

	public void onStatDescriptionArrow(int i)
	{
		currentHelp += i;
		AudioManager.Instance.Play (0);

		if (currentHelp < 0)
			currentHelp = 0;
		else if (currentHelp > helpList.Count - 1)
			currentHelp = helpList.Count - 1;
		
		onStatPanelClick();
	}
}
