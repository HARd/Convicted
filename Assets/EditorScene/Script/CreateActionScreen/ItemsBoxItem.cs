using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemsBoxItem : MonoBehaviour, IPointerClickHandler
{
	public Text itemText;

	Image image;
	Color selectColor = new Color(1f, 0.9f, 0.5f, 1f);
	Color startColor;


	void Start() 
	{
		image = GetComponent<Image>();
		startColor = image.color;
	}

	public virtual void OnPointerClick (PointerEventData eventData)
	{
		if(itemText.text != "")
		{
			SendMessageUpwards("OnItemClick", this);
			image.color = selectColor;
		}
	}
	void OnClearItem()
	{
		image.color = startColor;
	}
}
