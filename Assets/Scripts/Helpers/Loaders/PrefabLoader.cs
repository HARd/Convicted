﻿/*
	Создает обьект из указанного префаба.
*/


using UnityEngine;
using System.Collections;

public class PrefabLoader : MonoBehaviour
{
	[SerializeField]
	bool whenOnStart = false;

	[SerializeField]
	GameObject prefab;

	[SerializeField]
	bool zeroPosition = true;

	[SerializeField]
	Transform parent;

	[SerializeField]
	protected bool worldPositionStays = false;

	[SerializeField]
	string resourcePrefabPath;

	public GameObject InstantiatedPrefab { get; private set; }

	void Start()
	{
		if(whenOnStart)
			DoAction();
	}

	protected virtual void DoAction()
	{
		GameObject prefabToLoad = prefab != null ? prefab : Resources.Load(resourcePrefabPath) as GameObject;

		if (zeroPosition)
			InstantiatedPrefab = Instantiate(prefabToLoad, Vector3.zero, Quaternion.identity) as GameObject;
		else
			InstantiatedPrefab = Instantiate(prefabToLoad) as GameObject;

		if(parent != null)
			InstantiatedPrefab.transform.SetParent(parent, worldPositionStays);
	}
}
