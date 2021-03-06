﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using ObjectsExtensionMethods;

public class StoryManager : MonoBehaviour 
{

	private static StoryManager _instance;
	public static StoryManager Instance { get { return _instance ?? (_instance = FindObjectOfType<StoryManager>()); } }
	protected StoryManager() { _instance = null; }

	[SerializeField]
	string saveCompleteStoryKey = "COMPLETE_STORY";

	public int activeStoreCounter {set; get;}

	public GameObject storyActions {set; get;}
	public GameObject storyTasks {set; get;}

	public const string resourceStoryPath = "Story/Story/character";
	public const string resourceStoryActionsPath = "Story/StoryActions/character";


	// Use this for initialization
	void Start() 
	{
		AddStoryActions();

		storyTasks = Instantiate(Resources.Load(resourceStoryPath + GameData.current.currentCharacterID, typeof(GameObject))) as GameObject;
		List<string> completeStoryNames = new List<string>(SaveLoadXML.GetArrayValue(saveCompleteStoryKey));
		foreach (Story story in GetStory())
			story.complete = completeStoryNames.Exists(name => name == story.name);

		//if(SceneManager.GetActiveScene().name != "Editor")
			CheckStory();
	}

	public void AddStoryActions()
	{
		storyActions = Instantiate(Resources.Load(resourceStoryActionsPath + GameData.current.currentCharacterID, typeof(GameObject))) as GameObject;

		foreach (Transform period in storyActions.transform)
		{
			List<Action> storyActionList = period.GetChildrenComponents<Action>();
			if (period.name != "all")
				EventManager.Instance.GeneralEventList.Find(x => x.name == period.name).GetComponent<ActionListController>().actions.AddRange(storyActionList);
			else 
				foreach (GameObject _event in EventManager.Instance.GeneralEventList) 
					_event.GetComponent<ActionListController>().actions.AddRange(storyActionList);
		}
	}

	public virtual void CheckStory()
	{
		Story storyActive = storyTasks.GetFirstComponentInChildren<Story>(x => x.active);
		if(storyActive != null)
		{
			storyActive.BroadcastMessage("Check", SendMessageOptions.DontRequireReceiver);
			if(storyActive.GetFirstComponentInChildren<Story>(x => !x.complete) == null && ((storyActive.requirements.Count == 0) || storyActive.complete))
			{
				storyActive.complete = true;
				storyActive.BroadcastMessage("SetActive", false, SendMessageOptions.DontRequireReceiver);
				storyActive = storyTasks.GetFirstComponentInChildren<Story>(x => !x.active && !x.complete);
				if(storyActive == null)
					PlayerInfo.Instance.CharacterComplete();
				else
				{
					storyActive.BroadcastMessage("SetActive", true, SendMessageOptions.DontRequireReceiver);
					CheckStory();
				}
			}
		}

		activeStoreCounter = GetActiveStory().Count-1;
		activeStoreCounter = (activeStoreCounter < 0) ? 0 : activeStoreCounter;
	}

	public bool VerifyStory(string story_name)
	{
		return GetActiveStory().Exists(x => x.name == story_name);
	}

	public List<Story> GetStory()
	{
		return new List<Story>(storyTasks.GetComponentsInChildren<Story>());
	}

	public List<Story> GetActiveStory()
	{
		return GetStory().FindAll (x => x.active && !x.complete);
	}

	public List<Story> GetCompleteStory()
	{
		return GetStory().FindAll (x => x.complete);
	}

	public List<string> GetCompleteStoryNames()
	{
		List<string> storyNamesList = new List<string>();
		foreach(Story story in GetCompleteStory())
			storyNamesList.Add(story.name);

			return storyNamesList;
	}

	public Story GetFirstActiveStory()
	{
		//return GetStory().Find (x => x.active && !x.complete);
		return storyTasks.GetFirstComponentInChildren<Story>(x => x.active && !x.complete);
	}

	public void SaveCompleteStory()
	{
		SaveLoadXML.SetValue(saveCompleteStoryKey, GetCompleteStoryNames().ToArray());
		//LastChangeTime = Time.time;
	}
}
