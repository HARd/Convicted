public class character : Parameter
{
	public character(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return (value != PlayerInfo.Instance.charImageID) ? -999f : 0f;
	}
}
