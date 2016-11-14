﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Story : MonoBehaviour 
{

	public int text_id;
	public int hint_id;
	public int image_id;
	public bool active;
	public bool complete;
	public List<Parameter> requirements;

	public void OnOpenQuestLog(QuestController questController)
	{
		questController.gameObject.SetActive(true);
		questController.questPin.SetActive (true);
		questController.questText.text = Localization.Instance.GetLocale(text_id);
		questController.hintText = Localization.Instance.GetLocale(hint_id);
	}
}
