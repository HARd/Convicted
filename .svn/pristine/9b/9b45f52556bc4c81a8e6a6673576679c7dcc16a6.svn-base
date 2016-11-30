using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IllustrationScreen : ScreenBase  
{
	public GameObject bg;

	Image imageScreen;

	void Start()
	{
		imageScreen = GetComponent<Image>();
		imageScreen.enabled = false;
		bg.SetActive(false);
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
}
