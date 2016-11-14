using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimeTableScreen : ScreenBase {

	public GameObject timeItem;
	public Text tableLabel;
	public GameObject forwardArrow;
	public GameObject backArrow;

	List<TimeTableData> timetable = new List<TimeTableData>();
	List<GameObject> timeItemList = new List<GameObject>();
	Vector3 itemPos = new Vector3 (-125, 327.5f, 0);
	int page = 0;

	// Use this for initialization
	void Start () {
		tableLabel.text = Localization.Instance.GetLocale (467);

		GetActions ();
		ShowTimeItems ();

	}

	void GetActions()
	{
		foreach (GameObject _event in EventManager.Instance.GeneralEventList)
		{
			foreach (Action action in _event.GetComponent<ActionListController>().actions)
			{
				if (action.tag == "Action" && _event.name != "night") 
				{
					TimeTableData data = new TimeTableData ();
					data.action = action;

					switch (_event.name) 
					{
					case "morning":
						data.morning = true;
						break;
					case "day":
						data.day = true;
						break;
					case "evening":
						data.evening = true;
						break;
					}

					ChangeTimeTable (data);

				}
			}
		}
	}

	void ShowTimeItems()
	{
		int j = 0;
		for (int i = page*11;i < timetable.Count;i++) {
			if (j >= 11)
				break;

			GameObject item = Instantiate(timeItem) as GameObject;
			item.transform.SetParent (transform,false);
			item.transform.localPosition = new Vector3 (itemPos.x,(itemPos.y - 69*j),itemPos.z);
			j++;
			item.GetComponent<Text> ().text = Localization.Instance.GetLocale (timetable[i].action.text_id);

			foreach (Transform checkbox in item.transform) {
				if(checkbox.name == "morning"){
					if(timetable[i].morning){
						checkbox.gameObject.SetActive (true);
					} else {
						checkbox.gameObject.SetActive (false);
					}
				}
				if(checkbox.name == "day"){
					if(timetable[i].day){
						checkbox.gameObject.SetActive (true);
					} else {
						checkbox.gameObject.SetActive (false);
					}
				}
				if(checkbox.name == "evening"){
					if(timetable[i].evening){
						checkbox.gameObject.SetActive (true);
					} else {
						checkbox.gameObject.SetActive (false);
					}
				}
			}

			timeItemList.Add (item);
		}

		if (page == 0) {
			backArrow.SetActive (false);
		} else {
			backArrow.SetActive (true);
		}

		if (page >= timetable.Count/11) {
			forwardArrow.SetActive (false);
		} else {
			forwardArrow.SetActive (true);
		}

	}

	void ClearTimeTable()
	{
		foreach (GameObject _object in timeItemList) {
			Destroy (_object);
		}

		timeItemList.Clear ();
	}
	
	void ChangeTimeTable(TimeTableData data)
	{
		TimeTableData timecheck = timetable.Find(x => x.action == data.action);
		if (timecheck == null && data.action.VerifyActionRequirements(true)) {
			timetable.Add (data);
		} else if(timecheck != null){
			if (data.morning) {
				timecheck.morning = true;
			}
			if (data.day) {
				timecheck.day = true;
			}
			if (data.evening) {
				timecheck.evening = true;
			}
		}
	}

	public void ChangePage(int _page)
	{
		page += _page;
		if (page < 0)
			page = 0;

		ClearTimeTable ();
		ShowTimeItems ();
	}
}
