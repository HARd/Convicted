using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventPanelController : MonoBehaviour 
{
	public ActionPanelController EventActionPanel;
	public Text descriptionText;

	void Start () 
	{
		gameObject.SetActive(false);
	}
}
