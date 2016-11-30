using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ObjectsExtensionMethods;

public class EventPanel : EventPanelBase,  IPointerClickHandler
{
	[SerializeField]
	protected Text textName;


	public GeneralEvent generalEvent;
	public Image arrow_in;
	public Transform line_in;
	public ContainerEventController containerEventController;

	static Dictionary<EventPanel, int> linesEventPanel = new Dictionary<EventPanel, int>();
	static List<int> linesStep = new List<int>();

	public override void Draw(Transform  trans)
	{
		textName.text = name;
		foreach(Transform transform in trans)
		{
			GeneralEvent generalEvent = transform.GetComponent<GeneralEvent>();
			if(generalEvent == null)
			{
				EditorAction eItem = (EditorBaseItem.CreatEditorChangeItem(transform.gameObject, actionPrefab, this.transform, transform.name) as EditorAction);

				Action action = transform.GetComponent<Action>();
				if(action != null)
				{
					eItem.textName.text = action.text;
					if(action.GetFirstComponentInChildren<Outcome>() != null)
					{
						eItem.GetComponent<Image>().color = Color.green;
						RandomEventController randomEvent = transform.GetComponent<RandomEventController>();
						eItem.arrow_out.gameObject.SetActive(randomEvent != null && randomEvent.Count > 0);
					}
				}
				else
				{
					eItem.GetComponent<Image>().color = Color.green;
					eItem.arrow_out.gameObject.SetActive(false);
				}
			}
		}

	}

	public override void DestroyContent()
	{
		foreach(EditorBaseItem item in this.GetChildrenComponents<EditorBaseItem>())
			Destroy(item.gameObject);
	}

	public void DrawLineEvent(EditorAction eItem, GeneralEvent generalEvent, List<EventPanel> EventPanels, bool is_double_event = false)
	{
		EventPanel ePanel = EventPanels.Find(x => x.generalEvent == generalEvent);
		if(ePanel != null)
		{
			float scale = (eItem.line_event.position.y - ePanel.line_in.position.y)*0.9965f;
			eItem.line_event.localScale = new Vector3(1, scale, 1);
			int step = 0;
			//print(" --generalEvent = " + generalEvent);
			if(scale > 0)
			{

				if(linesEventPanel.ContainsKey(ePanel))
				{
					step = linesEventPanel[ePanel];
				}
				else
				{
					step = (!is_double_event) ? FindEmptyStep() : eItem.level_line;
					linesStep.Add(step);
					linesEventPanel[ePanel] = step;
					eItem.level_line = step;
					ePanel.line_in.localScale = new Vector3((step+1)*2f, ePanel.line_in.localScale.y, 1);
				}
			}
			else
			{
				step = FindMaxLevel(EventPanels) + 1;
				eItem.level_line = step;
				ePanel.line_in.localScale = new Vector3((step+1)*2f, ePanel.line_in.localScale.y, 1);
			}

			scale = (step+1)*2f;
			if(!is_double_event)
				eItem.line_out.localScale = new Vector3(scale*eItem.line_out.localScale.x, 1, 1);
			//print(eItem.line_out.localScale);
			eItem.line_event.localScale = new Vector3(1/eItem.line_out.localScale.x, eItem.line_event.localScale.y, 1);
		}
	}

	public void DrawLine(List<EventPanel> EventPanels)
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
					DrawLineEvent(eItem, randomEvent.GetEvent(i), EventPanels, i > 0);
			}
			else
			{
				ActionMGController actionMGController = eItem.storyTransform.GetComponent<ActionMGController>();
				if(actionMGController != null)
				{
					DrawLineEvent(eItem, actionMGController.MgComplite, EventPanels);
					DrawLineEvent(eItem, actionMGController.MgNotComplite, EventPanels, true);
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

	public int FindMaxLevel(List<EventPanel> EventPanels)
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

	public static void Clear()
	{
		linesStep.Clear();
		linesEventPanel.Clear();
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		containerEventController.DrawPreview(this);
	}
}
