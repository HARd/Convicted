public class story : Parameter
{
	public story(string stat, string event_name, float value): base(stat, event_name, value){}

	public override float GetModifier()
	{
		return (!StoryManager.Instance.VerifyStory (event_name)) ? -999f : 0f;
	}
}
