#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ObjectsExtensionMethods;
using UnityEditor;

public class EditorStoryManager : StoryManager
{
	[SerializeField]
	ContainerControllerBase container;

	const string resourcePath = "Assets/Resources/";

	public override void CheckStory()
	{
		container.Draw(storyActions.transform);
	}

	void OnApplicationQuit()
	{
		//PrefabUtility.CreatePrefab(resourcePath + resourceStoryActionsPath + GameData.current.currentCharacterID + ".prefab", storyActions);
		//GameStrings.SaveData();
	}

	public void SaveCharacter()
	{
		PrefabUtility.CreatePrefab(resourcePath + resourceStoryActionsPath + GameData.current.currentCharacterID + ".prefab", storyActions);
	}

	public void SaveStrings()
	{
		GameStrings.SaveData();
	}
}
#endif
