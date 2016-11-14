using UnityEngine;

public class body : Parameter
{
	public body(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void Applay()
	{
		PlayerInfo.Instance.body += value;
		//Debug.Log("-- Applay() body - " + value);
	}

	public override float GetModifier()
	{
		//Debug.Log("-- GetModifier() body - " + value);
		return PlayerInfo.Instance.body * value;
	}

	public override Sprite GetIcon()
	{
		return SpriteManager.Instatce.GetSprite("Icons/Stats/body_icon");
	}

	public override void ChangeStat()
	{
		PlayerInfo.Instance.body += value;

		if (PlayerInfo.Instance.body > PlayerInfo.Instance.max_str)
			PlayerInfo.Instance.body = PlayerInfo.Instance.max_str;
		
		if (PlayerInfo.Instance.body < 0) 
			PlayerInfo.Instance.body = 0;
		
		GUIController.Instance.CreateParameterChangeEffect(PanelManager.Instance.ActionPanel.BodyText.gameObject, value);
		PanelManager.Instance.ActionPanel.ActivateChangeIcon(value, PanelManager.Instance.ActionPanel.BodyChangeIcon);
	}
}
