//using UnityEngine;
public class item : Parameter
{
	public item(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void ChangeStat()
	{
		for(int k = 0; k<value; k++) 
		{
			PlayerInfo.Instance.EquipItem(event_name, true, false);
		}
	}
}
