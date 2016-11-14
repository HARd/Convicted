using UnityEngine;

public class inmate_rep : Parameter
{
	public inmate_rep(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return PlayerInfo.Instance.inmate_rep * value;
	}

	public override Sprite GetIcon()
	{
		return SpriteManager.Instatce.GetSprite("Icons/Stats/inmaterep_icon");
	}

	public override void ChangeStat()
	{
		PlayerInfo.Instance.inmate_rep += value;

		if(PlayerInfo.Instance.inmate_rep < -200) 
			PlayerInfo.Instance.inmate_rep = -200;
		if(PlayerInfo.Instance.inmate_rep > 200) 
			PlayerInfo.Instance.inmate_rep = 200;
		if(value != 0) GUIController.Instance.CreateParameterChangeEffect(PanelManager.Instance.ActionPanel.InmateRepText.gameObject, value);
		PanelManager.Instance.ActionPanel.ActivateChangeIcon(value, PanelManager.Instance.ActionPanel.InmaterepChangeIcon);
	}
}
