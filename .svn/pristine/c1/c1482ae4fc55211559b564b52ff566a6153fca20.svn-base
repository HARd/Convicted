using UnityEngine;
public class stat_bonus : Parameter
{
	public stat_bonus(string stat, string event_name, float value): base(stat, event_name, value){}

	public override void ChangeStat()
	{
		if(event_name == "inmate_rep")
		{
			float val = (value > 0) ? (1+(PlayerInfo.Instance.charisma/100)*value*(1+PlayerInfo.Instance.CheckTrait("silvertongue",true)+PlayerInfo.Instance.CheckTrait("ugly",true))) :
				((100-PlayerInfo.Instance.charisma)/100)*value;
			new inmate_rep(stat, event_name, val).ChangeStat();
		}
		else if(event_name == "guard_rep")
		{
			float val = (value > 0) ? (1+(PlayerInfo.Instance.charisma/100)*value*(1+PlayerInfo.Instance.CheckTrait("silvertongue",true)+
				PlayerInfo.Instance.CheckTrait("ugly",true)+PlayerInfo.Instance.CheckTrigger("friendly_guards_bonus",true))) : ((100-PlayerInfo.Instance.charisma)/100)*value;
			new guard_rep(stat, event_name, val).ChangeStat();
		}
	}
}
