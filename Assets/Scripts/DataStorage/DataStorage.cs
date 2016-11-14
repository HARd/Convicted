/*
	Инакапсулирует и предоставляет функционал определения места хранения профилей, настроек,
	сейвов, логов и прочего.
	Автор: Кущ А.Е.
*/


using UnityEngine;
using System;
using System.IO;

public class DataStorage
{
	private static DataStorage instance;
	public static DataStorage Instance
	{
		get
		{
			if (instance == null)
				instance = new DataStorage();

			return instance;
		}
	}

	public DataStorage()
	{
//		if (!Directory.Exists(Path.GetDirectoryName(GetPath())))
//			Directory.CreateDirectory(Path.GetDirectoryName(GetPath()));
		Directory.CreateDirectory(GetPath());
		Debug.LogFormat("Storage path is {0}", GetPath());
	}

	public string GetSystemPath()
	{
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
		return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
#else
		return Application.persistentDataPath;
#endif
	}

	public string GetPath()
	{
//		string path = GetSystemPath();
//
//		path = Path.Combine(path, "Game Gourmet");//UnityEditor.PlayerSettings.companyName);
//		path = Path.Combine(path, "Convicted - JailBreak");//UnityEditor.PlayerSettings.productName.Replace(":", " -"));
//
//		return path;
		return Application.persistentDataPath;
	}

	public string GetLogsPath()
	{
		return Path.Combine(GetPath(), "Logs");
	}
}
