﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
	private static ScreenManager _instance;
	public static ScreenManager Instance { get { return _instance ?? (_instance = FindObjectOfType<ScreenManager>()); } }
	protected ScreenManager() { _instance = null; }

	public GameObject current;

	const string resourceScreenPath = "Screen/";

	public GameObject CreateScreen(string screenName, bool destroyCurrent = false)
	{
		if(destroyCurrent && current != null)
			Destroy(current);
		
		GameObject prefabToLoad = Resources.Load(resourceScreenPath + screenName, typeof(GameObject)) as GameObject;
		current = Instantiate(prefabToLoad) as GameObject;
		current.transform.SetParent(transform, false);
		return current;
	}

	public GameObject GetScreen(string screenName)
	{
		if(current && current.name == screenName)
			return current;

		Transform trans = transform.FindChild(screenName);
		return (trans != null) ? trans.gameObject : null;
	}
}
