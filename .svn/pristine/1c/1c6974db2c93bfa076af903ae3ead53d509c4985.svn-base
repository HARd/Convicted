using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolScreen : MonoBehaviour, IPointerClickHandler
{
//	[SerializeField]
//	string invSpriteFormatString = "Tools/{0}/inv";
//
	[SerializeField]
	Image image;

	[SerializeField]
	Text text;
//
//	Sprite invSprite;
	void Start()
	{
		transform.SetParent(Inventory.Instance.transform.parent.transform, false);
	}

	public void SetScreenView(Sprite sprite, string text)
	{
		image.sprite = sprite;
		this.text.text = text;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		Destroy(gameObject);
	}
}
