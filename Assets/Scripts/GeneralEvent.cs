using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using ObjectsExtensionMethods;

[System.Serializable]
public class EventTime
{
	public float start;
	public float end;
}

public class GeneralEvent : MonoBehaviour 
{
	
	public int description_id;
	public string description;
	public EventTime eventTime = new EventTime();

	//public List<GameObject> actionList = new List<GameObject>();


	public float base_chance;
	public float chance;
	public List<Parameter> modifierList = new List<Parameter>();

	void Awake()
	{
		if(Parameter.ChangeStart(modifierList))
			Debug.LogErrorFormat("Object {0} Error", name);
	}

	void Start()
	{
		if(description_id != 0) 
			description = Localization.Instance.GetLocale(description_id);
	}

	public float ModifyChance()
	{
		chance = base_chance;
		foreach(Parameter mod in modifierList)
			chance += mod.GetModifier();

		return chance;
	}

	public bool Launch() 
	{
		//GeneralEvent eventData = this;
		PanelManager.Instance.CallEventPanel(true);
		WorldTime.Instance.pause = true;
		EventManager.Instance.current_event = gameObject;

		PanelManager.Instance.EventPanel.descriptionText.text = Localization.Instance.GetLocale(description_id);

		if(PanelManager.Instance.EventPanel.descriptionText.text != "")
			Debug.Log(tag + " - " + name);

		//print("--current_event.tag " + current_event.tag + " " + );
		switch(tag)
		{
		case "QuestEvent":
			if(QuestManager.Instance.CheckFreeChar(description)) 
			{
				QuestManager.Instance.GenerateQuest(description);
				PanelManager.Instance.EventPanel.descriptionText.text = QuestManager.Instance.descriptionText;
				//Debug.Log(descriptionText.text);
				break;
			} 
			else 
			{
				PanelManager.Instance.CallEventPanel(false);
				EventManager.Instance.current_event = EventManager.Instance.GeneralEventList[EventManager.Instance.current_event_id];
				return false;
			}
		case "SingleCombat":
			CombatManager.Instance.ResolveCombat(description,1);
			return true;
		case "GroupCombat":
			CombatManager.Instance.ResolveCombat("",Mathf.FloorToInt(Random.Range(4,6)));
			return true;
		case "SingleMurder":
			CombatManager.Instance.ResolveCombat(description,1);
			return true;
		case "GroupMurder":
			CombatManager.Instance.ResolveCombat("",Mathf.FloorToInt(Random.Range(4,6)));
			return true;
		case "ShakedownPlayer":
			Shakedown();
			return true;
		case "Shakedown":
			Shakedown();
			return true;
		}

		List<Action> actionList = this.GetChildrenComponents<Action>();
		if(actionList.Count == 0) 
		{
			int chance = Random.Range (0,100);
			this.BroadCastSinglMessage<Outcome>("ResolveOutcome", (outcom) => {outcom.ModifyChance(); return chance <= outcom.chance;});
		}
		else
			EventManager.Instance.DrawActions(actionList, true);

		return true;
	}

	void Shakedown() 
	{
		string outcome = "notfoundOutcome";


		if(Random.Range(0,100) > PlayerInfo.Instance.consealment + PlayerInfo.Instance.CheckTrait("smuggler",true)) 
		{
			string toolName = Inventory.Instance.GetRandomToolName();
			if(toolName != null)
			{
				PlayerInfo.Instance.EquipItem(toolName,false,false);
				outcome = "foundOutcome";
			}
		}

		BroadcastMessage("OnResolveOutcome", outcome);
	}

	public Sprite GetIcon(ref float chance)
	{
		Sprite sprite = null;
		foreach (Parameter mod in modifierList)
		{
			sprite = mod.GetIcon();
			if(sprite != null)
			{
				ModifyChance();
				chance = (mod.value < 0) ? 100 - this.chance : this.chance;
				break;
			}
		}
		return sprite;
	}
}
