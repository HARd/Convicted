/*
	Сохраняемый в XML ассоицативный массив с проверкой контрольной суммы при загрузке.
	Используется в реализации сейвов.
	Автор: Кущ А.Е.
*/


using UnityEngine;

using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;

using System.Linq;

using System;
using System.IO;

public class DictionaryFile
{
	public const string CHECK_HASH_KEY = "___HASH___";

	public bool IsSuccessfullyLoaded { get; private set; }
	public bool HasHashError { get; private set; }

	public class item
	{
		[XmlAttribute]
		public string key;
		[XmlAttribute]
		public string value;
	}
	Dictionary<string, string> dict = new Dictionary<string, string>();

	public void SetValue(string key, string value)
	{
		dict[key] = value;
	}

	public void SetValue<T>(string key, T value)
	{
		dict[key] = value.ToString();
	}

	public void SetValue(string key, string[] value)
	{
		SetValue(key, string.Join(";", value));
	}

	public void SetValue(string key, int[] value)
	{
		SetValue(key, string.Join(";", value.Select(x => x.ToString()).ToArray()));
	}

	public bool HasKey(string key)
	{
		return dict.ContainsKey(key);
	}

	public int Count { get { return dict.ContainsKey(CHECK_HASH_KEY) ? dict.Count - 1 : dict.Count; } }

	public bool IsEmpty { get { return Count == 0; } }

	public void RemoveKey(string key)
	{
		if (HasKey(key))
			dict.Remove(key);
	}

	public string GetValue(string key, string defaultValue = null)
	{
		return HasKey(key) ? dict[key] : defaultValue;
	}

	public T GetValue<T>(string key, T defaultValue = default(T))
	{
		return HasKey(key) ? (T)Convert.ChangeType(dict[key], typeof(T)) : defaultValue;
	}

	public int AddInt(string key, int value)
	{
		int savedValue = GetValue<int>(key, 0);
		savedValue += value;
		SetValue(key, savedValue);

		return savedValue;
	}

	public string[] GetArrayValue(string key, string[] defaultValue = null)
	{
		if (HasKey(key))
			return dict[key].Split(new Char[] { ';' });//, StringSplitOptions.RemoveEmptyEntries);
		else
			return defaultValue ?? new string[0];
	}

	public int[] GetIntArrayValue(string key, int[] defaultValue = null)
	{
		if (HasKey(key))
			return dict[key].Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
		else
			return defaultValue ?? new int[0];
	}

	int CalculateHash()
	{
		int result = 0;
		foreach (var record in dict)
		{
			if (record.Key != CHECK_HASH_KEY)
			{
				result += record.Key.GetHashCode();
				if (record.Value != null)
					result += record.Value.GetHashCode();
			}
		}

		return result;
	}

	void AddHashRecord()
	{
		dict[CHECK_HASH_KEY] = CalculateHash().ToString();
	}

	bool IsHashValid()
	{
		return HasKey(CHECK_HASH_KEY) && dict[CHECK_HASH_KEY] == CalculateHash().ToString();
	}

	void CheckHash()
	{
		if (!IsHashValid())
		{
			HasHashError = true;
			IsSuccessfullyLoaded = false;
			Clear();
			Debug.Log("DictionaryFile data load hash error");
		}
	}

	public void SaveToFile(string filename)
	{
		AddHashRecord();

		if (!Directory.Exists(Path.GetDirectoryName(filename)))
			Directory.CreateDirectory(Path.GetDirectoryName(filename));

		FileStream stream = new FileStream(filename, FileMode.Create);

		XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
		ns.Add("", "");
		using (XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true }))
		{
			XmlSerializer serializer = new XmlSerializer(typeof(item[]), new XmlRootAttribute() { ElementName = "items" });
			serializer.Serialize(writer, dict.Select(kv => new item() { key = kv.Key, value = kv.Value }).ToArray(), ns);
		}

		stream.Close();
	}

	public void LoadFromFile(string filename)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(item[]), new XmlRootAttribute() { ElementName = "items" });

		IsSuccessfullyLoaded = false;
		try
		{
			using (FileStream stream = new FileStream(filename, FileMode.Open))
			{
				dict = ((item[])serializer.Deserialize(stream)).ToDictionary(i => i.key, i => i.value);
				IsSuccessfullyLoaded = true;
			}
		}
		catch (System.Exception)
		{
			Clear();
		}

		if (IsSuccessfullyLoaded)
			CheckHash();
	}

	public void Clear()
	{
		dict.Clear();
	}

	public void DebugDump()
	{
		foreach (KeyValuePair<string, string> item in dict)
			Debug.Log(String.Format("KEY= {0}, VALUE = {1}", item.Key, item.Value));
	}
}
