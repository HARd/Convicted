using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class DailyBonusScreen : ScreenBase 
{
	[SerializeField]
	GameObject DailyBonusWindow;
	[SerializeField]
	GameObject DailyBonusCashIcon;
	[SerializeField]
	Text DailyBonusCashText;
	[SerializeField]
	Text DailyBonusDescriptionText;
	[SerializeField]
	Text GetDailyBonusButtonText;
	[SerializeField]
	Text NextDailyBonusText;

	void Start()
	{
		int bonusDay = (!PlayerInfo.Instance.isTutorial && SaveLoadXML.HasKey("PLAYER_INFO")) ? 0 : 1;
		onClick = ()=> {ShowDailyBonusPanel(bonusDay);};
	}

	void Update()
	{
		System.TimeSpan time_difference = GameData.current.nextDailyBonusTime.Subtract(System.DateTime.Now);
		NextDailyBonusText.text = Localization.Instance.GetLocale(932) + ":" + "\n" + string.Format ("{0:00}:{1:00}:{2:00}", time_difference.Hours, time_difference.Minutes, time_difference.Seconds);
	}

	public void ShowDailyBonusPanel(int bonusDay)
	{
		destroyOnClick = true;
		DailyBonusWindow.SetActive(true);

		for(int i = 0;i < EventManager.Instance.dailyRewardList[bonusDay].rewardList.Count;i++)
		{
			Parameter reward = EventManager.Instance.dailyRewardList[bonusDay].rewardList[i];
			switch (reward.stat) 
			{
			case "cash":
				DailyBonusCashIcon.SetActive (true);
				DailyBonusCashText.gameObject.SetActive (true);
				DailyBonusCashText.text = reward.value.ToString();
				PlayerInfo.Instance.cash += reward.value;
				break;
			}
		}
		ReloadDailyBonusTimer();
		DailyBonusDescriptionText.text = Localization.Instance.GetLocale(930);

		GetDailyBonusButtonText.text = Localization.Instance.GetLocale(933);
	}
	void ReloadDailyBonusTimer()
	{
		System.DateTime current_date = System.DateTime.Now;
		System.DateTime next_day = current_date.AddDays(1);
		System.DateTime next_date = new System.DateTime(next_day.Year, next_day.Month, next_day.Day, 0, 1, 0);
		GameData.current.nextDailyBonusTime = next_date;
		//System.DateTime next_date = new System.DateTime(next_day.Year, next_day.Month, next_day.Day, Random.Range(10, 20), 0, 0);
		System.TimeSpan diff = next_date.AddHours(UnityEngine.Random.Range(10, 20)).Subtract(current_date);
		//print(diff);
		//GameData.current.nextDailyBonusTime = System.DateTime.Now.AddHours(24);
		//GameData.current.nextDailyBonusTime = System.DateTime.Now.AddMinutes(1);
		//SendMessage("SendNotification", 60L);
		SendMessage("SendNotification", (long)diff.TotalSeconds);
		SaveLoadXML.SaveGameDataXML();
	}

	public void OnButtonClick()
	{
		Destroy(gameObject);
	}
}
