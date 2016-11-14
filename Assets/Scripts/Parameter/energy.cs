public class energy : Parameter
{
	public energy(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return PlayerInfo.Instance.energy * value;
	}

	public override void ChangeStat()
	{
		float value = this.value;

		if (value > 0)
			value = value * (1 + PlayerInfo.Instance.CheckTrait("quick_recovery", true) + PlayerInfo.Instance.CheckTrait("slow_recovery", true));
		
		PlayerInfo.Instance.energy += value;

		if (PlayerInfo.Instance.energy > PlayerInfo.Instance.health)
			PlayerInfo.Instance.energy = PlayerInfo.Instance.health;
		
		if (PlayerInfo.Instance.energy < 0)
			PlayerInfo.Instance.energy = 0;

		PanelManager.Instance.ActionPanel.EnergyText.GetComponent<GUIEffect>().status = true;
	}
}
