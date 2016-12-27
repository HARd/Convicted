using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
//using System.Reflection;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using ObjectsExtensionMethods;

[System.Serializable]
public class statReq
{
	public string name;
	public int value;
}

public class Action : MonoBehaviour 
{

	public int text_id;
	public string text;
	public string type;
	public enum ActionColor{Default, Accept, Refuse};
	public ActionColor color;
	public float time_boost;
	public float unlockDay;
	public List<string> requireItem = new List<string>();
	public List<statReq> requireStat = new List<statReq>();
	public string requireStory;
	public List<Parameter> statChange = new List<Parameter>(); // using a class from Outcome.cs
	public float base_chance;
	public float chance;
	public List<Parameter> modifierList = new List<Parameter>();

	void Awake()
	{
		if(Parameter.ChangeStart(modifierList))
			Debug.LogErrorFormat("Object {0} Error", name);
		
		if(Parameter.ChangeStart(statChange))
			Debug.LogErrorFormat("Object {0} Error", name);
	}

	void Start()
	{
		if(text_id != 0) 
			text = Localization.Instance.GetLocale(text_id);
	}

	public void Perform_Action()
	{
		if (type == "skip" && statChange.Count != 0) {
			foreach(Parameter param in statChange)
				param.ChangeStat();
		}

		RandomEventController randomEventController = GetComponent<RandomEventController>();
		if(randomEventController != null)
		{
			GeneralEvent generalEvent = randomEventController.GetRandomEvent();
			if(generalEvent != null && generalEvent.Launch())
				return;
		}
		
		gameObject.SendMessage("action_" + type);
	}

	void action_skip()
	{
		int chance = Random.Range (0,100);
		gameObject.BroadCastSinglMessage<Outcome>("ResolveOutcome", (outcome) => {outcome.ModifyChance(); return outcome.chance > chance;});
	}

	void action_continue()
	{
		EventManager.Instance.current_event = EventManager.Instance.GeneralEventList [EventManager.Instance.current_event_id];
		StoryManager.Instance.CheckStory();
		EventManager.Instance.LaunchGeneralEvent (true);
		//EventManager.Instance.LaunchGeneralEvent (false);
		PanelManager.Instance.CallEventPanel(false);

		// If the players health is lower or equal to 0 he is sent to the infirmary
		if(PlayerInfo.Instance.health <= 0)
			EventManager.Instance.SendToInfirmary();
		
		
		WorldTime.Instance.pause = false;
	}

	void action_victory_next()
	{
		action_next("victory_outcome");
	}

	void action_defeat_next()
	{
		action_next("defeat_outcome");
	}

	void action_next(string outcom_name)
	{
		int chance = Random.Range (0,100);
		EventManager.Instance.current_event.BroadCastSinglMessage<Outcome>("ResolveOutcome", (outcome) => {
			if(outcome.name == outcom_name)
			{
				outcome.ModifyChance(); 
				return outcome.chance > chance;
			}
			return false;
		});
	}

	void action_timed()
	{
		WorldTime.Instance.boost = time_boost;
		WorldTime.Instance.pause = false;
		WorldTime.Instance.action_on = true;
		foreach(Parameter param in statChange)
			param.ChangeStat();
	}

	void action_more()
	{
		EventManager.Instance.DrawActions(this.GetChildrenComponents<Action>(), true);
	}

	void action_quest()
	{
		PanelManager.Instance.QuestPanel.CompleteQuest(gameObject.name, this);
	}

	void action_quest_agree()
	{
		QuestManager.Instance.CreateQuest();
		int chance = Random.Range (0,100);
		gameObject.BroadCastSinglMessage<Outcome>("ResolveOutcome", (outcome) => {outcome.ModifyChance(); return outcome.chance > chance;});
	}

	void action_merchant()
	{
		ScreenManager.Instance.CreateScreen("MerchantPanel");
	}

	void action_crafting()
	{
		PanelManager.Instance.CallCraftingPanel();
	}

	void action_show_ad()
	{
		if(Advertisement.IsReady("rewardedVideo") && WorldTime.Instance.rewarded_advertisement_cd <= 0)
			ShowRewardedAd();
	}
	void action_show_ad_rest()
	{
		if(Advertisement.IsReady("rewardedVideo"))
			ShowRewardedAd();
	}

	public void ShowRewardedAd()
	{
		var options = new ShowOptions { resultCallback = HandleShowResult };
		Advertisement.Show("rewardedVideo", options);
		ScreenManager.Instance.CreateScreen("BlockerPanel");
	}

	private void HandleShowResult(ShowResult result)
	{
		Destroy(ScreenManager.Instance.current);
		switch (result)
		{
		case ShowResult.Finished:
			//Debug.Log("The ad was successfully shown.");
			WorldTime.Instance.rewarded_advertisement_cd = 5;
			//GeneralEvent generalEvent = EventManager.Instance.GetAdvertisementEvent();
			GeneralEvent generalEvent = GetComponent<AdvertisementEventController>().GetEvent();
			if(generalEvent != null)
				generalEvent.Launch();
			break;
		case ShowResult.Skipped:
			break;
		case ShowResult.Failed:
			break;
		}
	}

	public void ModifyChance()
	{
		chance = base_chance;
		foreach(Parameter mod in modifierList)
			chance += mod.GetModifier();
	}

	bool VerifyRequireStat(bool no_energy)
	{
		bool hasValue = true;
		foreach(statReq stat in requireStat)
		{
			if ((no_energy && stat.name != "energy") || !PlayerInfo.Instance.CheckStat(stat.name, stat.value))
			{
				hasValue = false;
				break;
			} 
		}
		return hasValue;
	}

	public bool VerifyActionRequirements(bool no_energy = false)
	{
		bool story_check = (tag == "StoryAction") ? StoryManager.Instance.VerifyStory(requireStory) : true;
		return Inventory.Instance.HasTools(requireItem) && VerifyRequireStat(no_energy) && story_check;
	}
}
