﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MerchantScreen : ScreenBase 
{
	[SerializeField]
	Text MerchantTitleText;	

	[SerializeField]
	Text MerchantCashDisplay;

	[SerializeField]
	MerchantItemController[] MerchantItem;

	const string item_key = "ITEM_OFFERS";
	const string item_purchased = "ITEM_PURCHASED";
	List<int> ItemOfferList = new List<int>();
	List<int> ItemPurchased = new List<int>();

	void Start() 
	{
		AudioManager.Instance.Play(1);
		MerchantTitleText.text = Localization.Instance.GetLocale(948);
		if(SaveLoadXML.HasKey(item_key))
			ItemOfferList =  new List<int>(SaveLoadXML.GetIntArrayValue(item_key));
		if(SaveLoadXML.HasKey(item_purchased))
			ItemPurchased =  new List<int>(SaveLoadXML.GetIntArrayValue(item_purchased));
		ShowMerchantPanel();
	}

	public int GetRandomItemIndex()
	{
		int index = PlayerInfo.Instance.GetRandomItemIndex(item => item.cost > 0);
		if(ItemOfferList.Exists(i => i == index))
			index = GetRandomItemIndex();

		return index;
	}

	public void ShowMerchantPanel() 
	{
		if(ItemOfferList.Count == 0) 
		{
			for(int i = 0; i < MerchantItem.Length; i++) 
			{
//				int x = 0;
//				bool stop = false;
//				while(!stop) 
//				{
//					x = Mathf.FloorToInt(Random.Range(0,PlayerInfo.Instance.inventory.Count-1));
//					bool match = false;
//					foreach(int item_check in ItemOfferList) 
//					{
//						if(PlayerInfo.Instance.inventory[item_check].name == PlayerInfo.Instance.inventory[x].name) 
//						{
//							match = true;
//							break;
//						}  
//					}
//					if(!match && PlayerInfo.Instance.inventory[x].cost > 0) 
//						stop = true;
//				}
//				Item item = PlayerInfo.Instance.GetRandomItemIndex(it => it.cost > 0);

				ItemOfferList.Add(GetRandomItemIndex());
			}
			SaveLoadXML.SetValue(item_key, ItemOfferList.ToArray());
		} 
		DrawItemOffers();
	}

	public void DrawItemOffers()
	{
		for(int i = 0;i < MerchantItem.Length;i++) 
		{
			MerchantItem[i].DrawItem(ItemOfferList[i], HasPurchased(ItemOfferList[i]));
		}
		MerchantCashDisplay.text = Mathf.FloorToInt(PlayerInfo.Instance.cash).ToString();
	}

	public bool HasPurchased(int id)
	{
		return ItemPurchased.Exists(i => i == id);
	}

	public void ClearItemOffer() 
	{
		ItemOfferList.Clear();
	}

	public void OnClickBuyItem(MerchantItemController obj) 
	{
		AudioManager.Instance.Play(0);
		if(obj.ItemName != "no_cash") 
		{
			//print("--obj.ItemName  " + obj.ItemName);
			PlayerInfo.Instance.EquipItem(obj.ItemName,true,true);
			ItemPurchased.Add(obj.ID);
			SaveLoadXML.SetValue(item_purchased, ItemPurchased.ToArray());
			DrawItemOffers();
			AudioManager.Instance.Play(3);
		} 
		else
		{
			ScreenManager.Instance.CreateScreen("HintPanel");
			ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(Localization.Instance.GetLocale(894));
		}
	}

	public void OnClickButtonReturn() 
	{
		AudioManager.Instance.Play(1);
		PanelManager.Instance.RefreshCurrentPanel();
		Destroy(gameObject);
	}
}