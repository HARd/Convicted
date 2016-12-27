using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class EndGameScreen : ScreenBase 
{
	[SerializeField]
	Text EndGameText;	

	[SerializeField]
	Image EndGameMainImage;

	[SerializeField]
	Text MiddleTextBlock;	

	[SerializeField]
	Text Quote;	

	[SerializeField]
	Text LeftTextBlock;	

	[SerializeField]
	Text RightTextBlock;	

	[SerializeField]
	string[] urlPicture;

	bool isBlock = false;

	public void Start() 
	{
		WorldTime.Instance.pause = true;

		EndGameText.text = Localization.Instance.GetLocale("character" + GameData.current.currentCharacterID + "_end");
		MiddleTextBlock.text = Localization.Instance.GetLocale("character" + GameData.current.currentCharacterID + "_middle");
		Quote.text = Localization.Instance.GetLocale("character" + GameData.current.currentCharacterID + "_quote");
		LeftTextBlock.text = Localization.Instance.GetLocale("character" + GameData.current.currentCharacterID + "_left");
		RightTextBlock.text = Localization.Instance.GetLocale("character" + GameData.current.currentCharacterID + "_right");
	}
	public void OnButtonClick()
	{
		if(!isBlock)
		{
			SaveLoadXML.DeleteGameXML();
			SceneManager.LoadScene(1);
		}
	}

	public void FBPost()
	{
		isBlock = true;
		AudioManager.Instance.Play(1);
		StartCoroutine(checkInternetConnection((isConnected)=>{
			if(isConnected)
			{
				FB.FeedShare(
					string.Empty,
					new Uri("https://www.facebook.com/convictedapp/"),
					Localization.Instance.GetLocale("character" + GameData.current.currentCharacterID + "_post"),
					Localization.Instance.GetLocale(2506),
					Localization.Instance.GetLocale(2501),
					new Uri(urlPicture[GameData.current.currentCharacterID]),
					string.Empty,
					callback: ShareCallback
				);
			}
			else
			{
				ScreenManager.Instance.CreateScreen("HintPanel");
				ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(Localization.Instance.GetLocale(2496));
			}
		}));

	}

	private void ShareCallback (IShareResult result) 
	{
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) 
		{
			Debug.Log("ShareLink Error: "+result.Error);
		} 
		else if (!String.IsNullOrEmpty(result.PostId))
		{
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} 
		else 
		{
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
		isBlock = false;
	}

	IEnumerator checkInternetConnection(Action<bool> action)
	{
		WWW www = new WWW("http://google.com");
		yield return www;

		if (www.error != null) 
			action (false);
		else 
			action (true);
	} 
}
