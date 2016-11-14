using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour 
{
	private static AudioManager _instance;
	public static AudioManager Instance { get { return _instance ?? (_instance = FindObjectOfType<AudioManager>()); } }
	protected AudioManager() { _instance = null; }


	AudioSource source;

	public List<AudioClip> clipList = new List<AudioClip>();

	int current_id;
	int delayedSoundID;

	// Use this for initialization
	void Start () 
	{
		GameObject.DontDestroyOnLoad(gameObject);
		source = gameObject.GetComponent<AudioSource>();
	}
	
	public void Play(int id)
	{
		if(!GameData.current.mute)
		{
			if(current_id == 0 && id == 0)
			{
				PlaySound(id);
			}
			else if(source.isPlaying && id != 0)
			{
				delayedSoundID = id;
			}
			else if(!source.isPlaying) PlaySound(id);
		}
	}

	void Update()
	{
		if(delayedSoundID != 0 && !source.isPlaying)
		{
			PlaySound(delayedSoundID);
			delayedSoundID = 0;
		} 
	}

	void PlaySound(int id)
	{
		source.clip = clipList[id];
		current_id = id;
		source.Play();
	}

}
