using UnityEngine;
using System.Collections;

public class PlayerInfoLoader : MonoBehaviour 
{
	[SerializeField]
	GameObject prefab;
	// Use this for initialization
	void Awake() 
	{
		if(PlayerInfo.Instance == null)
			GameObject.DontDestroyOnLoad(Instantiate(prefab, Vector3.zero, Quaternion.identity));
	}
}
