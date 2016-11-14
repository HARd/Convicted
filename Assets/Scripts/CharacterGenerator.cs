﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class CharacterGenerator
{
	public static void GenerateCharacter(int id)
	{
		ClearGame();
		PlayerInfo.Instance.body = MainMenu.Instance.charactersList [id].body;
		PlayerInfo.Instance.charisma = MainMenu.Instance.charactersList [id].charisma;
		PlayerInfo.Instance.mind = MainMenu.Instance.charactersList [id].mind;
		PlayerInfo.Instance.inmate_rep = MainMenu.Instance.charactersList [id].inmate_rep;
		PlayerInfo.Instance.guard_rep = MainMenu.Instance.charactersList [id].guard_rep;
		PlayerInfo.Instance.charImageID = MainMenu.Instance.charactersList [id].image_id;
		PlayerInfo.Instance.consealment = MainMenu.Instance.charactersList [id].consealment;

		foreach(string trait in MainMenu.Instance.charactersList [id].traits)
		{
			ChangeTrait(PlayerInfo.Instance.traitList.Find (x => x.name == trait));
		}
	}

	static void ChangeTrait(Trait trait)
	{
		trait.status = true;
		trait.Applay();
	}

	static void ClearGame()
	{
		PlayerInfo.Instance.body = 0;
		PlayerInfo.Instance.charisma = 0;
		PlayerInfo.Instance.mind = 0;

		PlayerInfo.Instance.ResetTraitList();
	}


	static bool CheckExclude(string check)
	{
		return PlayerInfo.Instance.traitList.Exists(t => t.name == check);
	}
}
