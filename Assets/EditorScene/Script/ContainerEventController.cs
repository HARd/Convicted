using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using ObjectsExtensionMethods;

public class ContainerEventController : ContainerController
{
	[SerializeField]
	EventPanelController eventPanelController;

	public EventPanel currentEventPanel;

	List<EventPanel> EventPanels = new List<EventPanel>();


	public override void Draw(Transform  transform)
	{
		EventPanelLine.Clear();
		EventPanels.Clear();
		
		DestroyContent();

		GetComponent<ScrollRect>().verticalScrollbar.value = 1f;
		GetComponent<ScrollRect>().horizontalScrollbar.value = 1f;

		DrawEventPanels(CreateEventPanel(transform));

		Invoke("DrawLine", 0.1f);
		DrawPreview(0);
	}

	public void DrawPreview(EventPanel eventPanel)
	{
		if(currentEventPanel)
			currentEventPanel.SetActive(false);
		
		currentEventPanel = eventPanel;
		eventPanelController.gameObject.SetActive(true);
		List<Action> actionList = eventPanel.generalEvent.GetChildrenComponents<Action>();
		if(actionList.Count > 0) 
		{
			eventPanelController.descriptionText.text = Localization.Instance.GetLocale(eventPanel.generalEvent.description_id);
			eventPanelController.EventActionPanel.DrawActions(actionList, true);
		}
		else
		{
			List<Outcome> outcomList = eventPanel.generalEvent.GetChildrenComponents<Outcome>();
			if(outcomList.Count > 1)
				(eventPanelController.EventActionPanel as EditorActionPanelController).DrawPreview(outcomList);
			else
				DrawPreview(outcomList[0]);
		}

		eventPanel.SetActive(true);
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
		eventPanelController.descriptionText.text = outcom.description;
		eventPanelController.EventActionPanel.ClearActions();
		eventPanelController.EventActionPanel.DrawAction(0, EventManager.Instance.continue_action, new Color32(50,50,50,255));
	}

	public void DrawLine()
	{
		EventPanelLine.DrawLine(EventPanels);
	}
		
	public void DrawEventPanels(List<EventPanel> EventPanelList)
	{
		foreach(EventPanel eventPanel in EventPanelList)
			foreach(EditorBaseItem eItem in eventPanel.GetChildrenComponents<EditorBaseItem>())
				DrawEventPanels(CreateEventPanel(eItem.storyTransform));
	}

	public virtual List<EventPanel> CreateEventPanel(Transform  transform)
	{
		List<EventPanel> EventPanelList = new List<EventPanel>();
		RandomEventController randomEvent = transform.GetComponent<RandomEventController>();
		if(randomEvent != null)
		{
			randomEvent.randomEventList.RemoveAll(x => x == null);
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
		item.name = generalEvent.name;
		EventPanel eventPanel = item.GetComponent<EventPanel>();
		eventPanel.generalEvent = generalEvent;
		countItems++;
		return eventPanel;
	}

	public override void DestroyContent()
	{
		base.DestroyContent();

		foreach(EventPanel eventPanel in EventPanels)
			Destroy(eventPanel.gameObject);

		EventPanels.Clear();

		eventPanelController.gameObject.SetActive(false);
	}

	public void OnEventPanelClick(EventPanel eventPanel)
	{
		currentContainerController = this;

		if(currentEventPanel != null)
			currentEventPanel.BroadcastMessage("SetRespondable", false);
		
		DrawPreview(eventPanel);
	}


	public void OnEditorBaseItemClick(EditorBaseItem item)
	{
		currentContainerController = this;
	}

	public override Transform GetCurrentStoryTransform()
	{
		return currentEventPanel.GetCurrentStoryTransform();
	}
		
	public bool IsEmpty()
	{
		return EventPanels.Count == 0;
	}

	public int EventPanelsCount()
	{
		return EventPanels.Count;
	}
}
