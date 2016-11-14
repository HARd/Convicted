using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIEffect : MonoBehaviour {

	public bool status;
	public bool timed;
	public bool destroyOnTimer;
	public bool waitEventEnd = false;
	public enum EffectType{Rescale, SwingHorizontal, SwingVertical, TextBlink};
	public EffectType effectType = new EffectType();

	public float value = 0.75f;
	public float intensity = 0.75f;
	public float timer;
	float timerSave;

	public Vector3 startPos;

	int scaleDirection;


	// Use this for initialization
	void Start () 
	{
		//EventPanel = GameObject.Find("EventPanel");
		timerSave = timer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (waitEventEnd && PanelManager.Instance.EventPanel.gameObject.activeSelf) 
			return;

		if (status) {
			switch (effectType) {
			case EffectType.Rescale:
				if (gameObject.transform.localScale.x >= 1) {
					scaleDirection = -1;
				} else if (gameObject.transform.localScale.x <= value) {
					scaleDirection = 1;
				}

				float scaleX = transform.localScale.x + Time.deltaTime * intensity * scaleDirection;
				float scaleY = transform.localScale.x + Time.deltaTime * intensity * scaleDirection;

				transform.localScale = new Vector3 (scaleX,scaleY,1);
				break;
			case EffectType.SwingHorizontal:
				if (scaleDirection == 0) {
					scaleDirection = -1;
				}
				if (transform.localPosition.x >= (startPos.x + value)) {
					scaleDirection = -1;
				} else if (transform.localPosition.x <= (startPos.x - value)) {
					scaleDirection = 1;
				}

				float targetX = transform.localPosition.x + Time.deltaTime * intensity * scaleDirection;
				transform.localPosition = new Vector3 (targetX,transform.localPosition.y, 0);
				break;
			case EffectType.SwingVertical:
				if (scaleDirection == 0) {
					scaleDirection = -1;
				}
				if (transform.localPosition.y >= (startPos.y + value)) {
					scaleDirection = -1;
				} else if (transform.localPosition.y <= (startPos.y - value)) {
					scaleDirection = 1;
				}

				float targetY = transform.localPosition.y + Time.deltaTime * intensity * scaleDirection;
				transform.localPosition = new Vector3 (transform.localPosition.x,targetY, 0);
				break;
			case EffectType.TextBlink:
				if (gameObject.GetComponent<Text> ().canvasRenderer.GetAlpha () > 0.9f) {
					scaleDirection = 0;
				} 
				if (gameObject.GetComponent<Text> ().canvasRenderer.GetAlpha () < 0.1f) {
					scaleDirection = 1;
				}

				gameObject.GetComponent<Text> ().CrossFadeAlpha (scaleDirection, intensity, false);
				break;
			}
			if (timed) {
				if (timer > 0) {
					timer -= Time.deltaTime;
				} else if (destroyOnTimer) {
					timer = timerSave;
					gameObject.SetActive (false);
				} else if (!destroyOnTimer) {
					timer = timerSave;
					status = false;
				}
			}
		}
	}

	public void ResetData()
	{
		switch (effectType) {
		case EffectType.Rescale:
			break;
		case EffectType.SwingHorizontal:
			scaleDirection = 0;
			break;
		case EffectType.SwingVertical:
			scaleDirection = 0;
			break;
		}
	}
}
