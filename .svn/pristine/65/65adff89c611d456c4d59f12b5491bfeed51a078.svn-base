using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StatManager : MonoBehaviour 
{
	private static StatManager _instance;
	public static StatManager Instance { get { return _instance ?? (_instance = FindObjectOfType<StatManager>()); } }
	protected StatManager() { _instance = null; }

	public float StatChangeSpeed = 50;

	PlayerInfo playerInfo;

	string body_locale;
	Text BodyText;
	Text BodyValueText;
	Image BodyChangeIcon;
	string charisma_locale;
	Text CharismaText;
	Text CharismaValueText;
	Image CharismaChangeIcon;
	string mind_locale;
	Text MindText;
	Text MindValueText;
	Image MindChangeIcon;
	string inmaterep_locale;
	Text InmateRepText;
	Text InmaterepValueText;
	Image InmaterepChangeIcon;
	string guardrep_locale;
	Text GuardRepText;
	Text GuardrepValueText;
	Image GuardrepChangeIcon;
	//string cash_locale;
	Text cash_s;

	Text EnergyText;
	Text HealthText;

//	float transBody;
//	float transMind;
//	float transChar;
//	float transInmr;
//	float transGrep;
//	float transCash;
//	float transEnrg;
//	float transHp;

	// Use this for initialization
	void Awake () 
	{
		playerInfo = PlayerInfo.Instance;//GetComponent<PlayerInfo>();

		//cash_locale = Localization.Instance.GetLocale(54);

		cash_s = GameObject.Find("CashDisplay").GetComponent<Text>(); //34

		//GameObject.Find("QuickStatsLabel").GetComponent<Text>().text = Localization.Instance.GetLocale(55) + ": ";
		BodyText = GameObject.Find("QuickBodyText").GetComponent<Text>();
		BodyValueText = GameObject.Find("QuickBodyValueText").GetComponent<Text>();
		BodyChangeIcon = GameObject.Find("QuickBodyChangeIcon").GetComponent<Image>();
		CharismaText = GameObject.Find("QuickCharismaText").GetComponent<Text>();
		CharismaValueText = GameObject.Find("QuickCharismaValueText").GetComponent<Text>();
		CharismaChangeIcon = GameObject.Find("QuickCharismaChangeIcon").GetComponent<Image>();
		MindText = GameObject.Find("QuickMindText").GetComponent<Text>();
		MindValueText = GameObject.Find("QuickMindValueText").GetComponent<Text>();
		MindChangeIcon = GameObject.Find("QuickMindChangeIcon").GetComponent<Image>();
		InmateRepText = GameObject.Find("QuickInmateRepText").GetComponent<Text>();
		InmaterepValueText = GameObject.Find("QuickInmaterepValueText").GetComponent<Text>();
		InmaterepChangeIcon = GameObject.Find("QuickInmaterepChangeIcon").GetComponent<Image>();
		GuardRepText = GameObject.Find("QuickGuardRepText").GetComponent<Text>();
		GuardrepValueText = GameObject.Find("QuickGuardrepValueText").GetComponent<Text>();
		GuardrepChangeIcon = GameObject.Find("QuickGuardrepChangeIcon").GetComponent<Image>();
		EnergyText = GameObject.Find("DisplayEnergyText").GetComponent<Text>();
		HealthText = GameObject.Find("DisplayHealthText").GetComponent<Text>();

		body_locale = Localization.Instance.GetLocale(57);
		mind_locale = Localization.Instance.GetLocale(59);
		charisma_locale = Localization.Instance.GetLocale(58);
		inmaterep_locale = Localization.Instance.GetLocale(61);
		guardrep_locale = Localization.Instance.GetLocale(60);

		BodyChangeIcon.gameObject.SetActive (false);
		CharismaChangeIcon.gameObject.SetActive (false);
		MindChangeIcon.gameObject.SetActive (false);
		InmaterepChangeIcon.gameObject.SetActive (false);
		GuardrepChangeIcon.gameObject.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
//		if(Mathf.FloorToInt(transBody) < Mathf.FloorToInt(playerInfo.body))
//		{
//			transBody += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transBody) > Mathf.FloorToInt(playerInfo.body))
//		{
//			transBody -= Time.deltaTime*StatChangeSpeed;
//		}
//
//		if(Mathf.FloorToInt(transMind) < Mathf.FloorToInt(playerInfo.mind))
//		{
//			transMind += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transMind) > Mathf.FloorToInt(playerInfo.mind))
//		{
//			transMind -= Time.deltaTime*StatChangeSpeed;
//		}
//
//		if(Mathf.FloorToInt(transChar) < Mathf.FloorToInt(playerInfo.charisma))
//		{
//			transChar += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transChar) > Mathf.FloorToInt(playerInfo.charisma))
//		{
//			transChar -= Time.deltaTime*StatChangeSpeed;
//		}
//
//		if(Mathf.FloorToInt(transInmr) < Mathf.FloorToInt(playerInfo.inmate_rep))
//		{
//			transInmr += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transInmr) > Mathf.FloorToInt(playerInfo.inmate_rep))
//		{
//			transInmr -= Time.deltaTime*StatChangeSpeed;
//		}
//
//		if(Mathf.FloorToInt(transGrep) < Mathf.FloorToInt(playerInfo.guard_rep))
//		{
//			transGrep += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transGrep) > Mathf.FloorToInt(playerInfo.guard_rep))
//		{
//			transGrep -= Time.deltaTime*StatChangeSpeed;
//		}
//
//		if(Mathf.FloorToInt(transEnrg) < Mathf.FloorToInt(playerInfo.energy))
//		{
//			transEnrg += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transEnrg) > Mathf.FloorToInt(playerInfo.energy))
//		{
//			transEnrg -= Time.deltaTime*StatChangeSpeed;
//		}
//
//		if(Mathf.FloorToInt(transHp) < Mathf.FloorToInt(playerInfo.health))
//		{
//			transHp += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transHp) > Mathf.FloorToInt(playerInfo.health))
//		{
//			transHp -= Time.deltaTime*StatChangeSpeed;
//		}
//
//		if(Mathf.FloorToInt(transCash) < Mathf.FloorToInt(playerInfo.cash))
//		{
//			transCash += Time.deltaTime*StatChangeSpeed;
//		}
//		else if(Mathf.FloorToInt(transCash) > Mathf.FloorToInt(playerInfo.cash))
//		{
//			transCash -= Time.deltaTime*StatChangeSpeed;
//		}

		UpdateStats();


	}

	public void ChangeStat(string stat_type, float _value){

		switch(stat_type)
		{
		case "energy":
			if (_value > 0)
				_value = _value * (1 + playerInfo.CheckTrait ("quick_recovery", true) + playerInfo.CheckTrait ("slow_recovery", true));
			playerInfo.energy += _value;
			if (playerInfo.energy > playerInfo.health)
				playerInfo.energy = playerInfo.health;
			if (playerInfo.energy < 0)
				playerInfo.energy = 0;
			EnergyText.GetComponent<GUIEffect> ().status = true;
			break;
		case "health":
			if(_value < 0) _value = _value*(1+playerInfo.CheckTrait("tough",true));
			playerInfo.health += _value;
			if (playerInfo.health > playerInfo.max_health) playerInfo.health = playerInfo.max_health;
			if (playerInfo.health < 0) playerInfo.health = 0;
			HealthText.GetComponent<GUIEffect> ().status = true;
			break;
		case "body":
			playerInfo.body += _value;
			if (playerInfo.body > playerInfo.max_str) playerInfo.body = playerInfo.max_str;
			if (playerInfo.body < 0) playerInfo.body = 0;
			GUIController.Instance.CreateParameterChangeEffect(BodyText.gameObject,_value);
			ActivateChangeIcon (_value, BodyChangeIcon);
			break;
		case "charisma":
			playerInfo.charisma += _value;
			if (playerInfo.charisma > playerInfo.max_dex) playerInfo.charisma = playerInfo.max_dex;
			if (playerInfo.charisma < 0) playerInfo.charisma = 0;
			GUIController.Instance.CreateParameterChangeEffect(CharismaText.gameObject,_value);
			ActivateChangeIcon (_value, CharismaChangeIcon);
			break;
		case "mind":
			if(_value > 0) _value =  _value*(1+playerInfo.CheckTrait("quick_learning",true)+playerInfo.CheckTrait("slow_learning",true));
			playerInfo.mind += _value;
			if (playerInfo.mind > playerInfo.max_int) playerInfo.mind = playerInfo.max_int;
			if (playerInfo.mind < 0) playerInfo.mind = 0;
			GUIController.Instance.CreateParameterChangeEffect(MindText.gameObject,_value);
			ActivateChangeIcon (_value, MindChangeIcon);
			break;
		case "cash":
			playerInfo.cash += _value;
			if(playerInfo.cash < 0) playerInfo.cash = 0;
			if(_value > 0) AudioManager.Instance.Play(3); 
			GUIController.Instance.CreateParameterChangeEffect(cash_s.gameObject,_value);
			break;
		case "inmate_rep":
			playerInfo.inmate_rep += _value;
			if(playerInfo.inmate_rep < -200) playerInfo.inmate_rep = -200;
			if(playerInfo.inmate_rep > 200) playerInfo.inmate_rep = 200;
			if(_value != 0) GUIController.Instance.CreateParameterChangeEffect(InmateRepText.gameObject,_value);
			ActivateChangeIcon (_value, InmaterepChangeIcon);
			break;
		case "guard_rep":
			playerInfo.guard_rep += _value;
			if(playerInfo.guard_rep < -200) playerInfo.guard_rep = -200;
			if(playerInfo.guard_rep > 200) playerInfo.guard_rep = 200;
			GUIController.Instance.CreateParameterChangeEffect(GuardRepText.gameObject,_value);
			ActivateChangeIcon (_value, GuardrepChangeIcon);
			break;
		case "tunnel":
			playerInfo.tunnel += _value;
			if(playerInfo.tunnel < 0) playerInfo.tunnel = 0;
			if(playerInfo.tunnel > 100) playerInfo.tunnel = 100;
			break;
		}
		Debug.Log("Change " + stat_type + " " + _value);
	}

	void ActivateChangeIcon(float value, Image icon)
	{
		icon.gameObject.SetActive(true);
		icon.transform.localRotation = Quaternion.identity;
		icon.GetComponent<GUIEffect> ().timer = 2;

		if (value > 0) 
		{
			icon.color = new Color32 (0,255,0,255);
		} else 
		{
			icon.color = new Color32 (255,0,0,255);
			icon.transform.Rotate (0,0,180);
		}
	}

	void UpdateStats()
	{
//		BodyText.text = body_locale + " " + Mathf.FloorToInt(transBody);
//		MindText.text = mind_locale + " " + Mathf.FloorToInt(transMind);
//		CharismaText.text = charisma_locale + " " + Mathf.FloorToInt(transChar);
//		InmateRepText.text = inmaterep_locale + " " + Mathf.FloorToInt(transInmr);
//		GuardRepText.text = guardrep_locale + " " + Mathf.FloorToInt(transGrep);
//		cash_s.text = cash_locale + " " + Mathf.FloorToInt(transCash) + "$";
//		EnergyText.text = Mathf.FloorToInt(transEnrg).ToString();
//		HealthText.text = Mathf.FloorToInt(transHp).ToString();

		BodyText.text = body_locale + ":";
		BodyValueText.text = Mathf.FloorToInt(playerInfo.body) + "/" + playerInfo.max_str;
		MindText.text = mind_locale + ":";
		MindValueText.text = Mathf.FloorToInt(playerInfo.mind) + "/" + playerInfo.max_int;
		CharismaText.text = charisma_locale + ":";
		CharismaValueText.text = Mathf.FloorToInt(playerInfo.charisma) + "/" + playerInfo.max_dex;
		InmateRepText.text = inmaterep_locale + ":";
		InmaterepValueText.text = Mathf.FloorToInt(playerInfo.inmate_rep) + "/200";
		GuardRepText.text = guardrep_locale + ":";
		GuardrepValueText.text = Mathf.FloorToInt(playerInfo.guard_rep) + "/200";
		cash_s.text = Mathf.FloorToInt(playerInfo.cash).ToString();
		EnergyText.text = Mathf.FloorToInt(playerInfo.energy).ToString();
		HealthText.text = Mathf.FloorToInt(playerInfo.health).ToString();
	}

	public void GetData()
	{
//		transBody = playerInfo.body;
//		transMind = playerInfo.mind;
//		transChar = playerInfo.charisma;
//		transInmr = playerInfo.inmate_rep;
//		transGrep = playerInfo.guard_rep;
//		transCash = playerInfo.cash;
//		transEnrg = playerInfo.energy;
//		transHp = playerInfo.health;
		UpdateStats();
	}

}
