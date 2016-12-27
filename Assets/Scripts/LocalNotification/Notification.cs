using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Notification : MonoBehaviour 
{
	public int id = 1; 
	public long delay; 
	public string title = "Title"; 
	public int localizationTitle = 0; 
	public string message = "Long message text";
	public int[] localizationMessage;
	public Color32 bgColor = new Color32(0xff, 0x44, 0x44, 255);
	public bool sound = true;
	public bool vibrate = true;
	public bool lights = true;
	public string bigIcon = "app_icon";
	[SerializeField]
	LocalNotification.NotificationExecuteMode executeMode = LocalNotification.NotificationExecuteMode.Inexact;

	public long timeout; 

	void Start () 
	{
		if(localizationTitle > 0)
			title = Localization.Instance.GetLocale(localizationTitle);
		if (localizationMessage.Length > 0) 
		{
			message = Localization.Instance.GetLocale(localizationMessage[UnityEngine.Random.Range(0,localizationMessage.Length-1)]);
		}
			
	}

	public void SendNotification(long seconds)
	{
		delay = seconds;
		LocalNotification.SendNotification(id, delay, title, message, bgColor, sound, vibrate, lights, bigIcon, executeMode);
	}

	public void SendRepeatingNotification()
	{
		LocalNotification.SendRepeatingNotification(id, delay, timeout, title, message, bgColor, sound, vibrate, lights, bigIcon);
	}

	public void CancelNotification()
	{
		LocalNotification.CancelNotification(id);
	}

	public void CancelAllNotifications()
	{
		LocalNotification.CancelAllNotifications();
	}
}
