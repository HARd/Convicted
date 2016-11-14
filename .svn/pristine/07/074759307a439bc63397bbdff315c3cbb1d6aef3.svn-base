using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Sentence : ISerializeXML
{

	public string name;
	public int text_id;
	public float term;
	public List<Parameter> bonusList = new List<Parameter>();
	public int tier;
	public float chance;

	public string[] GetStringValues()
	{
		string[] str = {name, text_id.ToString(), term.ToString(), tier.ToString(), chance.ToString()};
		List<string> list = new List<string>(str);
		list.Add(bonusList.Count.ToString());

		foreach(Parameter parameter in bonusList)
			list.AddRange(parameter.GetStringValues());

		return list.ToArray();
	}

	public void SetStringValues(string[] str)
	{
		name = str[0];
		text_id = (int)Convert.ChangeType(str[1], typeof(int));
		term = (float)Convert.ChangeType(str[2], typeof(float));
		tier = (int)Convert.ChangeType(str[3], typeof(int));
		chance = (float)Convert.ChangeType(str[4], typeof(float));

		bonusList.Clear();
		int count =  (int)Convert.ChangeType(str[5], typeof(int));
		for(int i = 0; i < count; i++)
		{
			string[] s = {str[6+i*3], str[7+i*3], str[8+i*3]};
			Parameter parameter = new Parameter();
			parameter.SetStringValues(s);
			bonusList.Add(parameter);
		}
	}
	
}
