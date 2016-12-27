#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;
using ObjectsExtensionMethods;

public class EditContainer : MonoBehaviour 
{
	[SerializeField]
	ContainerController container2;
	[SerializeField]
	ContainerEventController container3;
	[SerializeField]
	GameObject characterEmptyActions;
	[SerializeField]
	GameObject characterEmpty;

	[SerializeField]
	GameObject bttn_create_event;
	[SerializeField]
	GameObject bttn_create_outcom_event;
	[SerializeField]
	GameObject bttn_create_action;
	[SerializeField]
	GameObject bttn_create_outcom;
	[SerializeField]
	GameObject bttn_create_continue_action;
	[SerializeField]
	GameObject bttn_delete;
	[SerializeField]
	GameObject bttn_cut;

	const string resourcePath = "Assets//Resources";
	const string resourceStoryPath = "Story//Story";
	const string resourceStoryActionsPath = "Story//StoryActions";

	const string actionName = "Action";
	const string eventName = "Event";
	const string outcomeName = "Outcome";

	public void OnCreateCharacter()
	{
		Destroy(StoryManager.Instance.storyActions);
		Destroy(StoryManager.Instance.storyTasks);
		StoryManager.Instance.storyActions = Instantiate(characterEmptyActions) as GameObject;
		StoryManager.Instance.storyActions.name = GetMaxCharacterName();
		StoryManager.Instance.storyTasks = Instantiate(characterEmpty) as GameObject;
		StoryManager.Instance.storyTasks.name = StoryManager.Instance.storyActions.name;
		StoryManager.Instance.CheckStory();
	}

	public void OnCreateAction(GameObject ActionEmpty)
	{
		Transform storyTransform = ContainerControllerBase.GetStoryTransform();
		//print("--OnCreateAction2 " + storyTransform);
		if(storyTransform != null)
		{
			SetPrefabName(Instantiate(ActionEmpty, storyTransform) as GameObject);
		}
		//ContainerController.current.Draw();
		EditorEventManager.TriggerEvent("Draw_" + ContainerControllerBase.currentContainerController.name);
	}

	public void OnCreateEvent(GameObject EventEmptyPrefab)
	{
		Transform storyTransform = ContainerControllerBase.GetStoryTransform();
		if(storyTransform != null)
		{
			GameObject go = Instantiate(EventEmptyPrefab, storyTransform) as GameObject;
			SetEventPrefabName(go);
		
			if(storyTransform.GetComponent<Action>() != null)
			{

				RandomEventController randomEventController = storyTransform.GetComponent<RandomEventController>();
				if(randomEventController == null)
					randomEventController = storyTransform.gameObject.AddComponent<RandomEventController>();

				randomEventController.randomEventList.Add(go.GetComponent<GeneralEvent>());
			}
			//ContainerController.current.Draw();
			EditorEventManager.TriggerEvent("Draw_" + ContainerControllerBase.currentContainerController.name);
		}

	}

	public void OnDelete()
	{
		Transform storyTransform = ContainerControllerBase.GetStoryTransform();
		if(storyTransform != null)
		{
			GeneralEvent generalEvent = storyTransform.GetComponent<GeneralEvent>();
			if(generalEvent != null)
			{
				foreach(RandomEventController randomEventController in container2.currentEditorBaseItem.storyTransform.GetComponentsInChildren<RandomEventController>())
					randomEventController.randomEventList.Remove(generalEvent);

			}
			ReAttachRandomEventControllerLinks(storyTransform);
			//Destroy(storyTransform.gameObject);
			DestroyImmediate(storyTransform.gameObject);
			ContainerControllerBase.currentContainerController.currentEditorBaseItem = null;
			//ContainerController.current.Draw(); 
			EditorEventManager.TriggerEvent("RemoveItem_" + ContainerControllerBase.currentContainerController.name);
		}
	}

	void ReAttachRandomEventControllerLinks(Transform storyTransform)
	{
		foreach(GeneralEvent generalEvent in storyTransform.GetComponentsInChildren<GeneralEvent>())
		{
			RandomEventController randomEventController = GetRootRandomEventControllerLink(container2.currentEditorBaseItem.storyTransform, generalEvent);
			if(randomEventController != null)
			{
				//print(randomEventController.name);
				generalEvent.transform.SetParent(randomEventController.transform,false);
			}
		}
	}

