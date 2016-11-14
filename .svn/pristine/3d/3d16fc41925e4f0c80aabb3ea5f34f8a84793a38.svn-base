using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour 
{

	private static StoryManager _instance;
	public static StoryManager Instance { get { return _instance ?? (_instance = FindObjectOfType<StoryManager>()); } }
	protected StoryManager() { _instance = null; }

	public GameObject storyActions;
	public GameObject storyTasks;

	[SerializeField]
	string saveCompleteStoryKey = "COMPLETE_STORY";

	public int activeStoreCounter {set; get;}

	// Use this for initialization
	void Start() 
	{
		storyTasks = Instantiate (storyTasks) as GameObject;

		List<string> completeStoryNames = new List<string>(SaveLoadXML.GetArrayValue(saveCompleteStoryKey));
		foreach (Story story in GetStory())
			story.complete = completeStoryNames.Exists(name => name == story.name);

		CheckStory();
	}

	public void AddStoryActions()
	{
		storyActions = Instantiate(storyActions) as GameObject;

		foreach (Transform period in storyActions.transform)
		{
			List<Action> storyActionList = new List<Action> ();
			foreach (Transform action in period.transform) 
			{
				storyActionList.Add(action.GetComponent<Action>());
			}

			int target_id = 0;
			switch(period.name)
			{
			case "morning":
				target_id = 0;
				break;
			case "day":
				target_id = 1;
				break;
			case "evening":
				target_id = 2;
				break;
			case "night":
				target_id = 3;
				break;
			}

			if (period.name != "all")
				EventManager.Instance.GeneralEventList [target_id].GetComponent<ActionListController>().actions.AddRange(storyActionList);
			else 
			{
				foreach (GameObject _event in EventManager.Instance.GeneralEventList) 
				{
					_event.GetComponent<ActionListController>().actions.AddRange(storyActionList);
				}
			}
		}
	}

	public void CheckStory()
	{
		List<Story> _storyList = new List<Story>(storyTasks.GetComponentsInChildren<Story>());
		List<Story> storyList = _storyList.FindAll (x => x.active);

		foreach (Story story in storyList) 
		{
			int req_complete = 0;
			int oneof_count = 0;
			int oneof_complete = 0;
			foreach(Parameter req in story.requirements)
			{
				bool hasTool = Inventory.Instance.HasTool(req.event_name);
				switch(req.stat)
				{
				case "stat":
					if(PlayerInfo.Instance.CheckStat(req.event_name,Mathf.FloorToInt(req.value))) req_complete++;
					break;
				case "item":
					if(hasTool)
						req_complete++;
					break;
				case "item_oneof":
					oneof_count++;
					if(hasTool)
						oneof_complete++;
					break;
				case "no_item":
					if(!hasTool)
						req_complete++;
					break;
				case "weapon":
					if(hasTool)
						req_complete++;
					break;
				case "no_negative_trait":
					int count = 0;
					foreach(Trait trait in PlayerInfo.Instance.traitList)
					{
						if(trait.negative && trait.status) count++;
					}
					if(count == 0) req_complete++;
					break;
				}

				if(oneof_complete > 0)
				{
					req_complete += oneof_count;
				} 

				if(req_complete >= story.requirements.Count)
				{
					story.complete = true;
					SaveCompleteStory();
				}
			}
		}

		if (storyList.FindAll (x => x.complete).Count == storyList.Count) 
		{
			foreach (Story story in storyList) 
			{
				story.active = false;
			}

			foreach (Story story in _storyList) 
			{
				if (!story.complete && !story.active) 
				{
					story.active = true;
					if (story.transform.childCount > 0) 
					{
						foreach (Transform child in story.transform) 
						{
							Story story_child = child.GetComponent<Story>();
							story_child.active = true;
						}
					}
					break;
				}
			}
			if(_storyList.Find(story => !story.complete) == null)
				PlayerInfo.Instance.CharacterComplete();
			else
				CheckStory();
		}

		activeStoreCounter = GetActiveStory().Count-1;
		activeStoreCounter = (activeStoreCounter < 0) ? 0 : activeStoreCounter;
	}

	public bool VerifyStory(string story_name)
	{
		List<Story> storyList = new List<Story>(storyTasks.GetComponentsInChildren<Story>());
		storyList = storyList.FindAll (x => x.active && !x.complete);

		foreach (Story story in storyList) 
		{
			//print("--VerifyStory " + story_name + " " + story.name);
			if (story.name == story_name) 
			{
				//print("--VerifyStory true");
				return true;
			}
		}
		//print("--VerifyStory false");
		return false;
	}

	public List<Story> GetStory()
	{
		return new List<Story>(storyTasks.GetComponentsInChildren<Story>());
	}

	public List<Story> GetActiveStory()
	{
		return GetStory().FindAll (x => x.active&& !x.complete);
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
		return GetStory().Find (x => x.active && !x.complete);
	}

	public void SaveCompleteStory()
	{
		SaveLoadXML.SetValue(saveCompleteStoryKey, GetCompleteStoryNames().ToArray());
		//LastChangeTime = Time.time;
	}
}
