/*
	Слушает UpdateCounterValue, если CounterValue >= counter запускает анимацию.
	Автор: Гарбуз В.П.
*/


using UnityEngine;

public class PlayAnimationOnCounterValueContoller : MonoBehaviour
{
	[SerializeField]
	Animator animator;

	public enum ControlMode { PlayAnimation, SetBool }

	[SerializeField]
	ControlMode mode;

	[SerializeField]
	string parameterName;

	[SerializeField]
	int counterValue = 10;

	void DisplayDigitalValue(int counter) //SendMessage handler
	{
		if (counter < counterValue)
			return;

		if (mode == ControlMode.PlayAnimation)
			animator.Play(parameterName);
		else
			animator.SetBool(parameterName, true);
	}
}
