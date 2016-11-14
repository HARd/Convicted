using UnityEngine;
using System.Collections;
using ObjectsExtensionMethods;

public class FocusController : MonoBehaviour 
{

	[SerializeField]
	Transform focus;
	[SerializeField]
	float timeFocused = 0.1f;

	TutorialStep tutorialStep;

	public void OnShowTutorialMessage(TutorialStep step)
	{
		Debug.Log("OnShowTutorialMessage " + step.name);
		tutorialStep = step;
		focus.gameObject.SetActive(step.hasFocus);
		if(step.targetFocus == null)
		{
			focus.localPosition = new Vector3(2000, 0, 1);
			focus.localScale = new Vector2(20, 20);
		}
		else
		{
			//focus.position = step.targetFocus.position;
			focus.MoveTo(0f, 0f, timeFocused);
			focus.ScaleTo(new Vector2(20, 20), timeFocused);
			StartCoroutine(OnFocus(timeFocused));
		}
	}


	IEnumerator OnFocus(float delay)
	{
		yield return new WaitForSeconds(delay);

		focus.MoveTo(tutorialStep.targetFocus.localPosition.x, tutorialStep.targetFocus.localPosition.y, timeFocused);
		focus.ScaleTo(new Vector2(tutorialStep.targetFocus.localScale.x, tutorialStep.targetFocus.localScale.y), timeFocused);
	}

	public void OnTutorialTap()
	{
		focus.gameObject.SetActive(false);
	}
}
