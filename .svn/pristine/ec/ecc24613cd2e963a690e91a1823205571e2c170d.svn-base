using UnityEngine;

public class guard_rep : Parameter
{
	public guard_rep(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return PlayerInfo.Instance.guard_rep * value;
	}

	public override Sprite GetIcon()
	{
		return SpriteManager.Instatce.GetSprite("Icons/Stats/guardrep_icon");
	}

	public override void ChangeStat()
	{
		PlayerInfo.Instance.guard_rep += value;

		if(PlayerInfo.Instance.guard_rep < -200) 
			PlayerInfo.Instance.guard_rep = -200;
		if(PlayerInfo.Instance.guard_rep > 200) 
			PlayerInfo.Instance.guard_rep = 200;
		GUIController.Instance.CreateParameterChangeEffect(PanelManager.Instance.ActionPanel.GuardRepText.gameObject, value);
		PanelManager.Instance.ActionPanel.ActivateChangeIcon(value, PanelManager.Instance.ActionPanel.GuardrepChangeIcon);
	}
}
