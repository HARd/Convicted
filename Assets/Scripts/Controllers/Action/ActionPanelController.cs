﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActionPanelController : MonoBehaviour 
{
	[SerializeField]
	protected BaseActionController[] actions;

	public BaseActionController GetAction(int ID)
	{
		return actions[ID];
	}

	public BaseActionController GetAction(string actionName)
	{
		return System.Array.Find(actions, ac => {return ac.action.name == actionName;});
	}

	public void ClearActions()
	{
		foreach(BaseActionController action in actions)
		{
			action.Clear();
		}
	}

	public virtual void DrawActions(List<Action> actionList, bool SpecialAction)
	{
		ClearActions();

		Action special_action = (SpecialAction) ? SelectSpecialAction(actionList) : null;
		Action story_action = SelectStoryAction (actionList);
		//print("--DrawActions SpecialAction = " + SpecialAction);
		int i = 0;

		foreach(Action action in actionList) 
		{
			if(action.tag != "SpecialAction" && action.tag != "StoryAction" && action.GetComponent<Action>().VerifyActionRequirements())
				actions[i++].Draw(action);
		}

		if(special_action != null)
			actions[i++].DrawSpecialAction(special_action, new Color32(90,155,0,255));

		if(story_action != null)
			actions[i].DrawSpecialAction(story_action, new Color32(0,0,255,255));
	}

	public void DrawAction(int index, Action action, Color32 color)
	{
		actions[index].DrawSpecialAction(action, color);
	}
		
	protected Action SelectSpecialAction(List<Action> actionList)
	{
		List<Action> specialActionList = new List<Action>(actionList.FindAll(action => action.tag == "SpecialAction"));

		foreach(Action action in specialActionList)
		{
			action.ModifyChance();
		}

		specialActionList.Sort((a, b) => a.chance.CompareTo(b.chance));

		foreach(Action actionData in specialActionList)
		{
			if((actionData.type == "quest" || Random.Range(0,100) <= actionData.chance) && actionData.VerifyActionRequirements()) 
				return actionData;
		}

		return null;
	}

	protected Action SelectStoryAction(List<Action> actionList)
	{
		List<Action> storyActionList = new List<Action>(actionList.FindAll(action => action.tag == "StoryAction"));

		foreach(Action action in storyActionList)
		{
			action.ModifyChance();
		}

		storyActionList.Sort ((a, b) => a.chance.CompareTo (b.chance));

		foreach(Action actionData in storyActionList)
		{
			if((Random.Range(0,100) <= actionData.chance) && actionData.VerifyActionRequirements()) 
				return actionData;
		}

		return null;
	}

}
