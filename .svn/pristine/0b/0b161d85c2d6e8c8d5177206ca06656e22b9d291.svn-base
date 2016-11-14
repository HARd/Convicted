using UnityEngine;
public class cash : Parameter
{
	public cash(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void Applay()
	{
		PlayerInfo.Instance.cash += value;
		//Debug.Log("-- Applay() cash - " + value);
	}

	public override void ChangeStat()
	{
		PlayerInfo.Instance.cash += value;

		if(PlayerInfo.Instance.cash < 0) 
			PlayerInfo.Instance.cash = 0;
		if(value > 0) 
			AudioManager.Instance.Play(3); 
		
		GUIController.Instance.CreateParameterChangeEffect(PanelManager.Instance.ActionPanel.cash_s.gameObject, value);
	}
}
