using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ContainerControllerBase : MonoBehaviour
{
	[SerializeField]
	protected GameObject actionPrefab;

	[SerializeField]
	protected GameObject content;

	public EditorBaseItem inputEditorBaseItem {set; get;}
	public EditorBaseItem currentEditorBaseItem {set; get;}
	public int countItems {set; get;}

	public static ContainerControllerBase currentContainerController;

	public virtual void Draw(Transform  trans)
	{
		DestroyContent();

		foreach(Transform transform in trans)
		{
			EditorBaseItem.CreatEditorChangeItem(transform.gameObject, actionPrefab, content.transform, transform.name).GetComponent<EditorEventExecuter>().eventName = "OnClick_" + name;
			countItems++;
		}
	}

	public virtual void Draw(GameObject go)
	{
		inputEditorBaseItem = go.GetComponent<EditorBaseItem>();
		Draw(inputEditorBaseItem.storyTransform);
	}

	public virtual void DrawInputEditorBaseItem() 
	{
		if(inputEditorBaseItem != null)
			Draw(inputEditorBaseItem.storyTransform);
	}

	public virtual void DestroyContent()
	{
		countItems = 0;
		foreach(Transform transform in content.transform)
			Destroy(transform.gameObject);
	}

	public virtual void OnEditorBaseItemClick(GameObject go) //SendMessage
	{
		currentContainerController = this;
		EditorBaseItem editorBaseItem = go.GetComponent<EditorBaseItem>();
		if(editorBaseItem == currentEditorBaseItem)
			return;

		if(currentEditorBaseItem != null)
			currentEditorBaseItem.ResetColor();

		currentEditorBaseItem = editorBaseItem;
	}

	public void SetThisCurrentContainer() //SendMessage
	{
		currentContainerController = this;
	}

	public virtual Transform GetCurrentStoryTransform()
	{
		return currentEditorBaseItem.storyTransform;
	}

	public static Transform GetStoryTransform()
	{
		return currentContainerController.GetCurrentStoryTransform();
	}
}
