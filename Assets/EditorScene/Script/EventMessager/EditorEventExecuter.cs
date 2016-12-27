using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EditorEventExecuter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler 
{
	public string eventName;

	[SerializeField]
	bool executeOnClick = true;

	[SerializeField]
	bool executeOnFocusIn = false;

	[SerializeField]
	bool executeOnFocusOut = false;


	public virtual void OnPointerClick (PointerEventData eventData)
	{
		if(executeOnClick)
			DoAction(gameObject);
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (executeOnFocusIn)
			DoAction(gameObject);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if (executeOnFocusOut)
			DoAction(gameObject);
	}

//	public virtual void DoAction()
//	{
//		EditorEventManager.TriggerEvent(eventName);
//	}

	public virtual void DoAction(GameObject go)
	{
		//print(go);
		EditorEventManager.TriggerEvent(eventName, go);
	}

	public virtual void DoAction(Transform go)
	{
		EditorEventManager.TriggerEvent(eventName, go);
	}

	public virtual void DoAction<T>(T go)
	{
		EditorEventManager.TriggerEvent(eventName, go);
	}
}
