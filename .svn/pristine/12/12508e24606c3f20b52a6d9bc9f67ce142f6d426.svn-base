using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Parameter
{
	public string stat;
	public string event_name;
	public float value;

	public Parameter(){}

	public Parameter(string stat, string event_name, float value)
	{
		this.stat = stat;
		this.event_name = event_name;
		this.value = value;
	}

	public string[] GetStringValues()
	{
		string[] str = {stat, event_name, value.ToString()};
		return str;
	}

	public void SetStringValues(string[] str)
	{
		stat = str[0];
		event_name = str[1];
		value = (float)Convert.ChangeType(str[2], typeof(float));
	}

	public virtual void Applay(){}
	public virtual float GetModifier(){Debug.LogError("--Parameter.GetModifier() - " + stat); return 0f;}
	public virtual Sprite GetIcon(){Debug.LogError("--Parameter.GetIcon() - " + stat); return null;}
	public virtual void ChangeStat(){Debug.LogError("--Parameter.ChangeStat() - " + stat);}

	public static Parameter CreateParameter(string stat, string event_name, float value, bool class_name_stat = true)
	{
		string class_name = (class_name_stat) ? stat : event_name;
		Type t = Type.GetType(class_name);
		if (t != null)
		{
			System.Reflection.ConstructorInfo ci = t.GetConstructor(new Type[] {typeof(string), typeof(string), typeof(float)});
			return ci.Invoke(new object[] {stat, event_name, value}) as Parameter;
		}

		Debug.LogErrorFormat("Class {0} Error", class_name);
		return null;
	}

	public static bool ChangeStart(List<Parameter> paramlist, bool class_name_stat = true)
	{
		bool error = false;
		for(int i = 0; i < paramlist.Count; i++)
		{
			if(paramlist[i].stat == "")
					paramlist[i].stat = "trigger";
				
			Parameter newParameter = CreateParameter(paramlist[i].stat, paramlist[i].event_name, paramlist[i].value, class_name_stat);
			if(newParameter == null) 
				error = true;
			else
				paramlist[i] = newParameter;
		}
		return error;
	}
}
