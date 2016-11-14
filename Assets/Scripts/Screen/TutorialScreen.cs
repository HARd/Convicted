﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialScreen : ScreenBase 
{
	[SerializeField]
	Image TutorialCharacter;
	[SerializeField]
	GameObject TutorialTextPanel;
	[SerializeField]
	Text TutorialDescriptionMessage;
	[SerializeField]
	Text TutorialTapMessage;
	[SerializeField]
	Image TutorialPointerImage;

	[SerializeField]
	Transform AnchorTarget;

	[SerializeField]
	bool finishTutorial = true;

	//[SerializeField]
	public List<TutorialStep> tutorialStepList = new List<TutorialStep>();
	public int currentTutorialStep = 0;

	bool blocker = false;


	void Awake()
	{
		TutorialCharacter.gameObject.SetActive (false);
		TutorialTextPanel.SetActive (false);
		TutorialPointerImage.gameObject.SetActive (false);
	}

	void Start()
	{
		PlayerInfo.Instance.isTutorial = true;
		ShowTutorialMessage(tutorialStepList[currentTutorialStep]);
	}

	public void ShowTutorialMessage(TutorialStep step)
	{
		TutorialCharacter.gameObject.SetActive(step.hasCharacter);

		TutorialPointerImage.gameObject.SetActive (step.hasPointer);
		if (step.hasPointer) 
		{
			if(step.targetFocus != null)
				TutorialPointerImage.transform.position = new Vector3(step.targetFocus.position.x + step.pointerPosition.x, step.targetFocus.position.y + step.pointerPosition.y);
			else
				TutorialPointerImage.transform.localPosition = new Vector3(step.pointerPosition.x, step.pointerPosition.y);
//			TutorialPointerImage.transform.localPosition = new Vector3 (step.pointerPosition.x, step.pointerPosition.y, 0);
//			TutorialPointerImage.transform.position = new Vector3(step.targetFocus.position.x + step.pointerPosition.x, step.targetFocus.position.y + step.pointerPosition.y);
			GUIEffect pointerEffect = TutorialPointerImage.GetComponent<GUIEffect> ();
			pointerEffect.ResetData ();
			pointerEffect.startPos = TutorialPointerImage.transform.localPosition;
			TutorialPointerImage.transform.localRotation = Quaternion.identity;

			switch (step.pointerRot) 
			{
			case TutorialStep.PointerRotation.Up:
				pointerEffect.effectType = GUIEffect.EffectType.SwingVertical;
				break;
			case TutorialStep.PointerRotation.Right:
				TutorialPointerImage.transform.Rotate (0,0,-90);
				pointerEffect.effectType = GUIEffect.EffectType.SwingHorizontal;
				break;
			case TutorialStep.PointerRotation.Down:
				TutorialPointerImage.transform.Rotate (0,0,180);
				pointerEffect.effectType = GUIEffect.EffectType.SwingVertical;
				break;
			case TutorialStep.PointerRotation.Left:
				TutorialPointerImage.transform.Rotate (0,0,90);
				pointerEffect.effectType = GUIEffect.EffectType.SwingHorizontal;
				break;
			}
		} 

		TutorialTextPanel.SetActive (true);
		if (step.descriprion_id == 0 && step.tap_id == 0) 
		{
			TutorialTextPanel.SetActive (false);
		} 
		else 
		{
			TutorialDescriptionMessage.text = Localization.Instance.GetLocale (step.descriprion_id);
			TutorialTapMessage.text = Localization.Instance.GetLocale (step.tap_id);
		}

		switch (step.textPos) 
		{
		case TutorialStep.TextPosition.Bottom:
			TutorialTextPanel.transform.localPosition = new Vector3 (96,-475,0);
			break;
		case TutorialStep.TextPosition.Center:
			TutorialTextPanel.transform.localPosition = new Vector3 (0,0,0);
			break;
		case TutorialStep.TextPosition.Top:
			TutorialTextPanel.transform.localPosition = new Vector3 (0,450,0);
			break;
		}

		switch (step.panel) 
		{
		case TutorialStep.enumPanel.Action:
			AddClickObject(PanelManager.Instance.ActionPanel.GetAction(step.target.name).gameObject).GetComponent<ActionController>().action = step.target.GetComponent<Action>();
			break;
		case TutorialStep.enumPanel.Event:
			AddClickObject(PanelManager.Instance.EventPanel.EventActionPanel.GetAction(step.target.name).gameObject);
			break;
		case TutorialStep.enumPanel.OpenQuest:
			QuestManager.Instance.GenerateTutorialQuest();
			QuestManager.Instance.CreateQuest();
			QuestManager.Instance.questLogCountText.text = PanelManager.Instance.QuestPanel.questLog.Count.ToString();
			AddClickObject(PanelManager.Instance.QuestLogButton.gameObject);
			break;
		case TutorialStep.enumPanel.QuestHint:
			AddClickObject(PanelManager.Instance.QuestPanel.GetQuest(step.target.name).gameObject).AddComponent<OnClickDelegatController>().onClick += () => {
				ScreenManager.Instance.current.GetComponent<ScreenBase>().onDestroy += ()=>{PanelManager.Instance.CloseButton(); TutorialTap();};
			};
			break;
		case TutorialStep.enumPanel.CloseQuest:
			step.target.GetComponent<Action>().Perform_Action();
			break;
		case TutorialStep.enumPanel.Timed:
			blocker = true;
			StartCoroutine(TimedCoroutine(step.time));
			break;
		}

		SendMessage("OnShowTutorialMessage", step);
	}

	IEnumerator TimedCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		blocker = false;
		TutorialTap();
	}

	public GameObject AddClickObject(GameObject obj)
	{
		blocker = true;
		GameObject go = Instantiate(obj, obj.transform.parent) as GameObject;
		go.transform.SetParent(AnchorTarget, true);
		go.gameObject.AddComponent<OnClickDelegatController>().onClick += () => {blocker = false; Destroy(go); TutorialTap();};
		return go;
	}

	public void TutorialTap()
	{
		currentTutorialStep++;
		if(currentTutorialStep < tutorialStepList.Count)
		{
			AudioManager.Instance.Play(1);
			TutorialCharacter.gameObject.SetActive (false);
			TutorialTextPanel.SetActive (false);
			TutorialPointerImage.gameObject.SetActive (false);
			SendMessage("OnTutorialTap");

			ShowTutorialMessage(tutorialStepList[currentTutorialStep]);
		}
		else
		{
			if(finishTutorial)
			{
				SaveLoadXML.SetValue("TUTORIAL_SHOWED","true");
				PlayerInfo.Instance.SaveGame();
			}
			else
			{
				GameData.current.isTutorialCompleted = true;
				SaveLoadXML.SaveGameDataXML();
			}
			PlayerInfo.Instance.isTutorial = false;
			Destroy(gameObject);
		}
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		
		if(!blocker)
			TutorialTap();
	}
}
