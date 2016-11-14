using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PersonalGoal : ISerializeXML
{

	public string name;
	public int text_id;
	public float char_pts;
	public List<Parameter> requirementsList = new List<Parameter>();
	public bool complete;

	public string[] GetStringValues()
	{
		string[] str = {name, text_id.ToString(), char_pts.ToString(), complete.ToString()};
		List<string> list = new List<string>(str);
		list.Add(requirementsList.Count.ToString());

		foreach(Parameter parameter in requirementsList)
			list.AddRange(parameter.GetStringValues());

		return list.ToArray();
	}

	public void SetStringValues(string[] str)
	{
		name = str[0];
		text_id = (int)Convert.ChangeType(str[1], typeof(int));
		char_pts = (float)Convert.ChangeType(str[2], typeof(float));
		complete = (bool)Convert.ChangeType(str[3], typeof(bool));

		requirementsList.Clear();
		int count =  (int)Convert.ChangeType(str[4], typeof(int));
		for(int i = 0; i < count; i++)
		{
			string[] s = {str[5+i*3], str[6+i*3], str[7+i*3]};
			Parameter parameter = new Parameter();
			parameter.SetStringValues(s);
			requirementsList.Add(parameter);
		}
	}
}
