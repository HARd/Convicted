using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Facebook.Unity;
using System;

public class IllustrationScreen : ScreenBase  
{
	public GameObject bg;
	public Text fb_bttn_text;

	Image imageScreen;
	string postDescription;
	public string url {set; get;}

	void Start()
	{
		imageScreen = GetComponent<Image>();
		imageScreen.enabled = false;
		bg.SetActive(false);
		postDescription = Localization.Instance.GetLocale(UnityEngine.Random.Range(2498, 2503));
		fb_bttn_text.text = Localization.Instance.GetLocale(2497);
	}

	void Update()
	{
		if(!imageScreen.enabled && EventManager.Instance.current_event.tag == "GeneralEvent")
		{
			AudioManager.Instance.Play(3); 
			imageScreen.enabled = true;
			bg.SetActive(true);
		}
	}


	public void FBPost()
	{
		destroyOnClick = false;
		AudioManager.Instance.Play(1);
		StartCoroutine(checkInternetConnection((isConnected)=>{
			if(isConnected)
			{
				FB.FeedShare(
					string.Empty,
					new Uri("https://www.facebook.com/convictedapp/"),
					"Convicted: Jail Break",
					Localization.Instance.GetLocale(2504),
					postDescription,
					new Uri(url),
					string.Empty,
					callback: ShareCallback
				);


//				FB.FeedShare(
//					string.Empty,
//					new Uri("https://www.facebook.com/games/?fbs=106&app_id=686612761494023"),
//					"", //Convicted: Jail Break",
//					"Convicted: Jail Break - " + postDescription,
//					" ",
//					new Uri(url),
//					string.Empty,
//					callback: ShareCallback
//				);
			}
			else
			{
				ScreenManager.Instance.CreateScreen("HintPanel");
				ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(Localization.Instance.GetLocale(2496));
			}
			//destroyOnClick = true;
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
		Destroy(gameObject);
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
