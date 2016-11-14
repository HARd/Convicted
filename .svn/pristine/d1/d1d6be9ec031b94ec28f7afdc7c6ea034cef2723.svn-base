using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour {
	
	public Vector3 destination;

	float life_span = 1f;

	void Update () {

		Vector3 currentPosition = transform.position;

		transform.position = Vector3.Lerp(currentPosition,destination,0.5f * Time.deltaTime);

		life_span -= Time.deltaTime;

		if(life_span < 0)
		{
			Destroy(gameObject);
		}

		gameObject.GetComponent<Text>().CrossFadeAlpha(0,1f,true);

	}
}
