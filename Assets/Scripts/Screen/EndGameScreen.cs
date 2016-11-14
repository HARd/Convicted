using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameScreen : ScreenBase 
{
	[SerializeField]
	Text EndGameText;	

	[SerializeField]
	Image EndGameMainImage;

	[SerializeField]
	Text MiddleTextBlock1;	

	[SerializeField]
	Text MiddleTextBlock2;	

	[SerializeField]
	Text LeftTextBlock1;	

	[SerializeField]
	Text LeftTextBlock2;	

	[SerializeField]
	Text LeftTextBlock3;	

	[SerializeField]
	Text RightTextBlock1;	

	[SerializeField]
	Text RightTextBlock2;	

	public void Start() 
	{
		WorldTime.Instance.pause = true;
		EndGameText.text = Localization.Instance.GetLocale(65);
		//EndGameMainImage.sprite = GUIController.Instance.ItemImageList[40];
		//EndGameMainImage.sprite = SpriteManager.Instatce.GetSprite("Images/Items/endgame_escape");

		MiddleTextBlock1.text = Localization.Instance.GetLocale(863);
		MiddleTextBlock2.text = Localization.Instance.GetLocale(864);
		LeftTextBlock1.text = Localization.Instance.GetLocale(867);
		LeftTextBlock2.text = Localization.Instance.GetLocale(868);
		LeftTextBlock3.text = Localization.Instance.GetLocale(869);
		RightTextBlock1.text = Localization.Instance.GetLocale(870);
		RightTextBlock2.text = Localization.Instance.GetLocale(871);

		//GameData.current.characterPoints += 20;
		//SaveLoadXML.SaveGameDataXML();
	}
	public void OnButtonClick()
	{
		//Destroy(gameObject);
		//PanelManager.Instance.ReturnToMenu();
		SaveLoadXML.DeleteGameXML();
		SceneManager.LoadScene(1);
	}
}
