using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Recipe {

	public int output_id;
	//public string type;
	public List<string> ingridients = new List<string>();
	public float mindReq;

}
