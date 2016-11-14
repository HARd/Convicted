/*
	Инвентарь.
	Сохраняет и восстанавливает тулзы.
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
//using GameLogic;

public class Inventory : MonoBehaviour
{
	private static Inventory _instance;
	public static Inventory Instance { get { return _instance ?? (_instance = FindObjectOfType<Inventory>()); } }

	[SerializeField]
	Tool defaultTool;

	[SerializeField]
	ToolHolder holderPrefab;

	[SerializeField]
	GameObject organizer;

	[SerializeField]
	string saveToolsKey = "INVENTORY_TOOLS";

	public event EventHandler ToolDragStateChanged;

	public List<string> ItemHiddenList = new List<string>();

	public virtual void OnToolDragStateChanged()
	{
		if (ToolDragStateChanged != null)
			ToolDragStateChanged(this, EventArgs.Empty);
	}

	void Start()
	{
		//print("--PlayerInfo.Instance.day = " + PlayerInfo.Instance.day);
		//print("--WorldTime.Instance.current_time = " + WorldTime.Instance.current_time);
		RestoreTools();
		if(PlayerInfo.Instance.day == 0) // && WorldTime.Instance.current_time == 360
		{
			AddTool("no_escape_plan");
			AddTool("no_access_sewers");
			AddTool("canteen_access");
			AddTool("work_access");
		}
	}

	void RestoreTools()
	{
		foreach (string toolName in SaveLoadXML.GetArrayValue(saveToolsKey))
			AddTool(toolName);
	}

	public List<Tool> GetTools()
	{
		List<Tool> tools = new List<Tool>();
		foreach (Transform child in organizer.transform)
		{
			ToolHolder holder = child.GetComponent<ToolHolder>();
			if (holder != null && !holder.IsRemoving)
				tools.Add(holder.Tool);
		}

		return tools;
	}

	public List<string> GetToolNames()
	{
		//return GetTools().Select(t => t.Name).ToList();
		List<string> toolsName = new List<string>();
		foreach (Transform child in organizer.transform)
		{
			ToolHolder holder = child.GetComponent<ToolHolder>();
			if (holder != null && !holder.IsRemoving)
				for (int i = 0; i < holder.toolCounter.Counter; i++)
					toolsName.Add(holder.Tool.Name);
		}
		return toolsName;
	}

	public List<string> GetToolHiddenNames()
	{
		return ItemHiddenList;
	}

	public List<string> GetToolAllNames()
	{
		List<string> toolsName = GetToolNames();
		toolsName.AddRange(ItemHiddenList);
		return toolsName;
	}

	public float GetMaxDamage()
	{
		List<string> toolNames = GetToolNames();

		return (toolNames.Count != 0) ? toolNames.Max(str => PlayerInfo.Instance.GetItem(str).damage) : 0f;
	}

	public string GetRandomToolName()
	{
		Item item = PlayerInfo.Instance.GetRandomItem(i => i.contraband && HasTool(i.name));
		return (item != null) ? item.name : null;
	}

	public void SaveToolList()
	{
		SaveLoadXML.SetValue(saveToolsKey, GetToolAllNames().ToArray());
	}
		
	public ToolHolder CreateHolder()
	{
		ToolHolder toolHolder = UnityEngine.Object.Instantiate(holderPrefab) as ToolHolder;
		toolHolder.transform.SetParent(organizer.transform, false);

		return toolHolder;
	}

	public int GetHolderPosition(ToolHolder holder)
	{
		for (int i = 0; i < organizer.transform.childCount; i++)
		{
			if (organizer.transform.GetChild(i) == holder.transform)
				return i;
		}
		return -1;
	}

	public int GetHolderCount()
	{
		return organizer.transform.childCount;
	}

	public ToolHolder GetHolderForTool(Tool tool, bool createIfNotExist = true, bool scrollToWindow = true)
	{
		ToolHolder holder = FindToolHolder(tool.Name);
		if (holder == null)
		{
			if (createIfNotExist)
				holder = CreateHolder();
			else
				return null;
		}

		return holder;
	}

	public bool IsMoving { get { return GetComponent<PanelElementMover>().IsMoving; } }

	public bool IsVisible { get { return GetComponent<PanelElementMover>().IsVisible; } }

	public Tool AddTool(string toolName)
	{
		if(ItemHiddenList.Exists(t => t==toolName))
		{
			Debug.LogError("Don`t AddTool - " + toolName);
			return null;
		}
			
		Debug.Log("AddTool - " + toolName);
		Tool tool = null;
		//bool hasWeapon = PlayerInfo.Instance.HasWeapon(toolName);
		//if(hasWeapon || (!hasWeapon && !PlayerInfo.Instance.GetItem(toolName).hidden))
		if(PlayerInfo.Instance.HasItem(toolName))
		{
			ToolHolder toolHolder = FindToolHolder(toolName);
			if(toolHolder)
			{
				toolHolder.AddCounter();
			}
			else
			{
				tool = CreateTool(toolName);
				AddTool(tool);
			}
		}
		else
		{
			if(PlayerInfo.Instance.HasHidden(toolName))
				ItemHiddenList.Add(toolName);
			else
				Debug.LogError("Not hidden tool - " + toolName);
		}
		SaveToolList();
		return tool;
	}

	public void AddTool(Tool tool)
	{
		tool.AttachToHolder(CreateHolder());
	}

	public Tool CreateTool(string toolName)
	{
		Tool tool = Instantiate(defaultTool) as Tool;
		tool.Name = toolName;

		return tool;
	}

	public void DestroyTool(string toolName)
	{
		print("--DestroyTool " + toolName);
		ToolHolder holder = FindToolHolder(toolName);
		if (holder && holder.Tool)
		{
			if(holder.Counter > 1)
			{
				holder.LessCounter();
			}
			else
			{
				//holder.Tool.Destroy();
				holder.Remove();
			}
		}
		else
		{
			//print("--DestroyItemHiddenList " + toolName);
			ItemHiddenList.Remove(toolName);
		}
		SaveToolList();
	}

	public void DestroyAllTool()
	{
		foreach (Transform child in organizer.transform)
		{
			ToolHolder holder = child.GetComponent<ToolHolder>();
			if (holder)
			{
				if (holder.Tool)
					holder.Tool.Destroy();

				holder.Remove();
			}
		}
		SaveToolList();
	}
	public void DestroyAllToolHidden()
	{
		ItemHiddenList.Clear();
		SaveToolList();
	}

	public ToolHolder FindToolHolder(string name)
	{
		foreach (Transform child in organizer.transform)
		{
			ToolHolder holder = child.GetComponent<ToolHolder>();
			if (holder != null && holder.Tool.Name == name)
				return holder;
		}
		return null;
	}

	public Tool FindTool(string name)
	{
		ToolHolder holder = FindToolHolder(name);
		if (holder && !holder.IsRemoving)
			return holder.Tool;
		else
			return null;
	}

	public bool HasTool(string name)
	{
		return (FindTool(name) != null) || ItemHiddenList.Exists(tool => tool == name);
	}

	public bool HasTools(List<string> names)
	{
		bool hasTools = true;
		foreach(string name in names)
		{
			if(!HasTool(name)) 
			{
				hasTools = false;
				break;
			}
		}
		return hasTools;
	}

	void OnDestroy()
	{
		if (_instance == this)
			_instance = null;
	}
}
