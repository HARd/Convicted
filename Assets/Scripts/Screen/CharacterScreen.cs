﻿using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterScreen : ScreenBase 
{
	[SerializeField]
	Image CharacterImage;

	public void Awake()
	{
		SetCharacterImage(GameData.current.currentCharacterID);
	}

	public void SetCharacterImage(int id)
	{
		CharacterImage.sprite = SpriteManager.Instatce.GetSprite("Pers/pers_" + id + "_big");
	}
}
