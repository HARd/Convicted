using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterScreen : ScreenBase 
{
	[SerializeField]
	Image CharacterImage;

	public void Start()
	{
		CharacterImage.sprite = SpriteManager.Instatce.GetSprite("Pers/pers_" + PlayerInfo.Instance.charImageID + "_big");
	}
}
