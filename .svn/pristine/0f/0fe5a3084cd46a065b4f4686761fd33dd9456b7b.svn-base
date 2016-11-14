using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class BaseActionController : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
	public Action action;

	[SerializeField]
	Text ActionText;

	[SerializeField]
	Text CashIconText;

	[SerializeField]
	GameObject CashIcon;

	[SerializeField]
	Text EnergyIconText;

	[SerializeField]
	GameObject EnergyIcon;


	public virtual void Clear()
	{
		gameObject.SetActive(false);
		ActionText.text = "";
		CashIconText.text = "";
		CashIcon.SetActive (false);
		EnergyIconText.text = "";
		EnergyIcon.SetActive (false);
	}

	public virtual void DrawHealth()
	{

	}

	public virtual void Draw(Action action)
	{
		gameObject.SetActive(true);

		ActionText.text = " " + Localization.Instance.GetLocale(action.text_id);

		bool show_req = false;
		if(action.requireStat.Count > 0)
		{
			float energy_req = 0;
			float cash_req = 0;
			foreach(statReq stat in action.requireStat)
			{
				if(stat.name == "energy")
				{
					show_req = true;
					energy_req = stat.value;
					EnergyIconText.gameObject.SetActive (true);
					EnergyIcon.SetActive (true);
					if (energy_req == 0)
						EnergyIconText.text += " +";
				} 
				else if(stat.name == "cash")
				{
					show_req = true;
					cash_req = stat.value;
					CashIconText.gameObject.SetActive (true);
					CashIcon.SetActive (true);
				}
				else if(stat.name == "health")
				{
					show_req = true;
					DrawHealth();
				}
			}

			if (cash_req != 0) 
				CashIconText.text = " -" + cash_req;
			
			if (energy_req != 0)
				EnergyIconText.text += " -" + energy_req;
		}

		if(!show_req) 
			ActionText.text = " " + Localization.Instance.GetLocale(action.text_id);

		// Apply color to action text
		switch(action.color)
		{
		case Action.ActionColor.Default:
			ActionText.color = new Color32(50,50,50,255);
			break;
		case Action.ActionColor.Accept:
			ActionText.color = new Color32(20,120,50,255);
			break;
		case Action.ActionColor.Refuse:
			ActionText.color = new Color32(120,20,20,255);
			break;
		}

		this.action = action.GetComponent<Action>();
	}

	public void DrawSpecialAction(Action special_action, Color32 color)
	{
		gameObject.SetActive(true);
		ActionText.text = " " + Localization.Instance.GetLocale(special_action.text_id);
		ActionText.color = color;
		action = special_action;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log("OnPress " + action.text + "  " + action.type);
		AudioManager.Instance.Play(0);

		if(WorldTime.Instance.TimeSpeed == 1 || (WorldTime.Instance.pause && action.transform.parent.name != "Actions")) 
			action.Perform_Action();
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		if(WorldTime.Instance.TimeSpeed == 1) 
			ActionText.color = new Color32(255,175,0,255);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		ActionText.color = new Color32(50,50,50,255);
	}
}
