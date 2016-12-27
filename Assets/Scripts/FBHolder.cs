using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FBHolder : MonoBehaviour {
	public Button FBLoginButton;
	//public Button FBPostButton;
	//public GameObject FBInviteButton;

	void Awake(){
		Debug.Log ("Awake()");
		FB.Init (SetInit, onHideUnity);
	}

	private void SetInit(){
		Debug.Log ("FB Init Done");

		if(FB.IsLoggedIn){
			Debug.Log ("FB Logged In");
			manageFBUI(true);
		}else{
			manageFBUI(false);
		}
	}

	private void onHideUnity(bool isGameShown){
		if (!isGameShown) {
			Debug.Log ("Pause Game");
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void FBLogin()
	{
		FBLoginButton.interactable = false;
		StartCoroutine(checkInternetConnection((isConnected)=>{
			if(isConnected)
			{
				var perms = new List<string>(){"public_profile", "email", "user_friends"};
				FB.LogInWithReadPermissions(perms, AuthCallback);
			}
			else
			{
				ScreenManager.Instance.CreateScreen("HintPanel");
				ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(Localization.Instance.GetLocale(2496));
			
			}
			FBLoginButton.interactable = true;
		}));
	}

	void AuthCallback(ILoginResult result)
	{
		if(FB.IsLoggedIn){
			Debug.Log("FB Login Worked");

			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}

			manageFBUI(true);
		}else{
			Debug.Log("FB Login Failed");
			manageFBUI(false);
		}
	}

	public void FBPost(){
		FB.FeedShare(
			string.Empty,
			null,
			"Post Title",
			"Post Caption",
			"Post Description",
			new Uri("http://i.imgur.com/zkYlB.jpg"),
			string.Empty,
			callback: ShareCallback
		);
	}

	private void ShareCallback (IShareResult result) {
		if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) {
			Debug.Log("ShareLink Error: "+result.Error);
		} else if (!String.IsNullOrEmpty(result.PostId)) {
			// Print post identifier of the shared content
			Debug.Log(result.PostId);
		} else {
			// Share succeeded without postID
			Debug.Log("ShareLink success!");
		}
	}

	void manageFBUI(bool isLoggedIn){
		if (isLoggedIn) {
			FBLoginButton.gameObject.SetActive(false);
			//FBPostButton.gameObject.SetActive(true);
		} else {
			FBLoginButton.gameObject.SetActive(true);
			//FBPostButton.gameObject.SetActive(false);
		}
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
