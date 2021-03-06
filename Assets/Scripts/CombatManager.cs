﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Enemy
{
	public float health;
	public float strength;
	public float deadly;
	public float skilled_fighter;

	public float weaponDamage;
}

public class CombatManager : MonoBehaviour 
{

	private static CombatManager _instance;
	public static CombatManager Instance { get { return _instance ?? (_instance = FindObjectOfType<CombatManager>()); } }
	protected CombatManager() { _instance = null; }

	//float health;
	float strength;
	float deadly;
	float skilled_fighter;

	float weaponDamage;

	string[] description;

	string CombatResult;
	List<Enemy> enemyList = new List<Enemy>();


	Text descriptionText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResolveCombat(string enemy_type, int enemy_count)
	{
		enemyList.Clear();
		CombatResult = "";

		GetPlayerStats();
		GenerateEnemy(enemy_type, enemy_count);

		for(int i = 0; i < enemyList.Count; i++)
		{
			if(CombatResult != "defeat") RunCombat(i);
		}

		description = new string[3];

		if(enemy_count == 1)
		{
			description[0] = Localization.Instance.GetLocale(852) + " ";

			float enemy_str = enemyList [0].weaponDamage + Mathf.FloorToInt (enemyList [0].strength) + enemyList [0].deadly + enemyList [0].skilled_fighter;
			switch(enemy_type)
			{
			case "weak":
				description[1] = Localization.Instance.GetLocale(853) + " [" + Localization.Instance.GetLocale(181) + ": " + enemy_str +  "]";
				break;
			case "medium":
				description[1] = Localization.Instance.GetLocale(854) + " [" + Localization.Instance.GetLocale(181) + ": " + enemy_str +  "]";
				break;
			case "strong":
				description[1] = Localization.Instance.GetLocale(855) + " [" + Localization.Instance.GetLocale(181) + ": " + enemy_str +  "]";
				break;
			case "deadly":
				description[1] = Localization.Instance.GetLocale(856) + " [" + Localization.Instance.GetLocale(181) + ": " + enemy_str +  "]";
				break;
			}

			if(CombatResult == "victory") description[2] = ". " + Localization.Instance.GetLocale(857);
			else description[2] = ". " + Localization.Instance.GetLocale(858);
		}
		else
		{
			description[0] = Localization.Instance.GetLocale(859);

			if(CombatResult == "victory") description[2] = "\n" + Localization.Instance.GetLocale(860);
			else description[2] = "\n" + Localization.Instance.GetLocale(861);
		}
			

		descriptionText = GameObject.Find("DescriptionText").GetComponent<Text>();
		switch(CombatResult)
		{
		case "victory":
			descriptionText.text = "";
			foreach(string desc in description)
			{
				descriptionText.text += desc;
			}

			new health("","",-5).ChangeStat();
			EventManager.Instance.CreateNext(CombatResult);
			break;
		case "defeat":
			descriptionText.text = "";
			foreach(string desc in description)
			{
				descriptionText.text += desc;
			}

			if(EventManager.Instance.current_event.tag == "SingleCombat" || EventManager.Instance.current_event.tag == "GroupCombat")
			{
				new health("","",-20).ChangeStat();
			}
			else
			{
				new health("","",-200).ChangeStat();
			} 
			EventManager.Instance.CreateNext(CombatResult);
			break;
		}
		AudioManager.Instance.Play(2);
	}

	void GetPlayerStats()
	{
		//health = PlayerInfo.Instance.health;
		strength = PlayerInfo.Instance.body;

		deadly = PlayerInfo.Instance.CheckTrait("deadly",true);
		skilled_fighter = PlayerInfo.Instance.CheckTrait("skilled_fighter",true);

		weaponDamage = Inventory.Instance.GetMaxDamage();
	}

	void GenerateEnemy(string enemy_type, int enemy_count)
	{
		for(int i = 0; i < enemy_count; i++)
		{
			Enemy new_enemy = new Enemy();
		
			if(enemy_count > 1) enemy_type = "medium";

			// Усли игрок проходит игру первым персонажем, то мы упрощаем противников, которых мы генерируем
			if(GameData.current.currentCharacterID == 0)
			{
				switch (enemy_type) {
				case "medium":
					enemy_type = "weak";
					break;
				case "strong":
					enemy_type = "weak";
					break;
				case "deadly":
					enemy_type = "medium";
					break;
				}
			}


			switch(enemy_type)
			{
			case "weak":
				new_enemy.health = 100;
				new_enemy.strength = Mathf.FloorToInt(Random.Range(20,40));

				if(Random.Range (0,100) <= 10) // increase from 0% (v1.06)
					new_enemy.weaponDamage = PlayerInfo.Instance.GetRandomWeapon().damage;

				break;
			case "medium":
				new_enemy.health = 100;
				new_enemy.strength = Mathf.FloorToInt(Random.Range(40,60));
					
				if(Random.Range (0,100) <= 10) 
					new_enemy.skilled_fighter = PlayerInfo.Instance.GetTrait("skilled_fighter");

				if(Random.Range (0,100) <= 20) // increase from 10% (v1.06)
					new_enemy.weaponDamage = PlayerInfo.Instance.GetRandomWeapon().damage;
					
				break;
			case "strong":
				new_enemy.health = 100;
				new_enemy.strength = Mathf.FloorToInt(Random.Range(60,80));
					
				if(Random.Range (0,100) <= 30) 
					new_enemy.skilled_fighter = PlayerInfo.Instance.GetTrait("skilled_fighter"); // increase from 20% (v1.06)

				if(Random.Range (0,100) <= 35) // increase from 20% (v1.06)
					new_enemy.weaponDamage = PlayerInfo.Instance.GetRandomWeapon().damage;
					
				break;
			case "deadly":
				new_enemy.health = 100;
				new_enemy.strength = Mathf.FloorToInt(Random.Range(80,100));
					
				if(Random.Range (0,100) <= 50) 
					new_enemy.skilled_fighter = PlayerInfo.Instance.GetTrait("skilled_fighter");
				
				if(Random.Range (0,100) <= 25) 
					new_enemy.deadly = PlayerInfo.Instance.GetTrait("deadly");

				if(Random.Range (0,100) <= 50)
					new_enemy.weaponDamage = PlayerInfo.Instance.GetRandomWeapon().damage;
					
				break;
			}
			enemyList.Add(new_enemy);
		}
	}

	void RunCombat(int i)
	{
		bool resolved = false;
		while(resolved == false)
		{
			float player_damage = weaponDamage + Mathf.FloorToInt(strength) + skilled_fighter + deadly + PlayerInfo.Instance.CheckTrait("john_friend",true);
			float group_bonus = 10*((enemyList.Count - 1));
			if(group_bonus < 0) group_bonus = 0;
			float enemy_damage = enemyList[i].weaponDamage + Mathf.FloorToInt(enemyList[i].strength) + enemyList[i].deadly + enemyList[i].skilled_fighter + group_bonus;

			if(player_damage < enemy_damage)
			{
				resolved = true;
				CombatResult = "defeat";
			}
			else if(player_damage >= enemy_damage)
			{
				resolved = true;
				CombatResult = "victory";
			}
			Debug.Log("Player damage - " + player_damage + "  Enemy damage - " + enemy_damage);
		}
	}

}
