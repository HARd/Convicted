public class day : Parameter
{
	public day(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return PlayerInfo.Instance.day * value;
	}
}
