using UnityEngine;
public class ad_cd : Parameter
{
	public ad_cd(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void ChangeStat()
	{
		WorldTime.Instance.rewarded_advertisement_cd = Mathf.FloorToInt(value);
	}
}
