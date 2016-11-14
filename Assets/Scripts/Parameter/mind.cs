using UnityEngine;

public class mind : Parameter
{
	public mind(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void Applay()
	{
		PlayerInfo.Instance.mind += value;
		//Debug.Log("--Applay() mind - " + value);
	}

	public override float GetModifier()
	{
		//Debug.Log("--GetModifier() mind - " + value);
		return PlayerInfo.Instance.mind * value;
	}

	public override Sprite GetIcon()
	{
		return SpriteManager.Instatce.GetSprite("Icons/Stats/int_icon");
	}

	public override void ChangeStat()
	{
		float value = this.value;

		if(value > 0) 
			value =  value*(1+PlayerInfo.Instance.CheckTrait("quick_learning",true)+PlayerInfo.Instance.CheckTrait("slow_learning",true));
		
		PlayerInfo.Instance.mind += value;

		if (PlayerInfo.Instance.mind > PlayerInfo.Instance.max_int) 
			PlayerInfo.Instance.mind = PlayerInfo.Instance.max_int;
		
		if (PlayerInfo.Instance.mind < 0) 
			PlayerInfo.Instance.mind = 0;
		
		GUIController.Instance.CreateParameterChangeEffect(PanelManager.Instance.ActionPanel.MindText.gameObject, value);
		PanelManager.Instance.ActionPanel.ActivateChangeIcon(value, PanelManager.Instance.ActionPanel.MindChangeIcon);
	}
}
