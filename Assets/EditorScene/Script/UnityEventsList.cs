using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;


public class ObjectUnityEvent<T> : UnityEvent<T>
{
	public ObjectUnityEvent(UnityAction<T> listener) 
	{
		AddListener(listener);
	}
}

public class ObjectUnityEvent : UnityEvent
{
	public ObjectUnityEvent(UnityAction listener)
	{
		AddListener(listener);
	}
}

public class UnityEventsList
{
	List<UnityEventBase> UnityEvents;

	public UnityEventsList()
	{
		UnityEvents = new List<UnityEventBase>();
	}

	public UnityEventsList(UnityEventBase obj)
	{
		UnityEvents = new List<UnityEventBase>();
		UnityEvents.Add(obj);
	}

	public UnityEventBase GetEventType(Type t)
	{
		return UnityEvents.Find(x => x.GetType().Equals(t));
	}

	public void AddEvent(UnityAction listener)
	{
		UnityEventBase unityEventBase = GetEventType(typeof(ObjectUnityEvent));
		if(unityEventBase != null)
			(unityEventBase as ObjectUnityEvent).AddListener(listener);
		else
			UnityEvents.Add(new ObjectUnityEvent(listener));
	}

	public void AddEvent<T>(UnityAction<T> listener)
	{
		UnityEventBase unityEventBase = GetEventType(typeof(ObjectUnityEvent<T>));
		if(unityEventBase != null)
			(unityEventBase as ObjectUnityEvent<T>).AddListener(listener);
		else
			UnityEvents.Add(new ObjectUnityEvent<T>(listener));
	}

	public void RemoveEvent(UnityAction listener)
	{
		UnityEventBase unityEventBase = GetEventType(typeof(ObjectUnityEvent));
		if(unityEventBase != null)
			(unityEventBase as ObjectUnityEvent).RemoveListener(listener);
	}

	public void RemoveEvent<T>(UnityAction<T> listener)
	{
		UnityEventBase unityEventBase = GetEventType(typeof(ObjectUnityEvent<T>));
		if(unityEventBase != null)
			(unityEventBase as ObjectUnityEvent<T>).RemoveListener(listener);
	}

	public void ExecuteEvents()
	{
		foreach(UnityEventBase unityEventBase in UnityEvents)
		{
			if(unityEventBase.GetType().Equals(typeof(ObjectUnityEvent)))
				(unityEventBase as ObjectUnityEvent).Invoke();
		}
	}

	public void ExecuteEvents<T>(T go)
	{
		foreach(UnityEventBase unityEventBase in UnityEvents)
		{
			if(unityEventBase.GetType().Equals(typeof(ObjectUnityEvent)))
				(unityEventBase as ObjectUnityEvent).Invoke();
			else
				(unityEventBase as ObjectUnityEvent<T>).Invoke(go);
		}
	}
}