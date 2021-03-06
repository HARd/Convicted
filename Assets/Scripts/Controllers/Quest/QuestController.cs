﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class QuestController : MonoBehaviour, IPointerClickHandler
{
	public string questName;

	public string hintText;

	public Text questText;

	public GameObject removeQuestButton;
	public GameObject questPin;
	public GameObject questOk;

	public void OnPointerClick (PointerEventData eventData)
	{
		ScreenManager.Instance.CreateScreen("HintPanel");
		ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(hintText);
	}

	public void OnRemoveQuestButtonClick ()
	{
		PanelManager.Instance.RemoveQuest(questName);
	}
}
