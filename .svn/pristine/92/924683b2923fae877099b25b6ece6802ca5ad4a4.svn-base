using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using ObjectsExtensionMethods;

public class EditorChangeItem : EditorBaseItem
{
	public override void OnPointerClick (PointerEventData eventData)
	{
		GeneralEvent generalEvent = storyTransform.GetComponent<GeneralEvent>();
		if(generalEvent != null)
			SendMessageUpwards("DrawPreview", generalEvent);
		else
			SendMessageUpwards("DrawPreview", storyTransform.GetComponent<Outcome>());
		
		SendMessageUpwards("DestroyChangeEventPanel");
	}
}
