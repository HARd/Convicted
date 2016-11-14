using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Goal : ISerializeXML
{

	public string name;
	public string plan;
	public int parent_id;
	public int task_id;
	public string task;
	public int image_id;
	public int hint_id;
	public bool complete;
	public int unlock;
	public int locked;
	
	public List<Parameter> requirements = new List<Parameter>();

	public string[] GetStringValues()
	{
		string[] str = {name, plan, parent_id.ToString(), task_id.ToString(), task, image_id.ToString(), hint_id.ToString(), complete.ToString(), unlock.ToString(), locked.ToString()};
		List<string> list = new List<string>(str);
		list.Add(requirements.Count.ToString());

		foreach(Parameter parameter in requirements)
			list.AddRange(parameter.GetStringValues());

		return list.ToArray();
	}

	public void SetStringValues(string[] str)
	{
		name = str[0];
		plan = str[1];
		parent_id = (int)Convert.ChangeType(str[2], typeof(int));
		task_id = (int)Convert.ChangeType(str[3], typeof(int));
		task = str[4];
		image_id = (int)Convert.ChangeType(str[5], typeof(int));
		hint_id = (int)Convert.ChangeType(str[6], typeof(int));
		complete = (bool)Convert.ChangeType(str[7], typeof(bool));
		unlock = (int)Convert.ChangeType(str[8], typeof(int));
		locked = (int)Convert.ChangeType(str[9], typeof(int));

		requirements.Clear();
		int count =  (int)Convert.ChangeType(str[10], typeof(int));
		for(int i = 0; i < count; i++)
		{
			string[] s = {str[11+i*3], str[12+i*3], str[13+i*3]};
			Parameter parameter = new Parameter();
			parameter.SetStringValues(s);
			requirements.Add(parameter);
		}
	}

}
