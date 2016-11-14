using UnityEngine;

public class health : Parameter
{
	public health(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return PlayerInfo.Instance.health * value;
	}

	public override void ChangeStat()
	{
		//Debug.Log("--health ChangeStat() " + this.value);
		float value = this.value;

		if (value > 0)
			value = value * (1 + PlayerInfo.Instance.CheckTrait("tough", true));

		PlayerInfo.Instance.health += value;

		if (PlayerInfo.Instance.health > PlayerInfo.Instance.max_health)
			PlayerInfo.Instance.health = PlayerInfo.Instance.max_health;

		if (PlayerInfo.Instance.health < 0)
			PlayerInfo.Instance.health = 0;

		PanelManager.Instance.ActionPanel.HealthText.GetComponent<GUIEffect>().status = true;
	}
}
