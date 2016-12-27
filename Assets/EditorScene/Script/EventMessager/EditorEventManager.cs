using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class EditorEventManager : MonoBehaviour 
{
	private Dictionary <string, UnityEventsList> eventDictionary = new Dictionary<string, UnityEventsList>();

	private static EditorEventManager editorEventManager;

	public static EditorEventManager Instance
	{
		get
		{
			if (!editorEventManager)
			{
				editorEventManager = FindObjectOfType (typeof (EditorEventManager)) as EditorEventManager;

				if (!editorEventManager)
					Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
			}
			return editorEventManager;
		}
	}

	public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEventsList eventList = null;
		if (Instance.eventDictionary.TryGetValue (eventName, out eventList))
			eventList.AddEvent(listener);
		else
		{
			eventList = new UnityEventsList(new ObjectUnityEvent(listener));
			Instance.eventDictionary.Add(eventName, eventList);
		}
	}

	public static void StartListening<T>(string eventName, UnityAction<T> listener)
	{
		UnityEventsList eventList = null;
		if (Instance.eventDictionary.TryGetValue (eventName, out eventList))
			eventList.AddEvent(listener);
		else
		{
			eventList = new UnityEventsList(new ObjectUnityEvent<T>(listener));
			Instance.eventDictionary.Add(eventName, eventList);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if (editorEventManager == null) 
			return;

		UnityEventsList eventList = null;
		if (Instance.eventDictionary.TryGetValue (eventName, out eventList))
			eventList.RemoveEvent(listener);
	}

	public static void StopListening<T>(string eventName, UnityAction<T> listener)
	{
		if (editorEventManager == null) 
			return;

		UnityEventsList eventList = null;
		if (Instance.eventDictionary.TryGetValue (eventName, out eventList))
			eventList.RemoveEvent(listener);
	}


	public static void TriggerEvent(string eventName)
	{
		UnityEventsList eventList = null;
		if (Instance.eventDictionary.TryGetValue (eventName, out eventList))
			eventList.ExecuteEvents();
	}

	public static void TriggerEvent<T>(string eventName, T go)
	{
		UnityEventsList eventList = null;
		if (Instance.eventDictionary.TryGetValue (eventName, out eventList))
			eventList.ExecuteEvents(go);
	}
}