using UnityEngine;
public class death_resist : Parameter
{
	public death_resist(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void Applay()
	{
		//PlayerInfo.Instance.death_resist += value;
		Debug.Log("-- Applay() death_resist - " + value);
	}
}
