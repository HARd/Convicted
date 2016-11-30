using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ObjectsExtensionMethods;

public class EditorStoryManager : StoryManager
{
	public GameObject content;
	public GameObject itemPrefab;
	public ContainerController container;

	public override void CheckStory()
	{
		foreach(Transform trans in storyActions.transform)
			EditorBaseItem.CreatEditorChangeItem(trans.gameObject, itemPrefab, content.transform, trans.name, container);
	}

}
