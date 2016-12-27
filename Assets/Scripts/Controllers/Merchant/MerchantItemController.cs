using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MerchantItemController : MonoBehaviour 
{
	[SerializeField]
	Text ItemOffer;	

	[SerializeField]
	Image ItemImage;

	[SerializeField]
	GameObject PurchaseButton;

	[SerializeField]
	Text PurchaseButtonText;	

	public string ItemName;
	public int ID;

	public void DrawItem(int id, bool HasPurchased)
	{
		ID = id;
		ItemImage.sprite = SpriteManager.Instatce.GetSprite("Tools/" + PlayerInfo.Instance.inventory[id].name + "/inv");
		PurchaseButtonText.text = PlayerInfo.Instance.inventory[id].cost.ToString();
		PurchaseButton.GetComponent<Button>().interactable = true;

		ItemOffer.text = Localization.Instance.GetLocale(PlayerInfo.Instance.inventory[id].text_id);
//		if(PlayerInfo.Instance.inventory[id].text_id != 0) 
//			ItemOffer.text = Localization.Instance.GetLocale(PlayerInfo.Instance.inventory[id].text_id);
//		else 
//			ItemOffer.text = PlayerInfo.Instance.inventory[id].text;

		if(HasPurchased) 
		{
			ItemOffer.text = Localization.Instance.GetLocale(73);
			PurchaseButton.GetComponent<Button>().interactable = false;
		} 
		else 
		{
			if(PlayerInfo.Instance.cash >= PlayerInfo.Instance.inventory[id].cost) 
				ItemName = PlayerInfo.Instance.inventory[id].name;
			else 
				ItemName = "no_cash";
		}
	}

	public GameObject DuplicateItemImage() 
	{
		return Instantiate(ItemImage.gameObject, ItemImage.transform.parent) as GameObject;
	}
}
