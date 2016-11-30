using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using ObjectsExtensionMethods;

public class EditorBaseItem : MonoBehaviour, IPointerClickHandler
{
	public Text textName;

	public Transform storyTransform;

	public ContainerController container;

	public virtual void OnPointerClick (PointerEventData eventData)
	{
		SendMessageUpwards("ResetColor");
		container.Draw(storyTransform);
		Image image = GetComponent<Image>();
		image.color = new Color(image.color.r, image.color.g, 0.3f);
	}

	public void SetColorAction(Color color)
	{
		Image image = GetComponent<Image>();
		image.color = color;
	}

	static public EditorBaseItem CreatEditorChangeItem(GameObject story, GameObject prefab, Transform parent, string text = "", ContainerController container = null)
	{
		GameObject action = Instantiate(prefab, parent, false) as GameObject;
		action.name = story.name;
		EditorBaseItem eAction = action.GetComponent<EditorBaseItem>();
		eAction.storyTransform = story.transform;
		eAction.textName.text = text;
		eAction.container = container;
		return eAction;
	}
}
