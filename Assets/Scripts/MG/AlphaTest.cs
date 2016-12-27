using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using ObjectsExtensionMethods;

public class AlphaTest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	bool testOnClick = true;
	[SerializeField]
	bool testOnFocusIn;
	[SerializeField]
	bool testOnFocusOut;
	[SerializeField]
	bool testOnDrag;

	Image image;
	RectTransform rectTransform;
	Canvas canvas;

	protected virtual void Start () 
	{
		image = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
		canvas = transform.GetParentCanvas();
	}

	public bool IsAlphaPoint(PointerEventData eventData)
	{
		Vector2 localCursor;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localCursor);
		Vector2 pos = RectTransformUtility.PixelAdjustPoint(localCursor, rectTransform, canvas);

		int x = (int)(pos.x + image.sprite.texture.width*rectTransform.pivot.x);
		int y = (int)(pos.y + image.sprite.texture.height*rectTransform.pivot.y);

		return image.sprite.texture.GetPixel(x, y).a > 0;
	}

	public GameObject GetNextObjectTestAlpha(PointerEventData eventData)
	{
		List<RaycastResult> raycastResults = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, raycastResults);
		RaycastResult rr = raycastResults.Find((RaycastResult x) => 
			{
				if(x.gameObject == gameObject)
					return false;

				AlphaTest alphaTest = x.gameObject.GetComponent<AlphaTest>();
				if(alphaTest == null)
					return true;
				else
					return alphaTest.IsAlphaPoint(eventData);
			});
		print(rr.gameObject.name);
		return rr.gameObject;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(!testOnClick)
			return;
		
		if(IsAlphaPoint(eventData))
			SendMessage("OnPointerClickAlphaTest", eventData, SendMessageOptions.DontRequireReceiver);
		else
			GetNextObjectTestAlpha(eventData).SendMessage("OnPointerClick", eventData, SendMessageOptions.DontRequireReceiver);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(!testOnFocusIn)
			return;
		
		if(IsAlphaPoint(eventData))
		{
			SendMessage("OnPointerEnterAlphaTest", eventData, SendMessageOptions.DontRequireReceiver);
		}
		else
			GetNextObjectTestAlpha(eventData).SendMessage("OnPointerEnter", eventData, SendMessageOptions.DontRequireReceiver);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if(!testOnFocusOut)
			return;
		
		if(IsAlphaPoint(eventData))
			SendMessage("OnPointerExitAlphaTest", eventData, SendMessageOptions.DontRequireReceiver);
		else
			GetNextObjectTestAlpha(eventData).SendMessage("OnPointerExit", eventData, SendMessageOptions.DontRequireReceiver);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!testOnDrag)
			return;
		
		if(!IsAlphaPoint(eventData))
			eventData.pointerDrag = GetNextObjectTestAlpha(eventData);
		
		SendMessage("OnStartDrag", eventData, SendMessageOptions.DontRequireReceiver);
	}

	public void OnDrag(PointerEventData eventData)
	{
		SendMessage("OnDragged", eventData, SendMessageOptions.DontRequireReceiver);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(!testOnDrag)
			return;
		
		if(eventData.pointerEnter != null)
		{
			AlphaTest alphaTest = eventData.pointerEnter.GetComponent<AlphaTest>();

			if(alphaTest != null && !alphaTest.IsAlphaPoint(eventData))
				eventData.pointerEnter = alphaTest.GetNextObjectTestAlpha(eventData);
		}

		SendMessage("OnDragFinish", eventData, SendMessageOptions.DontRequireReceiver);
	}
}
