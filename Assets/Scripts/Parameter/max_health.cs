using UnityEngine;
public class max_health : Parameter
{
	public max_health(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void Applay()
	{
		PlayerInfo.Instance.max_health += UnityEngine.Mathf.FloorToInt(value);
		Debug.Log("--Applay() max_health - " + value);
	}
}
