using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ObjectsExtensionMethods;

public class ItemsBox : MonoBehaviour 
{
	[SerializeField]
	GameObject itemPrefab;

	[SerializeField]
	GameObject inputItemScreenPrefab;

	ItemsBoxItem currentItem;
//	VerticalLayoutGroup verticalLayoutGroup;
//	ContentSizeFitter contentSizeFitter;

//	void Start() 
//	{
//		verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
//		contentSizeFitter = GetComponent<ContentSizeFitter>();
//	}

	void OnItemClick(ItemsBoxItem item)
	{
		BroadcastMessage("OnClearItem");
		currentItem = item;
	}

	public void DestroyCurrentItem()
	{
		BroadcastMessage("OnClearItem");
		if(currentItem != null)
		{
			if(transform.childCount > 3)
				Destroy(currentItem.gameObject);
			else
			{
				ItemsBoxItem item = this.GetFirstComponentInChildren<ItemsBoxItem>();
				if(item.itemText.text != "") 
					item.itemText.text = "";
			}
		}

	}

	public void AddItem(string nameItem)
	{
		GameObject go;  
		if(transform.childCount == 3)
		{
			ItemsBoxItem item = this.GetFirstComponentInChildren<ItemsBoxItem>();
			if(item.itemText.text == "") 
				go = item.gameObject;
			else
				go = Instantiate(itemPrefab, transform, false) as GameObject;
		}
		else
			go = Instantiate(itemPrefab, transform, false) as GameObject;
		
		go.name = nameItem;
		go.GetComponent<ItemsBoxItem>().itemText.text = nameItem;
	}

	public void OnButtonAddClick()
	{
		GameObject go = Instantiate(inputItemScreenPrefab, transform.parent.transform, false) as GameObject;
		go.GetComponent<InputItemScreen>().onSelect = AddItem;
	}
}
