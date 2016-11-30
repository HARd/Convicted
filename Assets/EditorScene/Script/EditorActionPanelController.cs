using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditorActionPanelController : ActionPanelController
{
	[SerializeField]
	ContainerEventController containerEventController;


	[SerializeField]
	GameObject changeEventPanel;

	public override void DrawActions(List<Action> actionList, bool SpecialAction)
	{
		ClearActions();

		Action special_action = (SpecialAction) ? SelectSpecialAction(actionList) : null;
		Action story_action = SelectStoryAction (actionList);
		//print("--DrawActions SpecialAction = " + SpecialAction);
		int i = 0;

		foreach(Action action in actionList) 
		{
			if(action.tag != "SpecialAction" && action.tag != "StoryAction")// && action.GetComponent<Action>().VerifyActionRequirements())
				actions[i++].Draw(action);
		}

		if(special_action != null)
			actions[i++].DrawSpecialAction(special_action, new Color32(90,155,0,255));

		if(story_action != null)
			actions[i].DrawSpecialAction(story_action, new Color32(0,0,255,255));
	}

	public void DrawPreview(GeneralEvent generalEvent)
	{
		containerEventController.DrawPreview(generalEvent);
	}

	public void DrawPreview(Outcome outcom)
	{
		containerEventController.DrawPreview(outcom);
	}

	public void DrawPreview(List<GeneralEvent> randomEventList)
	{
		GameObject changePanel = Instantiate(changeEventPanel, this.transform, false) as GameObject;
		changePanel.GetComponent<ChangeEventPanel>().Draw(randomEventList);
	}

	public void DrawPreview(List<Outcome> outcomList)
	{
		GameObject changePanel = Instantiate(changeEventPanel, this.transform, false) as GameObject;
		changePanel.GetComponent<ChangeEventPanel>().Draw(outcomList);
	}

	public void AddDrawPreview(List<Outcome> outcomList)
	{
		ChangeEventPanel changePanel = GetComponentInChildren<ChangeEventPanel>();
		changePanel.Draw(outcomList);
	}
}
