using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObjectsExtensionMethods;
//using UnityEditor;

public class EventPanelLine : MonoBehaviour
{
	static Dictionary<EventPanelLine, int> linesEventPanel = new Dictionary<EventPanelLine, int>();
	static List<int> linesStep = new List<int>();
	static List<EventPanel> EventPanels;
	
	public Image arrow_in;
	public Transform line_in;


	public void DrawLineEvent(EditorAction eItem, GeneralEvent generalEvent, bool is_double_event = false)
	{
		EventPanel ePanel = EventPanels.Find(x => x.generalEvent == generalEvent);
		EventPanelLine ePanelLine = ePanel.GetComponent<EventPanelLine>();
		if(ePanel != null)
		{
			float scale = (eItem.line_event.position.y - ePanelLine.line_in.position.y)*0.9965f;
			eItem.line_event.localScale = new Vector3(1, scale, 1);
			int step = 0;
			//print(" --generalEvent = " + generalEvent);
			if(scale > 0)
			{

				if(linesEventPanel.ContainsKey(ePanelLine))
				{
					step = linesEventPanel[ePanelLine];
				}
				else
				{
					step = (!is_double_event) ? FindEmptyStep() : eItem.level_line;
					linesStep.Add(step);
					linesEventPanel[ePanelLine] = step;
					eItem.level_line = step;
					ePanelLine.line_in.localScale = new Vector3((step+1)*2f, ePanelLine.line_in.localScale.y, 1);
				}
			}
			else
			{
				step = FindMaxLevel() + 1;
				eItem.level_line = step;
				ePanelLine.line_in.localScale = new Vector3((step+1)*2f, ePanelLine.line_in.localScale.y, 1);
			}

			scale = (step+1)*2f;
			if(!is_double_event)
				eItem.line_out.localScale = new Vector3(scale*eItem.line_out.localScale.x, 1, 1);
			//print(eItem.line_out.localScale);
			eItem.line_event.localScale = new Vector3(1/eItem.line_out.localScale.x, eItem.line_event.localScale.y, 1);
		}
	}

	public void DrawLine()
	{
		if(linesEventPanel.ContainsKey(this))
		{
			linesStep.Remove(linesEventPanel[this]);
			linesEventPanel.Remove(this);
		}

		foreach(EditorAction eItem in this.GetChildrenComponents<EditorAction>())
		{
			RandomEventController randomEvent = eItem.storyTransform.GetComponent<RandomEventController>();
			if(randomEvent != null)
			{
				for(uint i = 0; i < randomEvent.Count; i++)
					DrawLineEvent(eItem, randomEvent.GetEvent(i), i > 0);
			}
			else
			{
				ActionMGController actionMGController = eItem.storyTransform.GetComponent<ActionMGController>();
				if(actionMGController != null)
				{
					DrawLineEvent(eItem, actionMGController.MgComplite);
					DrawLineEvent(eItem, actionMGController.MgNotComplite, true);
				}
			}
		}
	}

	public static int FindEmptyStep()
	{
		int step = 0;
		if(linesStep.Count > 0)
		{
			linesStep.Sort();
			for(int i = 0; i < linesStep.Count; i++)
			{
				if(linesStep[i] != i)
					return i;
			}
			step = linesStep.Count;
		}
		return step;
	}

	public int FindMaxLevel()
	{
		int max = 0;
		foreach(EventPanel eventPanel in EventPanels)
		{
			if(eventPanel == this)
				break;
			
			foreach(EditorAction action in eventPanel.GetChildrenComponents<EditorAction>())
			{
				if(action.level_line > max)
					max = action.level_line;
			}
		}
		return max;
	}

	public static void DrawLine(List<EventPanel> panels)
	{
		EventPanels = panels;
		foreach(EventPanel eventPanel in EventPanels)
		{
			eventPanel.GetComponent<EventPanelLine>().DrawLine();
		}
	}

	public static void Clear()
	{
		linesStep.Clear();
		linesEventPanel.Clear();
	}
		
}

