using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChangeEventPanel : MonoBehaviour
{

	[SerializeField]
	GameObject textEventsPrefab;

	[SerializeField]
	GameObject textOutcomsPrefab;

	[SerializeField]
	GameObject actionPrefab;

	[SerializeField]
	Transform content;

	public virtual void Draw(List<GeneralEvent> randomEventList)
	{
		if(randomEventList.Count > 0)
			Instantiate(textEventsPrefab, content, false);
		foreach(GeneralEvent generalEvent in randomEventList)
			EditorBaseItem.CreatEditorChangeItem(generalEvent.gameObject, actionPrefab, content, generalEvent.name + "   " + generalEvent.base_chance + "%");
	}

	public virtual void Draw(List<Outcome> outcomList)
	{
		if(outcomList.Count > 0)
			Instantiate(textOutcomsPrefab, content, false);
		foreach(Outcome outcome in outcomList)
			EditorBaseItem.CreatEditorChangeItem(outcome.gameObject, actionPrefab, content, outcome.name + "   " + outcome.base_chance + "%");
	}

	public void DestroyChangeEventPanel()
	{
		Destroy(gameObject);
	}
}
