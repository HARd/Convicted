using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickDelegatController : MonoBehaviour, IPointerClickHandler
{
	public System.Action onClick;

	public void OnPointerClick (PointerEventData eventData)
	{
		//Debug.Log("--OnClickDelegatController " + name);
		onClick();
	}
}
