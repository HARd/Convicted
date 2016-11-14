using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hint : ScreenBase 
{
	[SerializeField]
	GameObject HintBox;
	[SerializeField]
	GameObject ConfirmButton;
	[SerializeField]
	GameObject CancelButton;
	[SerializeField]
	Text ConfirmButtonText;
	[SerializeField]
	Text CancelButtonText;
	[SerializeField]
	Text HintText;

	Action<bool> onPress;

	public void ShowHint(string text)
	{
		ConfirmButton.SetActive(false);
		CancelButton.SetActive(false);
		HintText.text = text;
		destroyOnClick = true;
	}

	public void ShowDialog(string text,string button_text, Action<bool> dConfirm = null)
	{
		destroyOnClick = false;
		if(dConfirm != null)
		{
			onPress = dConfirm;
			ConfirmButton.SetActive(true);
			CancelButton.SetActive(true);
		}

		ConfirmButtonText.text = button_text;
		CancelButtonText.text = Localization.Instance.GetLocale(899);

		HintText.text = text;
	}

	public void OnClickConfirmButton()
	{
		if(onPress != null)
			onPress(true);
		
		Destroy(gameObject);
	}

	public void OnClickCancelButton()
	{
		if(onPress != null)
			onPress(false);
		
		Destroy(gameObject);
	}
}
