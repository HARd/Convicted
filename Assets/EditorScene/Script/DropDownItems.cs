using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DropDownItems : MonoBehaviour 
{
	[SerializeField]
	bool isHidden = false;

	Dropdown dropdown;

	void Start() 
	{
		dropdown = GetComponent<Dropdown>();
		List<Dropdown.OptionData> dataList = new List<Dropdown.OptionData>();


		if(!isHidden)
		{
			dataList.Add(new Dropdown.OptionData(" AddTool"));
			List<Item> itemList = new List<Item>(PlayerInfo.Instance.GetItems());
			itemList.Sort((x, y) => x.name.CompareTo(y.name));
			foreach(Item item in itemList)
				dataList.Add(new Dropdown.OptionData(item.name));
		}
		else
		{
			dataList.Add(new Dropdown.OptionData(" AddHidden"));
			List<string> itemList = new List<string>(PlayerInfo.Instance.hidden);
			itemList.Sort();
			foreach(string item in itemList)
				dataList.Add(new Dropdown.OptionData(item));
		}
		dropdown.options = dataList;
	}
		
	public void OnValueChanged(int value) 
	{
		SendMessageUpwards("OnChangeItem", dropdown.options[dropdown.value].text);
	}
}
