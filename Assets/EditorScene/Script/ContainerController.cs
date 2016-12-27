using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContainerController : ContainerControllerBase 
{
	public override void Draw(Transform  trans)
	{
		//print(currentEditorBaseItem + "  ---Draw----  " + name);
		DestroyContent();
		foreach(Transform transform in trans)
		{
			Action action = transform.GetComponent<Action>();
			if(action != null)
			{
				EditorBaseItem.CreatEditorChangeItem(transform.gameObject, actionPrefab, content.transform, action.text).GetComponent<EditorEventExecuter>().eventName = "OnClick_" + name;
				countItems++;
			}
		}
	}
}
