using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerController : EventPanelBase 
{
	[SerializeField]
	protected GameObject content;

	[SerializeField]
	protected ContainerController container;

	public override void Draw(Transform  trans)
	{
		DestroyContent();
		foreach(Transform transform in trans)
		{
			Action action = transform.GetComponent<Action>();
			if(action != null)
				EditorBaseItem.CreatEditorChangeItem(transform.gameObject, actionPrefab, content.transform, action.text, container);
		}
	}

	public override void DestroyContent()
	{
		foreach(Transform transform in content.transform)
			Destroy(transform.gameObject);
	}

	public void ResetColor()
	{
		content.BroadcastMessage("SetColorAction", new Color(0.971f, 0.902f, 0.678f));
	}
}
