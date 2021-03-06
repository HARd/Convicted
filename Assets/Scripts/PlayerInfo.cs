﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour,  ISerializeXML 
{
	private static PlayerInfo _instance;
	public static PlayerInfo Instance { get { return _instance ?? (_instance = FindObjectOfType<PlayerInfo>()); } }

	public float health = 100;
	public int max_health = 100;//
	public float cash = 50;
	public float energy = 100;
	public float body = 0;
	public int max_str = 100;//
	public float charisma = 0;
	public int max_dex = 100;//
	public float mind = 0;
	public int max_int = 100;//
	public float inmate_rep = 0;
	public float guard_rep = 0;
	public float day = 0;
	public int day_offset = 0;
	public float tunnel;
	public int consealment;

	public List<Item> inventory = new List<Item>();
	public List<string> hidden = new List<string>();
	public List<Trait> traitList = new List<Trait>();
	public List<EventTriggers> triggerList = new List<EventTriggers>();

	public bool isTutorial {set; get;}

	public string[] GetStringValues()
	{
		string[] str = {health.ToString(), cash.ToString(), energy.ToString(), body.ToString(), charisma.ToString(), mind.ToString(), 
			inmate_rep.ToString(), guard_rep.ToString(), day.ToString(), tunnel.ToString(), consealment.ToString(), 
			max_health.ToString(), max_str.ToString(), max_dex.ToString(), max_int.ToString()};
		return str;
	}

	public void SetStringValues(string[] str)
	{
		health = (float)Convert.ChangeType(str[0], typeof(float));
		cash = (float)Convert.ChangeType(str[1], typeof(float));
		energy = (float)Convert.ChangeType(str[2], typeof(float));
		body = (float)Convert.ChangeType(str[3], typeof(float));
		charisma = (float)Convert.ChangeType(str[4], typeof(float));
		mind = (float)Convert.ChangeType(str[5], typeof(float));
		inmate_rep = (float)Convert.ChangeType(str[6], typeof(float));
		guard_rep = (float)Convert.ChangeType(str[7], typeof(float));
		day = (float)Convert.ChangeType(str[8], typeof(float));
		tunnel = (float)Convert.ChangeType(str[9], typeof(float));
		consealment = (int)Convert.ChangeType(str[10], typeof(int));
		max_health = (int)Convert.ChangeType(str[11], typeof(int));
		max_str = (int)Convert.ChangeType(str[12], typeof(int));
		max_dex = (int)Convert.ChangeType(str[13], typeof(int));
		max_int = (int)Convert.ChangeType(str[14], typeof(int));
	}
		
	public void Reset()
	{
		cash = 50;
		day = 0;
		tunnel = 0;
		health = max_health;
		energy = max_health;
	}

	public Item GetItem(string name)
	{
		return inventory.Find(item => item.name == name);
	}

	public bool HasItem(string name)
	{
		return inventory.Exists(item => item.name == name);
	}

	public List<Item> GetItems()
	{
		return inventory.FindAll(item => item.damage == 0);
	}

	public Item GetRandomItem(Predicate<Item> p)
	{
		List<Item> items = GetItems().FindAll(p);
		return (items.Count == 0) ? null : items[UnityEngine.Random.Range(0, items.Count-1)];
	}
		
	public int GetRandomItemIndex(Predicate<Item> p)
	{
		Item rItem = GetRandomItem(p);
		return inventory.FindIndex(item => item.name == rItem.name);
	}

	public List<Item> GetWeapons()
	{
		return inventory.FindAll(item => item.damage > 0);
	}

	public Item GetRandomWeapon()
	{
		List<Item> weapons = GetWeapons();
		return weapons[UnityEngine.Random.Range(0, weapons.Count-1)];
	}

	public bool HasHidden(string name)
	{
		return hidden.Exists(item => item == name);
	}
		
	void Start()
	{
		//print("PlayerInfo.Start()");
		//LoadXmlData();
		if(SceneManager.GetActiveScene().name == "Editor")
			return;

		if(SaveLoadXML.LoadXML())
			LoadGame();

//		GameData.current.AddCharacterCompleted(0);
//		GameData.current.AddCharacterCompleted(1);
	}

	void Awake()
	{
		foreach (Trait trait in traitList) 
		{
			if(Parameter.ChangeStart(trait.StatBonusList))
				Debug.LogErrorFormat("Object {0} Error", name);
		}
	}

	public void LoadGame()
	{
		SetStringValues(SaveLoadXML.GetArrayValue("PLAYER_INFO"));

		ResetTraitList();
		foreach(string name in SaveLoadXML.GetArrayValue("TRAITS"))
			traitList.Find(trait => trait.name == name).status = true;

		foreach(string name in SaveLoadXML.GetArrayValue("TRIGGER"))
			triggerList.Find(trigger => trigger.event_name == name).event_status = true;
	}

	public void SaveGame()
	{
		//print("--SaveGame()");
		SaveLoadXML.SetValue("PLAYER_INFO", GetStringValues());

		List<string> traitNames = new List<string>();
		foreach(Trait trait in traitList.FindAll(trait => trait.status))
			traitNames.Add(trait.name);
		if(traitNames.Count > 0)
			SaveLoadXML.SetValue("TRAITS", traitNames.ToArray());

		List<string> triggerNames = new List<string>();
		foreach(EventTriggers trigger in triggerList.FindAll(trigger => trigger.event_status))
			triggerNames.Add(trigger.event_name);
		if(triggerNames.Count > 0)
			SaveLoadXML.SetValue("TRIGGER", triggerNames.ToArray());

		if(QuestManager.Instance != null)
			QuestManager.Instance.Save();

		if(WorldTime.Instance != null)
			WorldTime.Instance.Save();

		if(EventManager.Instance != null)
			EventManager.Instance.Save();

		if(StoryManager.Instance != null)
			StoryManager.Instance.SaveCompleteStory();
	
		SaveLoadXML.SaveXML(); 
	}

	public void CharacterComplete()
	{
		GameData.current.AddCharacterCompleted(GameData.current.currentCharacterID);
		SaveLoadXML.SaveGameDataXML();
		SaveLoadXML.DeleteGameXML();
		Reset();
		ScreenManager.Instance.CreateScreen("EndGamePanel");
	}

	public float CheckTrigger(string event_name, bool status)
	{
		float chance = 0;
		foreach(EventTriggers _event in triggerList)
		{
			if(_event.event_name == event_name && _event.event_status == status) 
				chance = _event.event_chance;
		}
		return chance;
	}

	public void ChangeTrigger(string event_name, bool status, bool cooldown)
	{
		foreach(EventTriggers _event in triggerList)
		{
			if(_event.event_name == event_name)
			{
				_event.event_status = status;
				if(!_event.global && cooldown && _event.cooldown > 0) 
					WorldTime.Instance.setTriggerCD(_event.event_name, _event.event_status, _event.cooldown);
				
				if(_event.global && cooldown && _event.cooldown > 0) 
					WorldTime.Instance.SetGlobalTrigger(_event.event_name, _event.event_status, Mathf.CeilToInt(_event.cooldown*0.3f), _event.cooldown);
				
			}
		}
	}

	public bool CheckStat(string _stat, float _value)
	{
		switch(_stat)
		{
		case "body":
			if(_value >= 0)
			{
				if(body >= _value) return true;
				else return false;
			}
			else
			{
				if(body < _value*(-1)) return true;
				else return false;
			}
		case "charisma":
			if(_value >= 0)
			{
				if(charisma >= _value) return true;
				else return false;
			}
			else
			{
				if(charisma < _value*(-1)) return true;
				else return false;
			}
		case "mind":
			if(_value >= 0)
			{
				if(mind >= _value) return true;
				else return false;
			}
			else
			{
				if(mind < _value*(-1)) return true;
				else return false;
			}
		case "health":
			if(_value >= 0)
			{
				if(health >= _value) return true;
				else return false;
			}
			else
			{
				if(health < _value*(-1)) return true;
				else return false;
			}
		case "energy":
			if(_value >= 0)
			{
				if(energy >= _value) return true;
				else return false;
			}
			else
			{
				if(energy < _value*(-1)) return true;
				else return false;
			}
		case "cash":
			if(_value >= 0)
			{
				if(cash >= _value) return true;
				else return false;
			}
			else
			{
				if(cash < _value*(-1)) return true;
				else return false;
			}
		case "inmate_rep":
			if(_value >= 0)
			{
				if(inmate_rep >= _value) return true;
				else return false;
			}
			else
			{
				if(inmate_rep <_value*(-1)) return true;
				else return false;
			}

		case "guard_rep":
			if(_value >= 0)
			{
				if(guard_rep >= _value) return true;
				else return false;
			}
			else
			{
				if(guard_rep < _value*(-1)) return true;
				else return false;
			}
		case "negative_inmate_rep":
			if(inmate_rep <= _value) return true;
			else return false;
		case "negative_guard_rep":
			if(guard_rep <= _value) return true;
			else return false;
		case "day":
			if(_value >= 0)
			{
				if(day >= _value) return true;
				else return false;
			}
			else
			{
				if(day < _value*(-1)) return true;
				else return false;
			}
		case "tunnel":
			if(_value >= 0)
			{
				if(tunnel >= _value) return true;
				else return false;
			}
			else
			{
				if(tunnel < _value*(-1)) return true;
				else return false;
			}
		default:
			return false;
		}
	}

	//---------------------------------------------------------------
	void RefreshInventory(string name, bool equip)
	{
		if(equip)
		{
			//print(name);
			Inventory.Instance.AddTool(name);
		}
		else
		{
			Inventory.Instance.DestroyTool(name);
		}
	}
	//---------------------------------------------------------------

	public void EquipItem(string name, bool equip, bool purchase = false, bool recipe = false)
	{
		//print("-- EquipItem " + name);
		if(name == "all" && !equip)
			Inventory.Instance.DestroyAllTool();
		else 
		{
			if(purchase) 
				cash -= GetItem(name).cost;
			else if(HasItem(name) && !recipe)
			{
				GameObject screen = ScreenManager.Instance.CreateScreen("FindToolScreen");
				Sprite invSprite = SpriteManager.Instatce.GetSprite("Tools/" + name + "/inv");
				screen.GetComponent<FindToolScreen>().SetScreenView(invSprite, PlayerInfo.Instance.GetItemText(name), equip);
			}
			
			RefreshInventory(name, equip);
		}
	}
		
	public float GetTrait(string name)
	{
		float bonus = 0;
		foreach(Trait trait in traitList)
		{
			if(trait.name == name) bonus = trait.bonus;
		}
		return bonus;
	}

	public void ResetTraitList()
	{
		foreach (Trait trait in traitList) 
			trait.status = false;
	} 

	public float CheckTrait(string name, bool status)
	{
		Trait trait = traitList.Find(t => t.name == name && t.status == status);
		return (trait != null) ? trait.bonus : 0f;
	} 

	public void ChangeTrait(string name, bool status)
	{
		//print("--ChangeTrait " + name + " " + status);	
		Trait trait = traitList.Find(t => t.name == name);
		if(trait != null)
			trait.status = status;
	}
		

	public string GetItemText(string name)
	{
		return Localization.Instance.GetLocale(GetItem(name).text_id);
	}

	void OnDestroy()
	{
		_instance = null;
		//SaveGame();
	}
}
