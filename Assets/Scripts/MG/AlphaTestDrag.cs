using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using ObjectsExtensionMethods;

public class AlphaTestDrag : AlphaTest, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	DragObjectBase dragObject;

	protected override void Start () 
	{
		base.Start();
		dragObject = GetComponent<DragObjectBase>();
	}


	public void OnBeginDrag(PointerEventData eventData)
	{

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
		if(eventData.pointerEnter != null)
		{
			AlphaTest alphaTest = eventData.pointerEnter.GetComponent<AlphaTest>();

			if(alphaTest != null && !alphaTest.IsAlphaPoint(eventData))
				eventData.pointerEnter = alphaTest.GetNextObjectTestAlpha(eventData);
		}

		SendMessage("OnDragFinish", eventData, SendMessageOptions.DontRequireReceiver);
	}
}
