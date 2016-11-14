using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CharacterData {

	public int name_id;
	public int tier_desc_id;
	public int image_id;
	public int story_name_id;
	public int story_id;
	public int sentence_id;
	public float body;
	public float charisma;
	public float mind;
	public float inmate_rep;
	public float guard_rep;
	public int consealment;
	public List<string> traits = new List<string>();
}
