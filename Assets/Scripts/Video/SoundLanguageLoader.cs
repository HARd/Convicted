using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundLanguageLoader : MonoBehaviour
{
	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip sound_ru;

	void Awake()
	{
		if(!SaveLoadXML.LoadGameDataXML())
		{
			GameData.current = new GameData();
			string lang = Application.systemLanguage.ToString();
			switch(lang)
			{
			case "Russian":
				GameData.current.lang = "ru";
				break;
			case "Ukrainian":
				GameData.current.lang = "ru";
				break;
			default:
				GameData.current.lang = "en";
				break;
			}
			SaveLoadXML.SaveGameDataXML();
		}

		if(GameData.current.lang == "ru")
			audioSource.clip = sound_ru;
	}
}
