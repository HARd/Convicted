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

	public void OnOpenQuestLog(QuestController questController, bool isComplete)
	{
		questController.gameObject.SetActive(true);
		questController.questPin.SetActive (true);
		questController.questText.text = Localization.Instance.GetLocale(text_id);
		questController.hintText = Localization.Instance.GetLocale(hint_id);
		questController.questOk.SetActive(isComplete);
	}

	public void SetActive(bool value)
	{
		active = value;
	}

	public void Check()
	{
		//print("-- Check = " + name);	
		int req_complete = 0;
		int oneof_count = 0;
		int oneof_complete = 0;
		foreach (Parameter req in requirements) 
		{
			bool hasTool = Inventory.Instance.HasTool (req.event_name);
			switch (req.stat) 
			{
			case "stat":
				if (PlayerInfo.Instance.CheckStat (req.event_name, Mathf.FloorToInt (req.value)))
					req_complete++;
				break;
			case "item":
				if (hasTool)
					req_complete++;
				break;
			case "item_oneof":
				oneof_count++;
				if (hasTool)
					oneof_complete++;
				break;
			case "no_item":
				if (!hasTool)
					req_complete++;
				break;
			case "weapon":
				if (hasTool)
					req_complete++;
				break;
			case "no_negative_trait":
				int count = 0;
				foreach (Trait trait in PlayerInfo.Instance.traitList) 
				{
					if (trait.negative && trait.status)
						count++;
				}
				if (count == 0)
					req_complete++;
				break;
			}

			if (oneof_complete > 0) 
			{
				req_complete += oneof_count;
			} 

			if (req_complete >= requirements.Count) 
			{
				//active = false;
				complete = true;
				StoryManager.Instance.SaveCompleteStory ();
			}
		}
	}
}
