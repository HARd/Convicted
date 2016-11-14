using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class DailyReward
{
	public string day;
	public List<Parameter> rewardList = new List<Parameter>();
}

public class EventManager : MonoBehaviour 
{

	private static EventManager _instance;
	public static EventManager Instance { get { return _instance ?? (_instance = FindObjectOfType<EventManager>()); } }
	protected EventManager() { _instance = null; }

	public List<GameObject> GeneralEventList = new List<GameObject>();
	public List<GameObject> StartEventList = new List<GameObject>();
	public List<GameObject> AdvertisementEventList = new List<GameObject>();
	public List<DailyReward> dailyRewardList = new List<DailyReward>();
	public Action continue_action;
	public Action next_action;
	public int current_event_id = 0;
	public GameObject current_event;
	//public bool trigged_event = false;

	Text descriptionText;

	//Tutorial variables
	//public bool isTutorial;

	GeneralEvent eventData;

	// Use this for initialization
	void Start () 
	{
		descriptionText = PanelManager.Instance.EventPanel.descriptionText;

		current_event_id = SaveLoadXML.GetValue<int>("CURRENT_EVENT_ID", 0);

		// Adding story actions
		StoryManager.Instance.AddStoryActions();

		// Launching general event
		LaunchGeneralEvent(false);

		//if (GameData.current.characterPoints == 0 && PlayerInfo.Instance.day == 0) 
		RunTutorial();
	}

	public void Save()
	{
		SaveLoadXML.SetValue<int>("CURRENT_EVENT_ID", current_event_id);
	}

	public void LaunchGeneralEvent(bool continue_event) 
	{
		if(current_event_id >= GeneralEventList.Count) 
			current_event_id = 0;
		
		current_event = GeneralEventList[current_event_id];
		GeneralEvent eventData = current_event.GetComponent<GeneralEvent>();
		//print("--LaunchGeneralEvent " + continue_event);
		DrawActions (eventData.GetComponent<ActionListController>().actions, true);
		if (!continue_event) 
		{
			WorldTime.Instance.UpdateTime (eventData.eventTime.start);
		} 
	}

	public void ModifyChanceAdvertisementEventList()
	{
		foreach(GameObject go in AdvertisementEventList)
			go.GetComponent<GeneralEvent>().ModifyChance();
	}

	public GeneralEvent GetAdvertisementEvent()
	{
		ModifyChanceAdvertisementEventList();
		AdvertisementEventList.Sort((a, b) => a.GetComponent<GeneralEvent>().chance.CompareTo(b.GetComponent<GeneralEvent>().chance));
		//int chance = Random.Range(0,100);
		GameObject obj = AdvertisementEventList.Find(go => Random.Range(0,100) < go.GetComponent<GeneralEvent>().chance);
		return (obj != null) ? obj.GetComponent<GeneralEvent>() : null;
	}

	public void SendToInfirmary()
	{
		PanelManager.Instance.EventPanel.gameObject.SetActive(true);
		WorldTime.Instance.pause = true;
		descriptionText.text = Localization.Instance.GetLocale(63);
		PlayerInfo.Instance.ChangeTrigger("infirmary",true, true);
		CreateContinue();
	}


	public void ClearActions()
	{
		if(current_event.tag == "GeneralEvent") 
			PanelManager.Instance.ActionPanel.ClearActions();
		else 
			PanelManager.Instance.EventPanel.EventActionPanel.ClearActions();
	}


	public void DrawActions(List<Action> actionList, bool SpecialAction)
	{
		//print("-- DrawActions current_event.tag  " + current_event.tag );
		if(current_event.tag == "GeneralEvent") 
			PanelManager.Instance.ActionPanel.DrawActions(actionList, SpecialAction);
		else 
			PanelManager.Instance.EventPanel.EventActionPanel.DrawActions(actionList, SpecialAction);
	}



	public void CreateContinue()
	{
		ClearActions();
		PanelManager.Instance.EventPanel.EventActionPanel.GetComponent<ActionPanelController>().DrawAction(0, continue_action, new Color32(50,50,50,255));
	}

	public void CreateNext(string type)
	{
		next_action.GetComponent<Action>().type = type+"_next";

		ClearActions();
		PanelManager.Instance.EventPanel.EventActionPanel.GetComponent<ActionPanelController>().DrawAction(0, next_action, new Color32(50,50,50,255));
	}

	public void RunTutorial()
	{
		if(!GameData.current.HasCharacterCompleted(0) && !SaveLoadXML.HasKey("TUTORIAL_SHOWED"))
			ScreenManager.Instance.CreateScreen("Tutorial");
	}

}
