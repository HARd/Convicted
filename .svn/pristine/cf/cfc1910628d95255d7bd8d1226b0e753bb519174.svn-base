/*
	Устанавливает ActionCounter в значение 0.
	Автор: Гарбуз В.П.
*/


using UnityEngine;
using System.Collections;

namespace GameLogic
{
	public class ActionCounterReseter //: SingleEventAction
	{
		[SerializeField]
		ActionCounter counter;

		[SerializeField]
		int setCounterTo = 0;

		void DoAction()
		{
			counter.Counter = setCounterTo;
			counter.SendMessage("DisplayDigitalValue", counter.Counter, SendMessageOptions.DontRequireReceiver);
		}
	}
}