using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class SaveLoadXML
{
	public const string convicted = "convicted.xml";
	public const string gameData = "gameData.xml";

	static DictionaryFile data = new DictionaryFile();
	static DictionaryFile game_data = new DictionaryFile();

	public static DictionaryFile GetGameData()
	{
		return game_data;
	}

	public static string GetValue(string key, string defaultValue = null)
	{
		return data.GetValue(key, defaultValue);
	}

	public static T GetValue<T>(string key, T defaultValue = default(T))
	{
		return data.GetValue<T>(key, defaultValue);
	}

	public static int[] GetIntArrayValue(string key, int[] defaultValue = null)
	{
		return data.GetIntArrayValue(key, defaultValue);
	}

	public static string[] GetArrayValue(string key, string[] defaultValue = null)
	{
		return data.GetArrayValue(key, defaultValue);
	}

	public static void SetValue(string key, string value)
	{
		data.SetValue(key, value);
	}
		
	public static void SetValue<T>(string key, T value)
	{
		data.SetValue<T>(key, value);
	}

	public static void SetValue(string key, string[] value)
	{
		data.SetValue(key, value);
	}

	public static void SetValue(string key, int[] value)
	{
		data.SetValue(key, value);
	}

	public static bool HasKey(string key)
	{
		return data.HasKey(key);
	}

	public static void RemoveKey(string key)
	{
		data.RemoveKey(key);
	}

	public static void SaveList(IList list, string str)
	{
		int i = 0;
		foreach(ISerializeXML item in list)
		{
			data.SetValue(str + i, item.GetStringValues());
			i++;
		}
		data.SetValue(str + "_count", i);
	}
		

	public static void SaveXML() 
	{
		try
		{
			data.SaveToFile(Path.Combine(DataStorage.Instance.GetPath(),convicted));
		}
		catch (System.Exception e)
		{
			Debug.LogErrorFormat("SaveLoad: cannot create {0} {1}", convicted, e);
		}

	}	

	public static void LoadList<T>(IList list, string str) where T : new()
	{
		list.Clear();
		int count = data.GetValue<int>(str + "_count", 0);
		for(int i = 0; i < count; i++)
		{
			ISerializeXML item = new T() as ISerializeXML;
			item.SetStringValues(data.GetArrayValue(str + i));
			list.Add(item);
		}
	}

	public static bool LoadXML() 
	{
		
		data = new DictionaryFile();
		data.LoadFromFile(Path.Combine(DataStorage.Instance.GetPath(), convicted));

		if (!data.IsSuccessfullyLoaded)
		{
			Debug.LogErrorFormat("Game: recreating corrupted {0}.", convicted);
			if(File.Exists(Path.Combine(DataStorage.Instance.GetPath(), convicted))) 
				File.Delete(Path.Combine(DataStorage.Instance.GetPath(), convicted));
			if(File.Exists(Path.Combine(DataStorage.Instance.GetPath(), gameData))) 
				File.Delete(Path.Combine(DataStorage.Instance.GetPath(), gameData));
		}
//		else
//			PlayerInfo.Instance.LoadGame();
		
		return data.IsSuccessfullyLoaded;
	}

	public static bool IsSuccessfullyLoaded()
	{
		return data.IsSuccessfullyLoaded;
	}
		
	public static bool LoadGameDataXML()
	{
		game_data = new DictionaryFile();
		game_data.LoadFromFile(Path.Combine(DataStorage.Instance.GetPath(), gameData));

		if (!game_data.IsSuccessfullyLoaded)
		{
			Debug.LogErrorFormat("GameData: recreating corrupted {0}.", gameData);
			if(File.Exists(Path.Combine(DataStorage.Instance.GetPath(), gameData))) 
				File.Delete(Path.Combine(DataStorage.Instance.GetPath(), gameData));
			if(File.Exists(Path.Combine(DataStorage.Instance.GetPath(), convicted))) 
				File.Delete(Path.Combine(DataStorage.Instance.GetPath(), convicted));
		}
		else
		{
			GameData.current = new GameData();
			GameData.current.SetStringValues(game_data.GetArrayValue("GameData.current"));
			GameData.current.SetAllCharactersCompleted(game_data.GetIntArrayValue("CHARACTER_COMPLETED"));
		}

		return game_data.IsSuccessfullyLoaded;
	}
		

	public static void SaveGameDataXML()
	{
		game_data.SetValue("GameData.current", GameData.current.GetStringValues());
		game_data.SetValue("CHARACTER_COMPLETED", GameData.current.GetAllCharactersCompleted());
		try
		{
			game_data.SaveToFile(Path.Combine(DataStorage.Instance.GetPath(), gameData));
		}
		catch (System.Exception e)
		{
			Debug.LogErrorFormat("SaveLoad: cannot saveGameDataXML {0} {1}", gameData, e);
		}
	}

	public static void DeleteGameXML()
	{
		try
		{
			if(File.Exists(Path.Combine(DataStorage.Instance.GetPath(),convicted))) 
				File.Delete(Path.Combine(DataStorage.Instance.GetPath(),convicted));
		}
		catch (System.Exception e)
		{
			Debug.LogErrorFormat("SaveLoad: cannot deleteGameXML {0} {1}", convicted, e);
		}
		data = new DictionaryFile();
	}

}
