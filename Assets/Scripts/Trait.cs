using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Trait
{
	public string name;
	public int text_id;
	public string text;
	public float bonus;
	public List<Parameter> StatBonusList = new List<Parameter>();
	public float cost;
	public string exclude;
	public bool negative;
	public bool hidden;
	public bool status;

	public void Applay()
	{
//		Debug.Log(" --Trait - " + name);
//		for(int i = 0; i < StatBonusList.Count; i++)
//		{
//			Parameter param = StatBonusList[i].Applay();
//			if(param != null)
//				StatBonusList[i] = param;
//		}
		foreach(Parameter param in StatBonusList)
			param.Applay();
	}
}
