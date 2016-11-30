using UnityEngine;
using UnityEngine.UI;
using ObjectsExtensionMethods;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class EditorEventActionController : EventActionController
{

	public override void OnPointerClick (PointerEventData eventData)
	{

		if(action.type == "continue")
			return;
		
		List<Outcome> outcomList = action.GetChildrenComponents<Outcome>();
		RandomEventController randomEventController = action.GetComponent<RandomEventController>();
		if(randomEventController != null)
		{
			if(randomEventController.Count == 0 && outcomList.Count == 0)
				return;
			
			if(randomEventController.Count == 1 && outcomList.Count == 0)
				SendMessageUpwards("DrawPreview", randomEventController.randomEventList[0]);
			else if(randomEventController.Count == 0 && outcomList.Count == 1)
				SendMessageUpwards("DrawPreview",outcomList[0]);
			else
			{
				SendMessageUpwards("DrawPreview", randomEventController.randomEventList);
				SendMessageUpwards("AddDrawPreview", outcomList);
			}
		}
		else
		{
			if(outcomList.Count > 1)
				SendMessageUpwards("DrawPreview", outcomList);
			else if(outcomList.Count == 1)
				SendMessageUpwards("DrawPreview",outcomList[0]);
		}
	}

	protected override bool DrawOutcomeChance()
	{
		return true;
	}
}
