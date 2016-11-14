using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Quest : ISerializeXML
{

	public string name;
	public string char_name;
	public string task;
	public string hint;

	public List<Parameter> rewards = new List<Parameter>();
	public List<string> removeQuestItems = new List<string>();

	void Awake()
	{
		if(Parameter.ChangeStart(rewards))
			Debug.LogErrorFormat("Object {0} Error", name);
	}

	public string[] GetStringValues()
	{
		string[] str = {name, char_name, task, hint};
		List<string> list = new List<string>(str);
		list.Add(rewards.Count.ToString());

		foreach(Parameter parameter in rewards)
			list.AddRange(parameter.GetStringValues());

		list.Add(removeQuestItems.Count.ToString());
		list.AddRange(removeQuestItems);

		return list.ToArray();
	}

	public void SetStringValues(string[] str)
	{
		name = str[0];
		char_name = str[1];
		task = str[2];
		hint = str[3];

		rewards.Clear();
		int count =  (int)Convert.ChangeType(str[4], typeof(int));
		int next = 5;
		for(int i = 0; i < count; i++)
		{
			string[] s = {str[5+i*3], str[6+i*3], str[7+i*3]};
			Parameter parameter = new Parameter();
			parameter.SetStringValues(s);
			rewards.Add(parameter);
			next += 3;
		}

		removeQuestItems.Clear();
		int removeQuestItems_count =  (int)Convert.ChangeType(str[next++], typeof(int));
		for(int i = 0; i < removeQuestItems_count; i++)
			removeQuestItems.Add(str[next+i]);
	}

}