	RandomEventController GetRootRandomEventControllerLink(Transform storyTransform, GeneralEvent generalEvent)
	{
		if(storyTransform == generalEvent.transform)
			return null;
		
		RandomEventController randomEventController = storyTransform.GetComponent<RandomEventController>();
		if(randomEventController != null)
		{
			if(randomEventController.randomEventList.Exists(x => x == generalEvent))
			{
				//print(randomEventController.name);
				return randomEventController;
			}
		}

		foreach(Transform transform in storyTransform)
		{
			RandomEventController foundRandomEventController = GetRootRandomEventControllerLink(transform, generalEvent);
			if(foundRandomEventController != null)
				return foundRandomEventController;
		}

		return null;
	}

	public string GetMaxCharacterName()
	{
		string[] files = Directory.GetFiles(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), resourcePath), resourceStoryActionsPath));
		Array.Sort(files);
		string maxName = Path.GetFileNameWithoutExtension(files[files.Length-2]);
		int number = (int)Convert.ChangeType((maxName[maxName.Length-1]).ToString(), typeof(int));
		return "character" + ++number;
	}

	void SetEditorBaseItem(GameObject go)
	{
		//currentEditorBaseItem = go.GetComponent<EditorBaseItem>();
		DeActivateButton();
	}

	public void SetButtonFilter1()
	{
		DeActivateButton();
		bttn_create_action.SetActive(true);
	}

	public void SetButtonFilter2()
	{
		DeActivateButton();
		if(container3.IsEmpty())
		{
			bttn_create_event.SetActive(true);
			bttn_create_outcom_event.SetActive(true);
		}
		bttn_delete.SetActive(true);
	}

	public void SetButtonFilter3() // eventPanel
	{
		DeActivateButton();
		bttn_create_event.SetActive(true);
		bttn_create_outcom_event.SetActive(true);
		bttn_create_outcom.SetActive(true);
		bttn_delete.SetActive(true);
	}

	public void SetButtonFilter4() // eventAction
	{
		DeActivateButton();
		bttn_create_action.SetActive(true);
		bttn_create_continue_action.SetActive(true);
		bttn_delete.SetActive(true);
	}

	void DeActivateButton()
	{
		bttn_create_event.SetActive(false);
		bttn_create_outcom_event.SetActive(false);
		bttn_create_action.SetActive(false);
		bttn_create_outcom.SetActive(false);
		bttn_create_continue_action.SetActive(false);
		bttn_delete.SetActive(false);
		bttn_cut.SetActive(false);
	}

	void SetActionPrefabName(Action action)
	{
		action.name = actionName + ContainerControllerBase.currentContainerController.countItems;
		action.text_id = GameStrings.Instance.GetNextKey();
		string key = action.text_id.ToString();
		action.text = key;
		GameStrings.Instance.SetString(key, key);
	}

	void SetPrefabName(GameObject go)
	{
		Action action = go.GetComponent<Action>();
		if(action != null)
			SetActionPrefabName(action);
	}

	void SetEventPrefabName(GameObject go)
	{
		go.name = eventName + container3.EventPanelsCount();

		GeneralEvent generalEvent = go.GetComponent<GeneralEvent>();
		generalEvent.description_id = GameStrings.Instance.GetNextKey();
		string key = generalEvent.description_id.ToString();
		generalEvent.description = key;
		GameStrings.Instance.SetString(key, key);

		Action action = go.GetFirstComponentInChildren<Action>();
		if(action != null)
			SetActionPrefabName(action);
		else
			SetOutcomePrefabName(go.GetFirstComponentInChildren<Outcome>());
	}

	void SetOutcomePrefabName(Outcome outcome)
	{
		outcome.name = outcomeName + ContainerControllerBase.currentContainerController.countItems;
		outcome.description_id = GameStrings.Instance.GetNextKey();
		string key = outcome.description_id.ToString();
		outcome.description = key;
		GameStrings.Instance.SetString(key, key);
	}
}
#endif
