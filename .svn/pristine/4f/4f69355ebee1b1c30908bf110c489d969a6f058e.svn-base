using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using ObjectsExtensionMethods;

public class ContainerEventController : ContainerController
{
	[SerializeField]
	EventPanelController eventPanelController;

	List<EventPanel> EventPanels = new List<EventPanel>();

	public override void Draw(Transform  transform)
	{
		//print("--ContainerEventController.Draw() " + name);
		EventPanel.Clear();
		EventPanels.Clear();
		
		DestroyContent();

		GetComponent<ScrollRect>().verticalScrollbar.value = 1f;
		GetComponent<ScrollRect>().horizontalScrollbar.value = 1f;

		DrawEventPanels(CreateEventPanel(transform));

		Invoke("DrawLine", 0.1f);
		DrawPreview(0);
	}

	public void SetPanelsColorDefault()
	{
		foreach(EventPanel eventPanel in EventPanels)
			eventPanel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.4f);
	}

	public void DrawPreview(EventPanel eventPanel)
	{
		eventPanelController.gameObject.SetActive(true);
		List<Action> actionList = eventPanel.generalEvent.GetChildrenComponents<Action>();
		if(actionList.Count > 0) 
		{
			SetPanelsColorDefault();
			eventPanelController.descriptionText.text = Localization.Instance.GetLocale(eventPanel.generalEvent.description_id);
			eventPanelController.EventActionPanel.DrawActions(actionList, true);
		}
		else
		{
			List<Outcome> outcomList = eventPanel.generalEvent.GetChildrenComponents<Outcome>();
			if(outcomList.Count > 1)
			{
				(eventPanelController.EventActionPanel as EditorActionPanelController).DrawPreview(outcomList);
			}
			else
			{
				SetPanelsColorDefault();
				DrawPreview(outcomList[0]);
			}
		}
		eventPanel.GetComponent<Image>().color = Color.blue;
	}

	public void DrawPreview(int id)
	{
		if(EventPanels.Count > 0)
			DrawPreview(EventPanels[id]);
		else
			eventPanelController.gameObject.SetActive(false);
	}
	 
	public void DrawPreview(GeneralEvent generalEvent)
	{
		DrawPreview(EventPanels.Find(x => x.generalEvent == generalEvent));
	}

	public void DrawPreview(Outcome outcom)
	{
		//SetPanelsColorDefault();
		eventPanelController.descriptionText.text = outcom.description;
		eventPanelController.EventActionPanel.ClearActions();
		eventPanelController.EventActionPanel.DrawAction(0, EventManager.Instance.continue_action, new Color32(50,50,50,255));
	}

	public void DrawLine()
	{
		foreach(EventPanel eventPanel in EventPanels)
		{
			eventPanel.DrawLine(EventPanels);
		}
	}
		
	public void DrawEventPanels(List<EventPanel> EventPanelList)
	{
		foreach(EventPanel eventPanel in EventPanelList)
		{
			foreach(EditorBaseItem eItem in eventPanel.GetChildrenComponents<EditorBaseItem>())
			{
				DrawEventPanels(CreateEventPanel(eItem.storyTransform));
			}
		}
	}

	public virtual List<EventPanel> CreateEventPanel(Transform  transform)
	{
		List<EventPanel> EventPanelList = new List<EventPanel>();
		RandomEventController randomEvent = transform.GetComponent<RandomEventController>();
		if(randomEvent != null)
		{
			foreach(GeneralEvent generalEvent in randomEvent.randomEventList)
			{
				if(!EventPanels.Exists(x => x.generalEvent == generalEvent))
				{
					EventPanel eventPanel = CreatePanel(generalEvent);
					EventPanelList.Add(eventPanel);
					eventPanel.Draw(generalEvent.transform);
				}
			}
		}
		else
		{
			ActionMGController actionMGController = transform.GetComponent<ActionMGController>();
			if(actionMGController != null)
			{
				GeneralEvent generalEvent = actionMGController.MgComplite;
				if(!EventPanels.Exists(x => x.generalEvent == generalEvent))
				{
					EventPanel eventPanel = CreatePanel(generalEvent);
					EventPanelList.Add(eventPanel);
					eventPanel.Draw(generalEvent.transform);
				}

				generalEvent = actionMGController.MgNotComplite;
				if(!EventPanels.Exists(x => x.generalEvent == generalEvent))
				{
					EventPanel eventPanel = CreatePanel(generalEvent);
					EventPanelList.Add(eventPanel);
					eventPanel.Draw(generalEvent.transform);
				}
			}
		}
		EventPanels.AddRange(EventPanelList);
		return EventPanelList;
	}

	public EventPanel CreatePanel(GeneralEvent generalEvent)
	{
		GameObject item = Instantiate(actionPrefab, content.transform, false) as GameObject;
		//item
		item.name = generalEvent.name;
		EventPanel eventPanel = item.GetComponent<EventPanel>();
		eventPanel.generalEvent = generalEvent;
		eventPanel.containerEventController = this;
		return eventPanel;
	}
}
