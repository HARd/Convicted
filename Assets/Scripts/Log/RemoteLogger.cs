using UnityEngine;
using System.Collections;
using System.IO;

public class RemoteLogger : MonoBehaviour 
{

	string url = "https://script.google.com/macros/s/AKfycbzX-f-BYTJlcs82wS9NPUsoi_XCC9f-bEloAXcMWtqbW_a80iV9/exec"; // Your URL copy here

		
	string UrlEncode(string instring)
	{
		StringReader strRdr = new StringReader(instring);
		StringWriter strWtr = new StringWriter();
		int charValue = strRdr.Read();
		while (charValue != -1)
		{
			if (((charValue >= 48) && (charValue <= 57)) // 0-9
				|| ((charValue >= 65)  && (charValue <= 90)) // A-Z
				|| ((charValue >= 97)  && (charValue <= 122))) // a-z
			{
				strWtr.Write((char) charValue);
			}
			else if (charValue == 32) // Space
			{
				strWtr.Write("+");
			}
			else
			{
				strWtr.Write("%{0:x2}", charValue);
			}
			charValue = strRdr.Read();
		}
		return strWtr.ToString();
	}

	void SendLog(string mes) 
	{
		string t_url = url + "?p=" + UrlEncode(mes);
		WWW www = new WWW(t_url);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null) 
		{
			//Debug.Log("Ok");
		} 
		else 
		{
			//Debug.Log("Error");
		}
	}

	void OnEnable()
	{
		Application.logMessageReceived += HandleLog;
	}

	void OnDisable() 
	{
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog(string logString, string stackTrace, LogType type) {
		SendLog(logString);
	}	
}