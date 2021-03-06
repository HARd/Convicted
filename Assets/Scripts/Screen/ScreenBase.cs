﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenBase : MonoBehaviour, IPointerClickHandler
{
	public bool destroyOnClick = true;

	public System.Action onDestroy {get; set;}
	public System.Action onClick {get; set;}

	void OnDestroy()
	{
		if(ScreenManager.Instance != null && ScreenManager.Instance.current == gameObject)
			ScreenManager.Instance.current = (transform.childCount > 0) ? transform.GetChild(transform.childCount-1).gameObject : null;

		if(onDestroy != null)
			onDestroy();
	}

	public virtual void OnPointerClick (PointerEventData eventData)
	{
		AudioManager.Instance.Play(1);
		if(destroyOnClick)
			Destroy(gameObject);
		else
			if(onClick != null)
				onClick();
	}
}
