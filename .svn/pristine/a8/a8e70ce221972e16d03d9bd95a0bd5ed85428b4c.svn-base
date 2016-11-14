/*
	Текстовый счетчик для тулзов.
*/


using UnityEngine;


public class ToolCounter : MonoBehaviour
{
	[SerializeField]
	UnityEngine.UI.Text textField;

	[SerializeField]
	GameObject bg_text;

	int counter = 1;

	public int Counter { get { return counter; } }

	public void Add()
	{
		bg_text.SetActive(true);
		//textField.enabled = true;
		textField.text =  (++counter).ToString();
	}

	public void Less()
	{
		textField.text =  (--counter).ToString();
		bg_text.SetActive(counter > 1);
	}
}
