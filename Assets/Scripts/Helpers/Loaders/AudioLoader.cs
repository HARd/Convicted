using UnityEngine;
using System.Collections;

public class AudioLoader : MonoBehaviour 
{
	[SerializeField]
	GameObject prefab;
	// Use this for initialization
	void Awake() 
	{
		if(AudioManager.Instance == null)
			Instantiate(prefab, Vector3.zero, Quaternion.identity);
	}
}
