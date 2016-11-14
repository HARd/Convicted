using UnityEngine;
using System.Collections;

public class PrefabLoader : MonoBehaviour 
{
	[SerializeField]
	GameObject prefab;
	// Use this for initialization
	void Start() 
	{
		Instantiate(prefab, Vector3.zero, Quaternion.identity);
	}
}
