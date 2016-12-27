using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ObjectsExtensionMethods;

public class DragObject : DragObjectBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public void OnBeginDrag(PointerEventData eventData)
	{
		SendMessage("OnStartDrag", eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		SendMessage("OnDragged", eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		SendMessage("OnDragFinish", eventData);
	}
}
