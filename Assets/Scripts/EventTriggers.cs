using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class EventTriggers : ISerializeXML
{
	public string event_name;
	public bool global;
	public bool event_status;
	public float event_chance;
	public float cooldown;

	public string[] GetStringValues()
	{
		string[] str = {event_name, /*global.ToString(),*/ event_status.ToString()/*, event_chance.ToString(), cooldown.ToString()*/};
		return str;
	}

	public void SetStringValues(string[] str)
	{
		event_name = str[0];
		//global = (bool)Convert.ChangeType(str[1], typeof(bool));
		event_status = (bool)Convert.ChangeType(str[1], typeof(bool));
		event_chance = (float)Convert.ChangeType(str[2], typeof(float));
		cooldown = (float)Convert.ChangeType(str[3], typeof(float));
	}
}
