using UnityEngine;
using System;
using System.Collections;

public class EditorEventWatcher : MonoBehaviour 
{
	[SerializeField]
	string eventName;

	[SerializeField]
	string messageName = "Execute";

	[SerializeField]
	enumPanel type;

	enum enumPanel{None, Transform, GameObject};

	void OnEnable ()
	{
		switch (type) 
		{
		case enumPanel.None:
			EditorEventManager.StartListening(eventName, Send);
			break;
		case enumPanel.Transform:
			EditorEventManager.StartListening<Transform>(eventName, Send);
			break;
		case enumPanel.GameObject:
			EditorEventManager.StartListening<GameObject>(eventName, Send);
			break;
		}
	}

	void OnDisable ()
	{
		switch (type) 
		{
		case enumPanel.None:
			EditorEventManager.StopListening(eventName, Send);
			break;
		case enumPanel.Transform:
			EditorEventManager.StopListening<Transform>(eventName, Send);
			break;
		case enumPanel.GameObject:
			EditorEventManager.StopListening<GameObject>(eventName, Send);
			break;
		}
	}

	void Send()
	{
		//print(name);
		SendMessage(messageName, SendMessageOptions.DontRequireReceiver);
	}

	void Send<T>(T obj)
	{
		//print(obj);
		SendMessage(messageName, obj, SendMessageOptions.DontRequireReceiver);
	}
}
