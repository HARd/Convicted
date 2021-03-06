﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharInfoController : MonoBehaviour 
{
	public Text TraitsList;
	public Text DaysText;
	public Text CharTitleText;
	public Text TraitsLabel;
	public Image progressBar;
	public Text progressBarLabel;
	public Animator animator;

	List<string> itemList = new List<string>();

	int storyCount = 0;

	// Use this for initialization
	void Start ()
	{
		CharTitleText.text = Localization.Instance.GetLocale (949);
		TraitsLabel.text = Localization.Instance.GetLocale(56) + ":";
		progressBarLabel.text = Localization.Instance.GetLocale (998);
		gameObject.SetActive(false);
	}

	public void DrawCharWindow()
	{
		DaysText.text = Localization.Instance.GetLocale(885) + " " + (PlayerInfo.Instance.day + PlayerInfo.Instance.day_offset) + " " + Localization.Instance.GetLocale(886) + ".";

		itemList.Clear();
		foreach(Trait trait in PlayerInfo.Instance.traitList)
		{
			if(trait.status)
			{
				if(trait.text_id != 0) itemList.Add(Localization.Instance.GetLocale(trait.text_id));
				else itemList.Add(trait.text);
			}
		}

		if(itemList.Count > 0)
		{
			TraitsList.text = "";
			for(int i = 0;i < itemList.Count;i++)
			{
				TraitsList.text += itemList[i];
				if(i < (itemList.Count - 1)) TraitsList.text += ", ";
			}
			TraitsList.text += ".";
		}
		else 
			TraitsList.text = Localization.Instance.GetLocale(69);

		SetProgressBar();
	}

	public void SetProgressBar()
	{
		if(storyCount == 0)
			storyCount = StoryManager.Instance.GetStory().Count;

		float fillAmount = (float)StoryManager.Instance.GetCompleteStory().Count/storyCount;
		progressBar.fillAmount = fillAmount;
		animator.SetFloat("step", fillAmount);
	}

	public void OpenHelpScreen()
	{
		ScreenManager.Instance.CreateScreen("HelpPanel");
	}
}
