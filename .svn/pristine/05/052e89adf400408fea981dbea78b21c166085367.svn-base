using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class GameData : ISerializeXML
{
	
	public static GameData current;
	public string lang;
	public bool mute = false;
	//public float characterPoints = 0;
	//public List<string> personalGoalList = new List<string>();
	public DateTime nextDailyBonusTime;
	public bool isTutorialCompleted = false;

	List<int> charCompleted = new List<int>();

	public GameData(){}

	public string[] GetStringValues()
	{
		string[] str = {lang, mute.ToString(), nextDailyBonusTime.ToString(), isTutorialCompleted.ToString()};
		//List<string> list = new List<string>(str);

		//list.Add(personalGoalList.Count.ToString());
		//list.AddRange(personalGoalList);

		//return list.ToArray();
		return str;
	}

	public void SetStringValues(string[] str)
	{
		lang = str[0];
		mute = (bool)Convert.ChangeType(str[1], typeof(bool));
		//characterPoints = (float)Convert.ChangeType(str[2], typeof(float));
		nextDailyBonusTime = (DateTime)Convert.ChangeType(str[2], typeof(DateTime));
		isTutorialCompleted = (bool)Convert.ChangeType(str[3], typeof(bool));
	
		//personalGoalList.Clear();
//		int personalGoalList_count =  (int)Convert.ChangeType(str[5], typeof(int));
//		for(int i = 0; i < personalGoalList_count; i++)
//			personalGoalList.Add(str[i+6]);

	}

	public void AddCharacterCompleted(int id)
	{
		charCompleted.Add(id);
	}

	public void AddAllCharactersCompleted(int[] ids)
	{
		charCompleted.AddRange(ids);
	}

	public void SetAllCharactersCompleted(int[] ids)
	{
		charCompleted = new List<int>(ids);
	}

	public int[] GetAllCharactersCompleted()
	{
		return charCompleted.ToArray();
	}

	public bool HasCharacterCompleted(int id)
	{
		return charCompleted.Exists(i => i == id);
	}
}
