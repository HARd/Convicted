using UnityEngine;
public class work : Parameter
{
	public work(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void ChangeStat()
	{
		switch(event_name)
		{
		case "body":
			new cash(stat, event_name,(PlayerInfo.Instance.body/100)*value*(1+PlayerInfo.Instance.CheckTrait("strong",true)+PlayerInfo.Instance.CheckTrait("weak",true))).ChangeStat();
			break;
		case "mind":
			new cash(stat, event_name,(PlayerInfo.Instance.mind/100)*value*(1+PlayerInfo.Instance.CheckTrait("smart",true)+PlayerInfo.Instance.CheckTrait("dumb",true))).ChangeStat();
			break;
		case "charisma":
			new cash(stat, event_name,(PlayerInfo.Instance.charisma/100)*value).ChangeStat();
			break;
		}
	}
}
