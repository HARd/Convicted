using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ObjectsExtensionMethods;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EventPanel : ContainerControllerBase,  IPointerClickHandler
{
	[SerializeField]
	protected Text textName;


	public GeneralEvent generalEvent;

	public override void Draw(Transform  trans)
	{
		textName.text = name;
		foreach(Transform transform in trans)
		{
			GeneralEvent generalEvent = transform.GetComponent<GeneralEvent>();
			if(generalEvent == null)
			{
				EditorAction eItem = (EditorBaseItem.CreatEditorChangeItem(transform.gameObject, actionPrefab, this.transform, transform.name) as EditorAction);

				Action action = transform.GetComponent<Action>();
				if(action != null)
				{
					eItem.textName.text = action.text;
					if(action.GetFirstComponentInChildren<Outcome>() != null)
					{
						eItem.SetStartColor(Color.green);
						RandomEventController randomEvent = transform.GetComponent<RandomEventController>();
						eItem.arrow_out.gameObject.SetActive(randomEvent != null && randomEvent.Count > 0);
					}
				}
				else
				{
					eItem.SetStartColor(Color.green);
					eItem.arrow_out.gameObject.SetActive(false);
				}
			}
		}

	}

	public override void DestroyContent()
	{
		foreach(EditorBaseItem item in this.GetChildrenComponents<EditorBaseItem>())
			Destroy(item.gameObject);
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		//BroadcastMessage("SetRespondable", true);
		#if UNITY_EDITOR
		Selection.activeGameObject = generalEvent.gameObject;
		#endif

		if(currentEditorBaseItem != null)
			currentEditorBaseItem.ResetColor();

		currentEditorBaseItem = null;

		SendMessageUpwards("OnEventPanelClick", this);
	}

	public void OnEditorBaseItemClick(EditorBaseItem item)
	{
		if(currentEditorBaseItem != null)
			currentEditorBaseItem.ResetColor();

		currentEditorBaseItem = item;
	}

	public override Transform GetCurrentStoryTransform()
	{
		return (currentEditorBaseItem != null) ? currentEditorBaseItem.storyTransform : generalEvent.transform;
	}

	public void SetActive(bool value)
	{
		GetComponent<Image>().color = (value) ? Color.blue : new Color(1f, 1f, 1f, 0.4f);
		BroadcastMessage("SetRespondable", value);
	}

}


