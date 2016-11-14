/*
	Строки для локализации, читаются из Assets\StreamingAssets\Text\Strings.txt
	Привычный Strings.txt
	Автор: Кущ А.Е.
*/


using UnityEngine;

public sealed class GameStrings : SimpleKeyValueDataFile
{
	string resourceName = @"Text/Strings";
	string resourceExtension = ".txt";
	const string noStringFormat = "NO STRING \"{0}\"";



	private static readonly GameStrings instance = new GameStrings();
	public static GameStrings Instance { get { return instance; } }

	private GameStrings()
	{
		//LoadFromStreamingAsset(resourceName + resourceExtension);
	}

	public static void LoadData()
	{
		string ext = (GameData.current.lang == "en") ? "" : "_" + GameData.current.lang;
		instance.LoadFromStreamingAsset(instance.resourceName + ext + instance.resourceExtension);
	}

	public string GetString(string key)
	{
		return HasString(key) ? strings[key] : string.Format(noStringFormat, key);
	}

	public void DumpInfo()
	{
		string ext = (GameData.current.lang == "en") ? "" : GameData.current.lang;
		Debug.LogFormat("{0}: succesfully loaded {1} strings from {2}", GetType().Name, Count, resourceName + ext + resourceExtension);
	}
}
