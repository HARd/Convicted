using UnityEngine;
using System.Collections;

[System.Serializable]
public class TutorialStep
{
	public string name;
	//public string task;
	//public bool blockScreen;
	public bool hasCharacter;
	public bool hasCharacter2;
	public bool hasPointer;
	public enum TextPosition{Bottom, Center, Top, Flip};
	public TextPosition textPos = new TextPosition();
	public Vector2 pointerPosition;
	public enum PointerRotation{Up,Right,Down,Left};
	public PointerRotation pointerRot;
	public int descriprion_id;
	public int tap_id;

	//public enum Panel{Action, Event, Quest};
	public enum enumPanel{None, Action, Event, OpenQuest, QuestHint, CloseQuest, Timed, Screen, Story};
	public enumPanel panel;
	public GameObject target;
	public float time;
	public bool hasFocus = false;
	public Transform targetFocus;

}
