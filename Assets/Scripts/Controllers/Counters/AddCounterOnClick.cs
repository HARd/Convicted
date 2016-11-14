/*
	По клику по зоне вызывает Add у ActionCounter.
	Автор: Гарбуз В.П.
*/


using UnityEngine;

public class AddCounterOnClick : MonoBehaviour
{
	[SerializeField]
	ActionCounter counter;

	[SerializeField]
	int numberToAdd = 1;

	void OnClickBegin() //SendMessage handler
	{
		counter.Add(numberToAdd);
	}
}
