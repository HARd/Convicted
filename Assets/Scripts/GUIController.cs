using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
//using System;

[System.Serializable]
public class EffectData
{
	public GameObject target;
	public float value;
}

public class GUIController : MonoBehaviour 
{

	private static GUIController _instance;
	public static GUIController Instance { get { return _instance ?? (_instance = FindObjectOfType<GUIController>()); } }
	protected GUIController() { _instance = null; }


	public GameObject canvas;
	public GameObject text_effect;
	public GameObject cig_effect;

	List<EffectData> TextEffectQueue = new List<EffectData>();
	float effect_timer = 0;
	
	void Start()
	{
		// Выставляем ежедневный бонус для новых игроков используя уникальный опыт индуских кодеров
		//if (SaveLoadXML.HasKey("PLAYER_INFO") && PlayerInfo.Instance.day == 0 || GameData.current.nextDailyBonusTime == new System.DateTime()) 
		if (GameData.current.nextDailyBonusTime == new System.DateTime()) 
		{
			//print(" GameData.current.nextDailyBonusTime = " + GameData.current.nextDailyBonusTime + "  " + new System.DateTime());
			GameData.current.nextDailyBonusTime = System.DateTime.Now.Add (new System.TimeSpan (0, 5, 0));
			//GameData.current.nextDailyBonusTime = System.DateTime.Now.Add (new System.TimeSpan (0, 0, 30));
		}

	}

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape) && !PlayerInfo.Instance.isTutorial && !PanelManager.Instance.EventPanel.gameObject.activeSelf)
		{
			if(!PanelManager.Instance.IsActionPanel())
				PanelManager.Instance.CloseButton();
			else if(Inventory.Instance.IsVisible)
				Inventory.Instance.GetComponent<PanelElementMover>().ChangeShow();
			else if(ScreenManager.Instance.current != null)
				Destroy(ScreenManager.Instance.current);
			else
				CreateExiteMainMenuDialog();
		}

		if(effect_timer > 0) 
			effect_timer -= Time.deltaTime;

		if(TextEffectQueue.Count > 0 && effect_timer <= 0 && EventManager.Instance.current_event.tag == "GeneralEvent")
		{
			GameObject effect = Instantiate(text_effect);
			effect.transform.SetParent(canvas.transform,false);
			effect.transform.position = TextEffectQueue[0].target.transform.position;
			effect.GetComponent<TextEffect>().destination = new Vector3(TextEffectQueue[0].target.transform.position.x,TextEffectQueue[0].target.transform.position.y + 30, TextEffectQueue[0].target.transform.position.z);
			effect.GetComponent<Text>().text = "";
			if(TextEffectQueue[0].value > 0)
			{
				effect.GetComponent<Text>().text += "+";
				effect.GetComponent<Text>().color = new Color32(20,120,50,255);
			} 
			else if(TextEffectQueue[0].value < 0) 
				effect.GetComponent<Text>().color = new Color32(120,20,20,255);
			
			effect.GetComponent<Text>().text += TextEffectQueue[0].value.ToString();

			TextEffectQueue.RemoveAt(0);
			effect_timer = 0.2f;
		}

		if (GameData.current.nextDailyBonusTime <= System.DateTime.Now && !PlayerInfo.Instance.isTutorial && !PanelManager.Instance.EventPanel.gameObject.activeSelf) 
		{
			if(ScreenManager.Instance.current == null)
				ScreenManager.Instance.CreateScreen("DailyBonusPanel");
		}
	}
		
	public void CreateParameterChangeEffect(GameObject _object,float value) 
	{
		EffectData newdata = new EffectData();
		newdata.target = _object;
		newdata.value = value;
		TextEffectQueue.Add(newdata);
	}

	public void CreateChangeEffect(float value) 
	{
		GameObject effect = Instantiate(cig_effect);
		effect.transform.SetParent(PanelManager.Instance.ActionPanel.CashDisplayPanel.transform,false);
		effect.GetComponentInChildren<Text>().text = "+ " + value;
		PanelManager.Instance.ActionPanel.cashBlocker = true;
		effect.GetComponentInChildren<AnimatorDestroer>().onDestroy += ()=>{PanelManager.Instance.ActionPanel.cashBlocker = false;};
	}

	public void ShowHint(GameObject _object)
	{
		ScreenManager.Instance.CreateScreen("HintPanel");
		ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(_object.GetComponent<QuestController>().hintText);
	}

	public void CreateExiteMainMenuDialog()
	{
		AudioManager.Instance.Play(1);
		ScreenManager.Instance.CreateScreen("HintPanel");
		ScreenManager.Instance.current.GetComponent<Hint>().ShowDialog(Localization.Instance.GetLocale(898), Localization.Instance.GetLocale(76), (confirm)=>
			{
				if(confirm)
				{
					if(PlayerInfo.Instance.day > 0)
						PlayerInfo.Instance.SaveGame();	

					SaveLoadXML.SaveGameDataXML();
					PlayerInfo.Instance.SaveGame();
					SceneManager.LoadScene(1);
				}
			});
	}

}
