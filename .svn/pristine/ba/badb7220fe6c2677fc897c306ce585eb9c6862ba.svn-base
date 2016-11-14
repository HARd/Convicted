/*
	Компонент, выдвигающий/задвигающий элемент панели
	Автор: Гарбуз В.П.
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using ObjectsExtensionMethods;

public class PanelElementMover : MonoBehaviour
{
	[SerializeField]
	Transform unavailableAnchor;

	[SerializeField]
	Transform availableAnchor;

	[SerializeField]
	float moveTime = 0.4f;

	[SerializeField]
	Button ButtonBlocked;

	[SerializeField]
	GameObject PanelBlocker;

	[SerializeField]
	AudioSource sound;

//	[SerializeField]
//	bool hideIfUnavailable = true;

	Transform currentTarget;
	Mover mover;

	public bool IsMoving { get { return mover.enabled; } }
	public bool IsVisible { get { return currentTarget == availableAnchor; } }

	public void ChangeShow()
	{
		sound.Play();
		SetCurrentTarget((currentTarget == availableAnchor) ? unavailableAnchor : availableAnchor);
		StartMovingToCurrentTarget(moveTime);
	}

	public void SetCurrentTarget(Transform currentTarget)
	{
		this.currentTarget = currentTarget;
		PanelBlocker.SetActive(currentTarget == availableAnchor);
	}

	float GetAnchorsDistance()
	{
		return (availableAnchor.transform.localPosition - unavailableAnchor.transform.localPosition).magnitude;
	}

	public void StartMovingToCurrentTarget(float moveTime)
	{
		//gameObject.SetActive(true);
		if(ButtonBlocked)
			ButtonBlocked.interactable = false;

		float distanceToTarget = (currentTarget.localPosition - transform.localPosition).magnitude;
		float time = moveTime * distanceToTarget / GetAnchorsDistance();
		mover = (Mover)this.MoveTo(currentTarget.localPosition.x, currentTarget.localPosition.y, time);
		mover.SetFinalMessage(gameObject, "OnArrivedToTarget");
	}

	void OnArrivedToTarget()
	{
		if(ButtonBlocked)
			ButtonBlocked.interactable = true;
		
//		if (currentTarget == unavailableAnchor)
//		{
//			if (hideIfUnavailable)
//				gameObject.SetActive(false);
//		}
	}
}
