//using UnityEngine;
public class trigger : Parameter
{
	public trigger(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		//Debug.Log("--GetModifier() trigger - " + value);
		 if(value == 0) 
			return PlayerInfo.Instance.CheckTrigger(event_name, false);
		else
			return PlayerInfo.Instance.CheckTrigger(event_name, true) * value;
	}

	public override void ChangeStat()
	{
			PlayerInfo.Instance.ChangeTrigger(event_name, value == 1, true);
	}
}
