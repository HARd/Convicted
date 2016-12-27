using UnityEngine;
using UnityEngine.Advertisements;

public class advertisement_timer : Parameter
{
	public advertisement_timer(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		Debug.Log("-- advertisement_timer" + Advertisement.IsReady("rewardedVideo") + "  " + WorldTime.Instance.rewarded_advertisement_cd);
		if(Advertisement.IsReady("rewardedVideo") && WorldTime.Instance.rewarded_advertisement_cd <= 0) 
			return 100;
		else 
			return -999;
	}
}
