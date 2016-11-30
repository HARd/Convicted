using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterTierController : MonoBehaviour 
{
	[SerializeField]
	Text DescriptionDisplay;

	[SerializeField]
	Image CharacterImage;

	[SerializeField]
	Button SelectTierButton;
	[SerializeField]
	Text SelectButtonText;

	[SerializeField]
	Button CharacterZoomIcon;

	[SerializeField]
	Image CharacterLockIcon;

	public void NewGame(int characterNumber)
	{
		if (MainMenu.Instance.charactersList[characterNumber].image_id >= 0) 
			CharacterImage.sprite = SpriteManager.Instatce.GetSprite("Pers/pers_" + characterNumber);
		else 
			CharacterImage.sprite = SpriteManager.Instatce.GetSprite("Pers/char_coming_soon");;

		DescriptionDisplay.text = Localization.Instance.GetLocale(MainMenu.Instance.charactersList[characterNumber].tier_desc_id);

		SelectTierButton.gameObject.SetActive (false);
		CharacterZoomIcon.gameObject.SetActive (false);
		CharacterLockIcon.gameObject.SetActive (false);

		if (characterNumber == 0 || GameData.current.HasCharacterCompleted(characterNumber - 1))
		{
			SelectTierButton.gameObject.SetActive (true);
			SelectButtonText.text = (SaveLoadXML.HasKey("PLAYER_INFO") && characterNumber == GameData.current.currentCharacterID) ? Localization.Instance.GetLocale(32) : Localization.Instance.GetLocale(645);
		} 
		else if (MainMenu.Instance.charactersList[characterNumber].image_id >= 0) 
			CharacterZoomIcon.gameObject.SetActive (true);
		else 
			CharacterLockIcon.gameObject.SetActive (true);
	}
}
