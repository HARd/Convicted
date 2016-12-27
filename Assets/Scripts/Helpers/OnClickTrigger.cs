using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnClickTrigger : MonoBehaviour, IPointerClickHandler
{
	public Button.ButtonClickedEvent onClick;

	public void OnPointerClick (PointerEventData eventData)
	{
		//Debug.Log("--OnClickDelegatController " + name);
		onClick.Invoke();
	}
}
