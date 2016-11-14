using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class Skipped : MonoBehaviour
{
	[SerializeField]
	string loadScene;

	VideoDecoder videoDecoder;
	bool blocker = true;

	void Awake()
	{
		videoDecoder = GetComponent<VideoDecoder>();
		videoDecoder.PlayingCompleted += OnPlayingCompleted;
		string gameData = Path.Combine(DataStorage.Instance.GetPath(), SaveLoadXML.gameData);
		if(File.Exists(gameData)) 
			StartCoroutine(BlockCoroutine(2f));
	}

	public virtual void OnSkip()
	{
		if(!blocker)
			SceneManager.LoadScene(loadScene);
	}

	void OnPlayingCompleted(VideoDecoder sender)
	{
		SceneManager.LoadScene(loadScene);
	}

	IEnumerator BlockCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		blocker = false;
	}
}
