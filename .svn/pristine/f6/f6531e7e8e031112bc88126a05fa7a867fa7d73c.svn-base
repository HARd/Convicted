/*
	Классическая тулза из двух картинок.
	Загружается динамически из Resources.
	Автор: Кущ А.Е.
*/


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultTool : Tool, IPointerClickHandler
{
	[SerializeField]
	string invSpriteFormatString = "Tools/{0}/inv";

	[SerializeField]
	GameObject toolScreen;

	Sprite invSprite;

	protected override void SetInvView()
	{
		if (invSprite == null)
			invSprite = SpriteManager.Instatce.GetSprite(System.String.Format(invSpriteFormatString, Name));

		if (invSprite == null)
		{
			Debug.LogWarningFormat("Can not load inv.png for tool {0}. Creating temporary sprite.", Name);
			invSprite = SpriteManager.Instatce.GetSprite("Tools/no_tool/inv");
		}

		GetComponent<Image>().sprite = invSprite;
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		//print("-- OnPointerDown - " + Name);
		GameObject screen = Instantiate(toolScreen, new Vector3(0,800,0), Quaternion.identity) as GameObject;
		screen.GetComponent<ToolScreen>().SetScreenView(invSprite, PlayerInfo.Instance.GetItemText(Name));
	}
}

