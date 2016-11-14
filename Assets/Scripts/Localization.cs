using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Xml;
//using System.IO;
//using System.Xml.Serialization;

//[System.Serializable]
//public class Locale
//{
//	[XmlAttribute("id")]
//	public int id;
//	public string ru;
//	public string en;
//
//}
//
//[XmlRoot("Localization")]
//public class SaveLocales
//{
//	[XmlArray("locales"),XmlArrayItem("locale")]
//	public List<Locale> writeLocales = new List<Locale>();
//
//	public void SaveXmlData()
//	{
//		var serializer = new XmlSerializer(typeof(SaveLocales));
//		using(var stream = new FileStream(Path.Combine(Application.persistentDataPath, "locales.xml"), FileMode.Create))
//		{
//			serializer.Serialize(stream, this);
//		}
//	}
//	
//	public void LoadLocales()
//	{
//		int i = 0;
//		foreach(GameObject _object in GameObject.FindGameObjectsWithTag("Action"))
//		{
//			Action action = _object.gameObject.GetComponent<Action>();
//			if(action.text_id == 0)
//			{
//				Locale locale = new Locale();
//				locale.id = Localization.Instance.localeList.Count+i;
//				locale.ru = action.text;
//				locale.en = _object.name;
//				if(CheckLocales(locale.ru))
//				{
//					writeLocales.Add(locale);
//					i++;
//				}
//			}
//		}
//		foreach(GameObject _object in GameObject.FindGameObjectsWithTag("RandomEvent"))
//		{
//			GeneralEvent eventData = _object.gameObject.GetComponent<GeneralEvent>();
//			if(eventData.description_id == 0 && eventData.description != "")
//			{
//				Locale locale = new Locale();
//				locale.id = Localization.Instance.localeList.Count+i;
//				locale.ru = eventData.description;
//				locale.en = _object.name;
//				if(CheckLocales(locale.ru))
//				{
//					writeLocales.Add(locale);
//					i++;
//				}
//			}
//		}
//
//		foreach(GameObject _object in GameObject.FindGameObjectsWithTag("Outcome"))
//		{
//			Outcome outcome = _object.gameObject.GetComponent<Outcome>();
//			if(outcome.description_id == 0 && outcome.description != "")
//			{
//				Locale locale = new Locale();
//				locale.id = Localization.Instance.localeList.Count+i;
//				locale.ru = outcome.description;
//				locale.en = _object.name;
//				if(CheckLocales(locale.ru))
//				{
//					writeLocales.Add(locale);
//					i++;
//				}
//			}
//		}
//	}
//
//	bool CheckLocales(string text_check)
//	{
//		foreach(Locale locale in writeLocales)
//		{
//			if(locale.ru == text_check) return false;
//		}
//		foreach(Locale locale in Localization.Instance.localeList)
//		{
//			if(locale.ru == text_check) return false;
//		}
//		return true;
//	}
//
//
//}

public class Localization : MonoBehaviour 
{

	private static Localization _instance;
	public static Localization Instance { get { return _instance ?? (_instance = FindObjectOfType<Localization>()); } }
	protected Localization() { _instance = null; }

//	public TextAsset langFile;
//	public List<Locale> localeList = new List<Locale>();
//	string lang;
	bool ready = false;

	// Use this for initialization
	void Start () 
	{
//		if(!ready)
//		{
//			LoadXmlData();
//			ready = true;
//		}
//		GameStrings.LoadData();

		//SaveLocales save = new SaveLocales();
		//save.LoadLocales();
		//save.SaveXmlData();
	}

	public string GetLocale(int _id)
	{
		//lang = GameData.current.lang;

		if(!ready) //LoadXmlData();
		{
			GameStrings.LoadData();
			ready = true;
		}

//		if(_id == 0) return "";
//
//		switch(lang)
//		{
//		case "ru":
//			_locale = localeList[_id-1].ru;
//			break;
//		case "en":
//			_locale = localeList[_id-1].en;
//			break;
//		}
	
		string _locale = GameStrings.Instance.GetString(_id.ToString());
		return _locale.Replace("\\n", "\n").Replace("[green]", "<color=#147832>").Replace("[/green]", "</color>");
	}

//	void ProcessXmlData(XmlNodeList nodes)
//	{
//		Locale xmlLocale;
//		foreach(XmlNode node in nodes)
//		{
//			xmlLocale = new Locale();
//			xmlLocale.id = int.Parse(node.Attributes.GetNamedItem("id").Value);
//			xmlLocale.ru = node.SelectSingleNode("ru").InnerText;
//			xmlLocale.en = node.SelectSingleNode("en").InnerText;
//			localeList.Add(xmlLocale);
//		}
//
//		StreamWriter dataWriter = new StreamWriter("Strings_ru.txt");
//		foreach(var loc in localeList)
//		{
//			string str = loc.id + " = \"" + loc.ru + "\"";
//			dataWriter.WriteLine(str);
//		}
//		dataWriter.Flush();
//		dataWriter.Close(); 
//
//		dataWriter = new StreamWriter("Strings.txt");
//		foreach(var loc in localeList)
//		{
//			string str = loc.id + " = \"" + loc.en + "\"";
//			dataWriter.WriteLine(str);
//		}
//		dataWriter.Flush();
//		dataWriter.Close(); 
//	}
//
//	void LoadXmlData()
//	{
//		XmlDocument xmlDoc = new XmlDocument();
//		xmlDoc.LoadXml(langFile.text);
//		ProcessXmlData(xmlDoc.SelectNodes("locales/locale"));
//		ready = true;
//	}
//




}
