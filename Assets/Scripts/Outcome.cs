﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[System.Serializable]
public class ItemMod 
{
	public enum EItem{Add, Remove};
	public EItem mod;
	public string item;
}

public class Outcome : MonoBehaviour 
{
	
	public int description_id;
	public string description;
	public bool continuous;
	public List<ItemMod> itemMods = new List<ItemMod>();
	public List<Parameter> statChange = new List<Parameter>();
	public float base_chance;
	public float chance;
	public List<Parameter> modifierList = new List<Parameter>();

	Text descriptionText;

	void Awake()
	{
		if(Parameter.ChangeStart(modifierList))
			Debug.LogErrorFormat("Object {0} Error", name);
	
		if(Parameter.ChangeStart(statChange))
			Debug.LogErrorFormat("Object {0} Error", name);
	}

	void Start()
	{
		if(description_id != 0) 
			description = Localization.Instance.GetLocale(description_id);
	}


	public void ResolveOutcome()
	{
		Debug.Log("Outcome - " + name);
		descriptionText = PanelManager.Instance.EventPanel.descriptionText;
		descriptionText.text = description;
		foreach(Parameter param in statChange)
		{
			if(param.stat == "")
			{
				if(param.event_name != "")
					PlayerInfo.Instance.ChangeTrigger(param.event_name, param.value == 1, param.value != -1);
			}
			else 
				param.ChangeStat();
		}

		foreach(ItemMod mod in itemMods)
			PlayerInfo.Instance.EquipItem(mod.item, mod.mod == ItemMod.EItem.Add);

		if(gameObject.name == "solitary_outcome" || gameObject.name == "infirmary_outcome") 
		{
			EventManager.Instance.current_event_id = 0;

			if(PlayerInfo.Instance.health < 50) 
				new health("","",-5).ChangeStat();
			else
			{
				new health("","",100).ChangeStat();
				new health("","",-50).ChangeStat();
			}

			WorldTime.Instance.UpdateTime(360);
			PlayerInfo.Instance.day += 3;
		}

		EventManager.Instance.CreateContinue();
		if(!continuous) 
			WorldTime.Instance.current_time += EventManager.Instance.current_event.GetComponent<GeneralEvent>().eventTime.end;

		SendMessage("DoAction", SendMessageOptions.DontRequireReceiver);
	}

	public float ModifyChance()
	{
		chance = base_chance;
		foreach(Parameter mod in modifierList)
			chance += mod.GetModifier();

		return chance;
	}

	public void OnResolveOutcome(string name)
	{
		if(this.name == name)
			ResolveOutcome();
	}

	public Sprite GetIcon()
	{
		Parameter param = modifierList.Find(mod => mod.GetIcon() != null);
		return (param != null) ? param.GetIcon() : null;
	}
}
