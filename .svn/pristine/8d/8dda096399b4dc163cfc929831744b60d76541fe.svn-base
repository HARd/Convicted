/*
	Слушает UpdateCounterValue и соответственно обновляет float параметр анимации.
	Автор: Гарбуз В.П.
*/


using UnityEngine;
using System.Collections;

public class AnimatorFloatCounterDisplay : MonoBehaviour
{
	[SerializeField]
	Animator animator;

	[SerializeField]
	string parameterName;

	[SerializeField]
	float fillSpeed = 0.1f;

	[SerializeField]
	int fullCounterValue = 10;

	[SerializeField]
	float delay = 0f;

	[SerializeField]
	float resetSpeed = 0.2f;

	[SerializeField]
	float resetDelay = 0f;

//	[SerializeField]
//	AudioSourcePlayer audioSourcePlayer;

	bool firstTime = true;
	float currentLevel;
	float requeredLevel;

	void DisplayDigitalValue(int counter) //SendMessage handler
	{
		StartCoroutine(SetCounterCoroutine(counter, delay));
	}

	void DisplayDigitalReset(int counter) //SendMessage handler
	{
		StartCoroutine(SetCounterCoroutine(counter, resetDelay));
	}

	void Update()
	{
		if (currentLevel == requeredLevel)
			return;

		if (currentLevel < requeredLevel)
		{
			currentLevel += Time.deltaTime * fillSpeed;
			currentLevel = (currentLevel > requeredLevel) ? requeredLevel : currentLevel;
		}

		if (currentLevel > requeredLevel)
		{
			currentLevel -= Time.deltaTime * resetSpeed;
			currentLevel = (currentLevel < 0f) ? 0f : currentLevel;
		}
			//currentLevel = requeredLevel;

		animator.SetFloat(parameterName, currentLevel);
	}

	void SetCounter(int counter)
	{
		requeredLevel = Mathf.Clamp(counter / (float)fullCounterValue, 0f, 1f);
		if (firstTime)
		{
			firstTime = false;
			currentLevel = requeredLevel;
			animator.SetFloat(parameterName, currentLevel);
		}
	}

	IEnumerator SetCounterCoroutine(int counter, float delay)
	{
		if(delay == 0f)
		{
			SetCounter(counter);
			yield return null;
		}
		yield return new WaitForSeconds(delay);
		SetCounter(counter);
	}
}
