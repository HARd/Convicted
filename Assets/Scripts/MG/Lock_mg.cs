﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Lock_mg : MgBase
{
	[SerializeField]
	GameObject helpPanel;

	[SerializeField]
	Text helpText;

	[SerializeField]
	ActionCounter counter;

	public float delay = 0.5f;
	public float offset = 0.5f;

	[SerializeField]
	AnimatorContoller[] animators;

	[SerializeField]
	AnimatorContoller animator;

	[SerializeField]
	AnimatorContoller animatorFinish;
	// Use this for initialization
	bool isDisabled = false;
	bool isFirstClick = true;
	bool isRightClick = false;
	int blocker = 0;

	const string first_open_mg_lock = "FIRST_OPEN_MG_LOCK";

	void Start() 
	{
		StartCoroutine(BlockCoroutine(2f));
		if(!SaveLoadXML.HasKey(first_open_mg_lock))
		{
			SaveLoadXML.SetValue(first_open_mg_lock, "true");
			StartCoroutine(StartHelpShow(2f));
		}
		helpText.text = Localization.Instance.GetLocale(2524);
	}
	
	public override void OnPointerClick (PointerEventData eventData) //SendMessage handler
	{
		if(!isDisabled && counter.Counter < animators.Length)
		{
			//print(1);
			if(isFirstClick)
			{
				animators[counter.Counter].Execute(AnimatorContoller.ControlMode.SetBool, "up");
				animator.Execute("up");
				StartCoroutine(StartTimeCoroutine());
			}
			else
			{
				//print(2);
				isFirstClick = true;
				if(isRightClick == true)
				{
					animator.Execute("up");
					counter.Add();
					if(counter.Counter >= animators.Length)
					{
						animatorFinish.Execute();
						isComplite = true;
						Destroy(gameObject, 1f);
					}
				}
				else
				{
					Reset();
				}
				StartCoroutine(BlockCoroutine(1f));
			}
		}
	}

	public override void Reset()
	{
		for(int i = 0; i <= counter.Counter; i++)
		{
			//print(i);
			animators[i].Execute(AnimatorContoller.ControlMode.PlayAnimation, "forcer_down");
		}
		counter.Reset();
		StartCoroutine(BlockCoroutine(1f));
	}

	IEnumerator StartTimeCoroutine()
	{
		isFirstClick = false;

		yield return new WaitForSeconds(delay);
		isRightClick = true;

		yield return new WaitForSeconds(offset);
		isRightClick = false;
		if(!isFirstClick)
		{
			isFirstClick = true;
			Reset();
		}
		else
			StartCoroutine(BlockCoroutine(0.5f));
	}

	IEnumerator BlockCoroutine(float delay)
	{
		blocker++;
		isDisabled = true;

		yield return new WaitForSeconds(delay);
		if(--blocker == 0)
			isDisabled = false;
	}

	IEnumerator StartHelpShow(float delay)
	{
		yield return new WaitForSeconds(delay);
		HelpShow();
	}

	public void HelpHide()
	{
		helpPanel.SetActive(false);
	}

	public void HelpShow()
	{
		helpPanel.SetActive(true);
	}
}
