using UnityEngine;
public class trait : Parameter
{
	public trait(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		if(value == 0) 
			return PlayerInfo.Instance.CheckTrait(event_name, false);
		else
			return PlayerInfo.Instance.CheckTrait(event_name, true) * value;
	}

	public override void ChangeStat()
	{
		PlayerInfo.Instance.ChangeTrait(event_name, value == 1);
	}
}
