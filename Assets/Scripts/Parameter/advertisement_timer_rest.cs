using UnityEngine;
using UnityEngine.Advertisements;

public class advertisement_timer_rest : Parameter
{
	public advertisement_timer_rest(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		Debug.Log("-- advertisement_timer_rest " + Advertisement.IsReady("rewardedVideo"));
		if(Advertisement.IsReady("rewardedVideo")) 
			return 100;
		else 
			return -999;
	}
}
