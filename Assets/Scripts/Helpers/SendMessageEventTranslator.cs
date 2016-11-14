using UnityEngine;
using System.Collections;

public class SendMessageEventTranslator : MonoBehaviour 
{
	[SerializeField]
	GameObject inputTarget;

	[SerializeField]
	string message;


	void OnClick()
	{
		inputTarget.SendMessage(message, SendMessageOptions.DontRequireReceiver);
	}
}
