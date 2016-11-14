﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[System.Serializable]
public class TriggerCD : ISerializeXML
{
	public string event_name;
	public bool status;
	public float globalChance;
	public float cdDay;

	public string[] GetStringValues()
	{
		string[] str = {event_name, status.ToString(), globalChance.ToString(), cdDay.ToString()};
		return str;
	}

	public void SetStringValues(string[] str)
	{
		event_name = str[0];
		status = (bool)System.Convert.ChangeType(str[1], typeof(bool));
		globalChance = (float)System.Convert.ChangeType(str[2], typeof(float));
		cdDay = (float)System.Convert.ChangeType(str[3], typeof(float));
	}
}

public class WorldTime : MonoBehaviour 
{
	private static WorldTime _instance;
	public static WorldTime Instance { get { return _instance ?? (_instance = FindObjectOfType<WorldTime>()); } }
	protected WorldTime() { _instance = null; }

	public float current_time = 360;
	public float TimeSpeed = 1;
	public GameObject outcome;
	public bool pause = true;
	public float boost;
	public bool action_on = false;

	public List<TriggerCD> globalTriggersList = new List<TriggerCD>();
	public List<TriggerCD> triggerCooldownList = new List<TriggerCD>();

	public int advertisement_cd;
	public int rewarded_advertisement_cd;

	public Text displayTime;
	public Image PeriodImage;

	float end_time;
	float current_minutes;
	
	float fix_time;
	string minutes;
	string hours;

	void Start() 
	{
		//displayTime = GameObject.Find("TimeDisplay").GetComponent<Text>();
		//PeriodImage = GameObject.Find ("PeriodImage").GetComponent<Image> ();

		current_time = SaveLoadXML.GetValue<float>("CURRENT_TIME", 360f);
		//triggerCooldownList = Game.current.trigger_cd;
		SaveLoadXML.LoadList<TriggerCD>(triggerCooldownList, "TRIGGERCD");

		if(PlayerInfo.Instance.day == 0 && current_time == 360)
		{
			foreach(TriggerCD trigger in globalTriggersList)
			{
				if(Random.Range(0,100) < trigger.globalChance) trigger.status = !trigger.status;
				trigger.cdDay = Mathf.FloorToInt(Random.Range(Mathf.CeilToInt(trigger.cdDay*0.3f),trigger.cdDay));
			}
			triggerCooldownList.AddRange(globalTriggersList);
		}

		PeriodImage.sprite = Resources.Load<Sprite> (string.Format("Icons/Time/{0}", SaveLoadXML.GetValue<int>("CURRENT_EVENT_ID", 0)));
	}

	public void Save()
	{
		SaveLoadXML.SetValue<float>("CURRENT_TIME", current_time);
		SaveLoadXML.SaveList(triggerCooldownList, "TRIGGERCD");
	}

	void Update () {
		// Если wTimes не на паузе, то выполняем...
		if (!pause) 
		{
			// Расчитываем и прокручиваем время
			Timer();
			// Выводим время на экран
			DisplayTime();
		}
	}

	// Обновляем текущее время и время окончания события
	public void UpdateTime(float _time)
	{
		current_time = _time;
		end_time = EventManager.Instance.current_event.gameObject.GetComponent<GeneralEvent>().eventTime.end;
		_timer = 0;

		DisplayTime();
	}

	// Выводим время на экран
	void DisplayTime()
	{
		//if(displayTime == null) displayTime = GameObject.Find("TimeDisplay").GetComponent<Text>();
		current_minutes = (current_time/60 - Mathf.FloorToInt(current_time/60))*60;
		minutes = ((current_minutes<10) ? "0" : "") + current_minutes;
		hours = ((Mathf.FloorToInt(current_time/60)<10) ? "0" : "") + Mathf.FloorToInt(current_time/60);
		displayTime.text = hours+":"+minutes;
	}

