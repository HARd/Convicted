/*
	Создает обьект из указанного префаба.
*/


using UnityEngine;
using System.Collections;

public class PrisonEditorLoader : PrefabLoader
{
	public string language = "ru";
	public int characterID = 0;

	void Awake()
	{
		GameData.current = new GameData();
		GameData.current.lang = language;
		GameData.current.currentCharacterID = characterID;
		DoAction();
	}

	protected override void DoAction()
	{
		base.DoAction();
		InstantiatedPrefab.GetComponent<StoryManager>().enabled = false;
		InstantiatedPrefab.GetComponent<QuestManager>().enabled = false;
		InstantiatedPrefab.GetComponent<EventManager>().enabled = false;
		InstantiatedPrefab.GetComponent<WorldTime>().enabled = false;
		InstantiatedPrefab.GetComponent<CombatManager>().enabled = false;
	}
}
