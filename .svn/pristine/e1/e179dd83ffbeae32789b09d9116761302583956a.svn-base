using UnityEngine;
using System.Collections;

public class ActionCounter : MonoBehaviour
{
	[SerializeField]
	string saveKey;

	int counter = 0;

	public int Counter { get { return counter; } set { counter = value; Save(); } }

	void Start()
	{
		if (!string.IsNullOrEmpty(saveKey))
			counter = SaveLoadXML.GetValue<int>(saveKey, 0);
		SendMessage("DisplayDigitalValue", counter, SendMessageOptions.DontRequireReceiver);
	}

	public void Add(int numberToAdd = 1)
	{
		counter += numberToAdd;
		Save();
		SendMessage("DisplayDigitalValue", counter, SendMessageOptions.DontRequireReceiver);
	}
	public void Reset()
	{
		counter = 0;
		Save();
		SendMessage("DisplayDigitalReset", counter, SendMessageOptions.DontRequireReceiver);
	}

	void Save()
	{
		if (!string.IsNullOrEmpty(saveKey))
			SaveLoadXML.SetValue<int>(saveKey, counter);
	}
}