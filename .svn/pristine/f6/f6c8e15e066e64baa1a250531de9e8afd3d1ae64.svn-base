using UnityEngine;
public class tunnel : Parameter
{
	public tunnel(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return PlayerInfo.Instance.tunnel * value;
	}

	public override void ChangeStat()
	{
		PlayerInfo.Instance.tunnel += value;

		if(PlayerInfo.Instance.tunnel < 0) 
			PlayerInfo.Instance.tunnel = 0;
		
		if(PlayerInfo.Instance.tunnel > 100) 
			PlayerInfo.Instance.tunnel = 100;
	}
}
