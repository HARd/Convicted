/*
	Читалка файла ключ-значение.
	Используется, например, в Strings.txt, Links.txt, MoreGames.txt, BuildSettings.txt 
	Автор: Кущ А.Е.
*/


using UnityEngine;

using System.Text.RegularExpressions;
using System.Collections.Generic;

using System;
using System.IO;
using System.Linq;

public class SimpleKeyValueDataFile
{
	protected Dictionary<string, string> strings;
	protected List<string> keysInReadingOrder;

	public void LoadFromStreamingAsset(string name, bool fillKeysInReadingOrder = false)
	{
		strings = new Dictionary<string, string>();

		if (fillKeysInReadingOrder)
			keysInReadingOrder = new List<string>();

		LoadStrings(ResourcesLoader.LoadStreamingText(name).Split('\n'));
	}

	public void LoadFromResource(string name, bool fillKeysInReadingOrder = false)
	{
		strings = new Dictionary<string, string>();

		if (fillKeysInReadingOrder)
			keysInReadingOrder = new List<string>();

		LoadStrings(ResourcesLoader.LoadResourceText(name).Split('\n'));
	}

	void LoadStrings(IEnumerable<string> stringSet)
	{
		int lineNumber = 0;
		foreach (string line in stringSet)
		{
			lineNumber += 1;
			ParseLine(line, lineNumber);
		}
	}

	void ParseLine(string line, int lineNumber)
	{
		string trimmedLine = line.Trim();

		if (trimmedLine.Length == 0 || trimmedLine.StartsWith(";") || trimmedLine.StartsWith("#") || trimmedLine.StartsWith("//"))
			return;

		Regex r = new Regex(@"(^\w+)\s*=\s*(\S.*)$");
		Match m = r.Match(trimmedLine);

		if (!m.Success)
		{
			Debug.LogErrorFormat("{0}: parsing error in line {1}.", GetType().Name, lineNumber);
			return;
		}

		string key = m.Groups[1].Value;
		string value = m.Groups[2].Value;

		//remove quotes if any
//		if (value[0] == '"' && value[value.Length - 1] == '"')
//			value = value.Substring(1, value.Length - 2);

		//convert supported escaped characters
		value = value.Replace("\\n", "\n");
		value = value.Replace("\\\"", "\"");

		if (HasString(key))
			Debug.LogWarningFormat("{0}: line {1} key {2} already exists. Replacing old value.", GetType().Name, lineNumber, key);

		strings[key] = value;

		if (keysInReadingOrder != null)
			keysInReadingOrder.Add(key);
	}

	public virtual string GetString(string key, string defaultValue = null)
	{
		return HasString(key) ? strings[key] : defaultValue;
	}

	public virtual void SetString(string key, string value)
	{
		if (HasString(key))
			Debug.LogWarningFormat("{0}: key {1} already exists. Replacing old value.", GetType().Name, key);
		
		strings[key] = value;
	}

	public virtual int Count { get { return strings.Count; } }

	public bool HasString(string key)
	{
		if (key == null)
			return false;

		return strings.ContainsKey(key);
	}

	public List<string> GetKeysInReadingOrder()
	{
		return keysInReadingOrder;
	}

	public int GetNextKey()
	{
		List<int> keys = new List<int>();
		foreach(string str in strings.Keys)
		{
			int res = 0;
			if(Int32.TryParse(str, out res))
				keys.Add(res);
		}
		return keys.Max() + 1;
	}

	public void SaveFromStreamingAsset(string name)
	{
		Debug.Log(name);
		StreamWriter sr = File.CreateText(name);
		foreach(var str in strings)
		{
			string value = str.Value;
			value = value.Replace("\n", "\\n");
			//value = value.Replace("\"", "\\\"");
			sr.WriteLine (str.Key + " =  " + value);
		}
		sr.Close();
	}
}
