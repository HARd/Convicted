/*
	Поддержка анимированной реакции на клик.
*/


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class AnimatedClickReaction  : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	Animator animator;

	[SerializeField]
	string parameterName;

	[SerializeField]
	bool playOnClick = true;

	[SerializeField]
	bool playOnFocusIn = false;

	[SerializeField]
	bool stopOnFocusOut = false;

	[SerializeField]
	float delay = 0f;

	[SerializeField]
	float blockTime = 0f;
//	[SerializeField]
//	bool blockPlayfield = false;
//	[SerializeField]
//	bool blockPanel = false;
//
	bool isDisabled = false;

	public void OnPointerClick (PointerEventData eventData) //SendMessage handler
	{
		//print("--OnPointerClick ");
		if (playOnClick && !isDisabled)
			PlayAnimation();
	}

	public void OnPointerEnter(PointerEventData eventData) //SendMessage handler
	{
		if (playOnFocusIn && !isDisabled)
			PlayAnimation();
	}

	public void OnPointerExit(PointerEventData eventData) //SendMessage handler
	{
		if (stopOnFocusOut)
		{
			if (isDisabled)
			{
				StopAllCoroutines();
				isDisabled = false;
			}
			animator.SetBool(parameterName, false);
		}
	}

	void PlayAnimation()
	{
		StartCoroutine(PlayAnimationCoroutine());
	}

	IEnumerator PlayAnimationCoroutine()
	{
		isDisabled = true;

		//if (blockPlayfield)
			//Playfield.Current.InputBlocker.AddBlock(blockTime);

		//if (blockPanel)
			//Panel.Instance.InputBlocker.AddBlock(blockTime);

		yield return new WaitForSeconds(delay);
		animator.SetBool(parameterName, true);
		yield return new WaitForSeconds(blockTime - delay);

		isDisabled = false;
	}
}
