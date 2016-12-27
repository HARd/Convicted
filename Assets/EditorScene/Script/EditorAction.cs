using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using ObjectsExtensionMethods;

public class EditorAction : EditorBaseItem, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Image arrow_out;
	public Transform line_out;
	public Transform line_event;
	public int level_line = 0;
	public GameObject dragObjectPrefab;

	bool isDrag = false;
	GameObject dragObject;

	public override void OnPointerClick (PointerEventData eventData)
	{
		if(!IsSelected())
		{
			base.OnPointerClick(eventData);
			SendMessageUpwards("OnEditorBaseItemClick", this);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if(IsSelected())
		{
			isDrag = true;
			dragObject = Instantiate(dragObjectPrefab, transform, false) as GameObject;
		}
	}

	public void OnDrag(PointerEventData data)
	{
		if(isDrag)
		{
			//print("OnDrag");
			Vector3 globalMousePos;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(dragObject.GetComponent<RectTransform>(), data.position, data.pressEventCamera, out globalMousePos))
			{
				Vector3 pos = dragObject.transform.position;
				pos.y = globalMousePos.y;
				dragObject.transform.position = pos;
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(isDrag)
		{
			isDrag = false;
			EventPanel eventPanel = eventData.pointerEnter.GetComponent<EventPanel>();
			if(eventPanel != null)
			{
				RandomEventController randomEventController = storyTransform.GetComponent<RandomEventController>();
				if(randomEventController == null)
					randomEventController = storyTransform.gameObject.AddComponent<RandomEventController>();

				randomEventController.randomEventList.Add(eventPanel.generalEvent);
				//ContainerController.current.Draw();
				EditorEventManager.TriggerEvent("Draw_" + ContainerControllerBase.currentContainerController.name);
			}
		}
	}
}
