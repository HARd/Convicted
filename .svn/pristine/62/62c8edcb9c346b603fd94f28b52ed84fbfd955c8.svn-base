//using UnityEngine;
public class weapon : Parameter
{
	public weapon(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void ChangeStat()
	{
		for(int k = 0; k<value; k++) 
		{
			PlayerInfo.Instance.EquipItem(event_name, true);
		}
	}
}
