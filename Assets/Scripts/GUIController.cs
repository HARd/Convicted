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

	//public List<Sprite> CharImageList;
	//public List<Sprite> IconImageList;

	List<EffectData> TextEffectQueue = new List<EffectData>();
	float effect_timer = 0;
	
	void Start()
	{


//		GameObject.Find("CharacterImage").GetComponent<Image>().sprite = CharImageList[PlayerInfo.Instance.charImageID];
//		GameObject.Find("CharacterNumberText").GetComponent<Text>().text = PlayerInfo.Instance.characterNumber;




		//ApplyResolutionFix();

		// Выставляем ежедневный бонус для новых игроков используя уникальный опыт индуских кодеров
		if (SaveLoadXML.HasKey("PLAYER_INFO") && PlayerInfo.Instance.day == 0 || GameData.current.nextDailyBonusTime == new System.DateTime()) 
		{
			GameData.current.nextDailyBonusTime = System.DateTime.Now.Add (new System.TimeSpan (0, 5, 0));
			//GameData.current.nextDailyBonusTime = System.DateTime.Now.Add (new System.TimeSpan (0, 0, 30));
		}

	}

	void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape) && !PlayerInfo.Instance.isTutorial)
		{
			if(PanelManager.Instance.ReturnButton.gameObject.activeSelf)
				PanelManager.Instance.CloseButton();
			else if(Inventory.Instance.IsVisible)
			{
				Inventory.Instance.GetComponent<PanelElementMover>().ChangeShow();
			}
			else if(ScreenManager.Instance.current != null)
			{
				Destroy(ScreenManager.Instance.current);
			}
			else
			{
				AudioManager.Instance.Play(1);
				string text = Localization.Instance.GetLocale(898);
				//hint.ShowDialog(text,Localization.Instance.GetLocale(76),2,"");
				ScreenManager.Instance.CreateScreen("HintPanel");
				ScreenManager.Instance.current.GetComponent<Hint>().ShowDialog(text,Localization.Instance.GetLocale(76), (confirm)=>
					{
						if(confirm)
						{
							if(PlayerInfo.Instance.day > 0)
								PlayerInfo.Instance.SaveGame();	
							
							SaveLoadXML.SaveGameDataXML();
							SceneManager.LoadScene(1);
						}
				});

			}
		}

		if(effect_timer > 0) effect_timer -= Time.deltaTime;

		if(TextEffectQueue.Count > 0 && effect_timer <= 0 && EventManager.Instance.current_event.tag == "GeneralEvent"){
			GameObject effect = Instantiate(text_effect);
			effect.transform.SetParent(canvas.transform,false);
			effect.transform.position = TextEffectQueue[0].target.transform.position;
			effect.GetComponent<TextEffect>().destination = new Vector3(TextEffectQueue[0].target.transform.position.x,TextEffectQueue[0].target.transform.position.y + 30, TextEffectQueue[0].target.transform.position.z);
			effect.GetComponent<Text>().text = "";
			if(TextEffectQueue[0].value > 0){
				effect.GetComponent<Text>().text += "+";
				effect.GetComponent<Text>().color = new Color32(20,120,50,255);
			} 
			else if(TextEffectQueue[0].value < 0) effect.GetComponent<Text>().color = new Color32(120,20,20,255);
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





//	public void DropItem(string item_name) 
//	{
//		AudioManager.Instance.Play(0);
//		string text = Localization.Instance.GetLocale(848) + " " + PlayerInfo.Instance.GetItemText(item_name) + "?";
//		ScreenManager.Instance.CreateScreen("HintPanel");
//		Hint hint =  ScreenManager.Instance.current.GetComponent<Hint>();
//		hint.ShowDialog(text,Localization.Instance.GetLocale(849), (confirm)=>
//			{
//				if(confirm)
//				{
//					PlayerInfo.Instance.EquipItem(text,false,false);
//					PlayerInfo.Instance.EquipWeapon(text,false);
//					hint.gameObject.SetActive(false);
//					PanelManager.Instance.CharInfoPanel.DrawCharWindow();
//				}
//			});
//	}
		

	public void CreateParameterChangeEffect(GameObject _object,float value) 
	{
		EffectData newdata = new EffectData();
		newdata.target = _object;
		newdata.value = value;
		TextEffectQueue.Add(newdata);
	}



	public void ShowHint(GameObject _object)
	{
		ScreenManager.Instance.CreateScreen("HintPanel");
		ScreenManager.Instance.current.GetComponent<Hint>().ShowHint(_object.GetComponent<QuestController>().hintText);
	}

//	public void ShowTimeTable()
//	{
//		ScreenManager.Instance.CreateScreen ("TimeTablePanel");
//	}

//	void ApplyResolutionFix()
//	{
//		float hight = Screen.height;
//		float width = Screen.width;
//		float res = hight / width;
//
//		if(res == 4f / 3f){
//			GameObject GlobalScale = GameObject.Find("GlobalScale");
//
//			GlobalScale.transform.localScale = new Vector3(0.8333f,0.8333f,1);
//			GameObject.Find("FillerPanel").transform.localScale = new Vector3(0.8333f,0.8333f,1);
//
//			Vector3 new_pos = new Vector3(0,0,0);
//			GlobalScale.transform.localPosition = new_pos;
//		}
//	}
}