	// Функция расчета и прокрутки времени
	float _timer = 0;
	void Timer()
	{
		// Проверяем наступил ли новый день, 
		// после чего проверяем кулдауны триггеров и сбрасываем предложения мерчанта 
		if(current_time >= 1440)
		{
			fix_time -= 1440;
			if(fix_time < 0) fix_time = 0;
			PlayerInfo.Instance.day++;
			CheckTriggerCD();
//			EventManager.Instance.ClearItemOffer();
			SaveLoadXML.RemoveKey("ITEM_OFFERS");
			SaveLoadXML.RemoveKey("ITEM_PURCHASED");
//			if(PlayerInfo.Instance.day == 1) QuestManager.Instance.OpenInfoPanel();
			//TakeoverManager.Instance.ClearUIData();
			//TakeoverManager.Instance.ResetGoonData();

			if(Advertisement.IsReady("video") && advertisement_cd <= 0)
			{
				advertisement_cd = 10;
				//Advertisement.Show("video");
				ScreenManager.Instance.CreateScreen("BlockerPanel");
				var options = new ShowOptions {resultCallback = (showResult)=>{Destroy(ScreenManager.Instance.current);}};
				Advertisement.Show("video", options);
			}
			advertisement_cd--;
			rewarded_advertisement_cd--;
		}

		// Если текущее время равно времени окончания периода, то запускаем следующий период
		if(current_time >= end_time)
		{
			fix_time = 0;
			current_time = end_time;
			EventManager.Instance.current_event_id++;
			//print("--WorldTime");
			EventManager.Instance.LaunchGeneralEvent(false);
			action_on = false;

			PeriodImage.sprite = Resources.Load<Sprite> (string.Format("Icons/Time/{0}",EventManager.Instance.current_event_id));

			if(PlayerInfo.Instance.day > 0) 
				PlayerInfo.Instance.SaveGame();
			return;
		}

		// Если использовалось действие, которое прокручивает время, то выставлем конечное время ускорения
		if(boost > 0) 
		{
			fix_time = current_time+boost;
			boost = 0;
		}

		// Если использовалось действие, которое прокручивает время, то ускоряем прирост времени
		if(current_time < fix_time) TimeSpeed = 200;
		else 
		{
			TimeSpeed = 1;
			pause = true;
			fix_time = 0;
			if(action_on)
			{
				EventManager.Instance.DrawActions(EventManager.Instance.current_event.GetComponent<ActionListController>().actions, true);
				action_on = false;
			}
		}



		// Прирост таймеров
		_timer += Time.deltaTime*TimeSpeed;
		current_time  += Mathf.FloorToInt(_timer);

		// Проверки таймеров
		if(_timer>=1) _timer = 0;
	}

	// Устанавливаем кулдаун триггера
	public void setTriggerCD(string trigger_name, bool status, float cooldown)
	{
		TriggerCD new_cd = new TriggerCD();
		new_cd.event_name = trigger_name;
		new_cd.status = !status;
		new_cd.cdDay = PlayerInfo.Instance.day + cooldown;
		triggerCooldownList.Add(new_cd);
	}

	// Проверяем триггер
	void CheckTriggerCD()
	{
		for(int i = 0; i < triggerCooldownList.Count; i++)
		{
			TriggerCD trigger = triggerCooldownList[i];
			if(PlayerInfo.Instance.day >= trigger.cdDay)
			{
				PlayerInfo.Instance.ChangeTrigger(trigger.event_name, trigger.status, false);
				triggerCooldownList.RemoveAt(i);
			}
		}
	}

	public void SetGlobalTrigger(string trigger_name, bool status, float minDay, float maxDay)
	{
		TriggerCD trigger = new TriggerCD();
		trigger.event_name = trigger_name;
		trigger.status = !status;
		trigger.cdDay = PlayerInfo.Instance.day + Mathf.FloorToInt(Random.Range(minDay,maxDay));
		triggerCooldownList.Add(trigger);
	}
}
