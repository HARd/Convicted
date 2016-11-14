using UnityEngine;
using System.Collections;

public class LocalizationLoader : MonoBehaviour 
{
	[SerializeField]
	GameObject prefab;
	// Use this for initialization
	void Awake() 
	{
		if(Localization.Instance == null)
			GameObject.DontDestroyOnLoad(Instantiate(prefab, Vector3.zero, Quaternion.identity));
	}
}
