using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

public class RandomEventController : MonoBehaviour
{
	[SerializeField]
	List<GeneralEvent> randomEventList = new List<GeneralEvent>();

	public int Count {get {return randomEventList.Count;}}

	public void ModifyChanceRandomEventList()
	{
		foreach(GeneralEvent randomEvent in randomEventList)
			randomEvent.ModifyChance();
	}

	public GeneralEvent GetRandomEvent()
	{
		ModifyChanceRandomEventList();
		randomEventList.Sort((a, b) => a.chance.CompareTo(b.chance));
		int chance = Random.Range(0,100);
		return randomEventList.Find(re => chance < re.chance);
	}

	public GeneralEvent GetEvent(uint index)
	{
		return (index < randomEventList.Count) ? randomEventList[(int)index] : null;
	}
}
