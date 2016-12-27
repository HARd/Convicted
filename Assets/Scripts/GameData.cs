using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class GameData : ISerializeXML
{
	
	public static GameData current;
	public int currentCharacterID = 0;
	public string lang;
	public bool mute = false;
	public DateTime nextDailyBonusTime;
	public bool isTutorialCompleted = false;
	public bool isFirstOpened = true;

	List<int> charCompleted = new List<int>();

	//public GameData(){}

	public string[] GetStringValues()
	{
		string[] str = {lang, mute.ToString(), nextDailyBonusTime.ToString(), isTutorialCompleted.ToString(), currentCharacterID.ToString()};

		return str;
	}

	public void SetStringValues(string[] str)
	{
		lang = str[0];
		mute = (bool)Convert.ChangeType(str[1], typeof(bool));
		nextDailyBonusTime = (DateTime)Convert.ChangeType(str[2], typeof(DateTime));
		isTutorialCompleted = (bool)Convert.ChangeType(str[3], typeof(bool));
		currentCharacterID = (int)Convert.ChangeType(str[4], typeof(int));
	}

	public void AddCharacterCompleted(int id)
	{
		if(!charCompleted.Exists(x => x == id))
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
