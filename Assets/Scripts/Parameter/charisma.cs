using UnityEngine;

public class charisma : Parameter
{
	public charisma(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void Applay()
	{
		PlayerInfo.Instance.charisma += value;
		Debug.Log("-- Applay() charisma - " + value);
	}

	public override float GetModifier()
	{
		return PlayerInfo.Instance.charisma * value;
	}

	public override Sprite GetIcon()
	{
		return SpriteManager.Instatce.GetSprite("Icons/Stats/charisma_icon");
	}

	public override void ChangeStat()
	{
		PlayerInfo.Instance.charisma += value;

		if (PlayerInfo.Instance.charisma > PlayerInfo.Instance.max_dex)
			PlayerInfo.Instance.charisma = PlayerInfo.Instance.max_dex;

		if (PlayerInfo.Instance.charisma < 0) 
			PlayerInfo.Instance.charisma = 0;

		GUIController.Instance.CreateParameterChangeEffect(PanelManager.Instance.ActionPanel.CharismaText.gameObject, value);
		PanelManager.Instance.ActionPanel.ActivateChangeIcon(value, PanelManager.Instance.ActionPanel.CharismaChangeIcon);
	}
}
