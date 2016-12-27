using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


public class InputItemScreen : MonoBehaviour 
{
	public Action<string> onSelect;

	public void OnChangeItem(string nameItem) 
	{
		onSelect(nameItem);
		Destroy(gameObject);
	}

	public void OnEndEdit(InputField input)
	{
		OnChangeItem(input.text);
	}
}
