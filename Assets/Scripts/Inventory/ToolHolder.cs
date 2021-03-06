﻿/*
	Родительский обьект тулзы в инвентаре.
	(инвентарь содержит органайзер, занимающийся физическим размещением ToolHolderов.
	ToolHolderы содержат тулзы, обладающие собственным поведением)
	Автор: Кущ А.Е.
*/


using UnityEngine;

public class ToolHolder : MonoBehaviour
{
	public ToolCounter toolCounter;

	public RectTransform toolAncher;

	Tool _tool;
	public Tool Tool
	{
		get { return _tool; }
		set { _tool = value; }//Inventory.Instance.SaveToolList(); }
	}

	public int Counter { get { return  (Tool != null) ? toolCounter.Counter : 0; } }

	public bool IsRemoving { get; private set; }

	public void Remove()
	{
		IsRemoving = true;
		//Destroy(gameObject);
		DestroyImmediate(gameObject);
		//Inventory.Instance.SaveToolList();
	}

	public void AddCounter()
	{
		toolCounter.Add();
		//Inventory.Instance.SaveToolList();
	}

	public void LessCounter()
	{
		toolCounter.Less();
		//Inventory.Instance.SaveToolList();
	}
}
