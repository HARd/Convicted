﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ObjectsExtensionMethods;

public class QuestPanelController : MonoBehaviour 
{
	[SerializeField]
	QuestController[] questControllers;

	[SerializeField]
	Text personalGoalText;

	[SerializeField]
	Text personalGoalLabel;

	public List<Quest> questLog = new List<Quest>();

	void Start()
	{
		personalGoalLabel.text = Localization.Instance.GetLocale(707);
		gameObject.SetActive(false);
	}

	public QuestController GetQuest(int ID)
	{
		return questControllers[ID];
	}

	public QuestController GetQuest(string questName)
	{
		return System.Array.Find(questControllers, qu => {return qu.questName == questName;});
	}

	public void OpenQuestLog()
	{
		CleanUp();

		int i = 0;

		Story story = StoryManager.Instance.GetFirstActiveStory();
		personalGoalText.text = Localization.Instance.GetLocale(story.text_id);
		foreach (Transform child in story.transform) 
		{
			Story child_story = child.GetComponent<Story>();
			child_story.OnOpenQuestLog(questControllers[i++], child_story.complete);
		}

		foreach(Quest quest in questLog)
		{
			questControllers[i].gameObject.SetActive(true);
			questControllers[i].questText.text = quest.task;
			questControllers[i].questText.text += "\n\n" + Localization.Instance.GetLocale(112) + ": ";
			for(int j = 0;j < quest.rewards.Count;j++)
			{
				if(quest.rewards[j].stat == "cash")
				{
					if(j == 1) 
						questControllers[i].questText.text += "<color=black>, </color>";
					
					questControllers[i].questText.text += "<color=black>"+quest.rewards[j].value + " " + Localization.Instance.GetLocale(865) + "</color>";
				}
				else if(quest.rewards[j].stat == "inmate_rep")
				{
					if(j == 1) 
						questControllers[i].questText.text += "<color=black>, </color>";
					
					questControllers[i].questText.text += "<color=black>" + "+" + quest.rewards[j].value + " " + Localization.Instance.GetLocale(90) + "</color>";
				}
				else if(quest.rewards[j].stat == "guard_rep")
				{
					if(j == 1) 
						questControllers[i].questText.text += "<color=black>, </color>";
					
					questControllers[i].questText.text += "<color=black>" + "+" + quest.rewards[j].value + " " + Localization.Instance.GetLocale(103) + "</color>";
				}
			}

			questControllers[i].hintText = quest.hint;

			questControllers[i].gameObject.SetActive (true);
			questControllers[i].removeQuestButton.SetActive(true);
			questControllers[i].questName = quest.char_name;
			i++;
		}
	}


	public void RemoveQuest(string char_name)
	{
		//print("--RemoveQuest " + char_name);
		Quest quest = questLog.Find(x => x.char_name == char_name );
		if(quest != null)
		{
			PlayerInfo.Instance.EquipItem(char_name+"_quest",false,false);
			questLog.Remove(quest);
		}
		OpenQuestLog();
	}

	void CleanUp()
	{
		foreach(QuestController questController in questControllers)
		{
			questController.questText.text = "";
			questController.gameObject.SetActive(false);
			questController.removeQuestButton.SetActive(false);
			questController.questPin.SetActive(false);
			questController.questOk.SetActive(false);
		}
	}

	public void CompleteQuest(string char_name, Action quest_action) 
	{
		Quest quest = questLog.Find(x => x.char_name == char_name );
		if(quest != null)
		{
			PanelManager.Instance.CallEventPanel(true);
			WorldTime.Instance.pause = true;

			foreach(string item in quest.removeQuestItems) 
			{
				PlayerInfo.Instance.EquipItem(item,false,false);
			}

			switch(char_name) 
			{
			case "oscar":
				char_name = Localization.Instance.GetLocale(125);
				break;
			case "michael":
				char_name = Localization.Instance.GetLocale(132);
				break;
			case "vincent":
				char_name = Localization.Instance.GetLocale(140);
				break;
			case "old_jim":
				char_name = Localization.Instance.GetLocale(164);
				break;
			case "chief":
				char_name = Localization.Instance.GetLocale(239);
				break;
			}

			PanelManager.Instance.EventPanel.descriptionText.text = char_name + " " + Localization.Instance.GetLocale(757);
			PanelManager.Instance.EventPanel.descriptionText.text += "\n\n" + Localization.Instance.GetLocale(112) + ": <color=#147832>";

			foreach(Parameter reward in quest.rewards) 
			{
				reward.ChangeStat();
				if(reward.stat != "item" && reward.stat != "weapon" && reward.stat != "trigger") 
				{
					PanelManager.Instance.EventPanel.descriptionText.text += " +" + reward.value;
					if(reward.stat == "cash") 
						PanelManager.Instance.EventPanel.descriptionText.text += " " + Localization.Instance.GetLocale(865) + " ";
					else if(reward.stat == "inmate_rep") 
						PanelManager.Instance.EventPanel.descriptionText.text += " " + Localization.Instance.GetLocale(90) + " ";
					else if(reward.stat == "guard_rep") 
						PanelManager.Instance.EventPanel.descriptionText.text += " " + Localization.Instance.GetLocale(103) + " ";
				} 
			}
			PanelManager.Instance.EventPanel.descriptionText.text += "</color>";

			EventManager.Instance.CreateContinue();
			if(PlayerInfo.Instance.isTutorial)
			{
				EventManager.Instance.continue_action.Perform_Action();
			}
			questLog.Remove(quest);
		}
	}
}
