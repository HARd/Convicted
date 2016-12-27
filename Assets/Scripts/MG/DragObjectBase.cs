using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ObjectsExtensionMethods;

public class DragObjectBase : MonoBehaviour
{
	
	[SerializeField]
	Transform draggingAnchor;
	[SerializeField]
	GameObject targetObject;
	[SerializeField]
	float moveTime = 0.4f;
	[SerializeField]
	bool IsMovedToHome = true;
	[SerializeField]
	bool IsMovedToTarget = true;
	[SerializeField]
	bool IsRotatededToTarget = true;

	public bool IsDragging { private set; get; }

	Image image;
	Transform parentTransform;
	Vector2 startPsition;

	void Start()
	{
		parentTransform = transform.parent;
		startPsition = transform.localPosition;
		image = GetComponent<Image>();
	}

	public void OnStartDrag(PointerEventData eventData)
	{
		IsDragging = true;
		transform.SetParent(draggingAnchor, false);
		image.raycastTarget = false;
		print("--OnStartDrag " + name);
	}

	public void OnDragged(PointerEventData data)
	{
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), data.position, data.pressEventCamera, out globalMousePos))
		{
			transform.position = globalMousePos;
		}
	}

	public void OnDragFinish(PointerEventData eventData)
	{
		IsDragging = false;
		if(eventData.pointerEnter == targetObject)
		{
			SetToTarget();
			SendMessage("OnDropTarget", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			SetToHome();
			SendMessage("OnDrop", SendMessageOptions.DontRequireReceiver);
		}
	}

	void SetToTarget()
	{
		transform.SetParent(targetObject.transform, true);
		if(IsMovedToTarget)
			this.MoveTo(0, 0, moveTime/2).SetFinalMessage(gameObject, "OnArrivedToTarget", null, SendMessageOptions.DontRequireReceiver);

		if(IsRotatededToTarget)
			this.RotateTo(0, moveTime/2, false, (transform.localEulerAngles.z > 0 && transform.localEulerAngles.z <= 180));
	}

	void SetToHome()
	{
		transform.SetParent(parentTransform, false);
		if(IsMovedToHome)
			this.MoveTo(startPsition.x, startPsition.y, moveTime).SetFinalMessage(gameObject, "OnArrivedToHome", null, SendMessageOptions.DontRequireReceiver);
		else
			OnArrivedToHome();
	}

	void OnArrivedToHome()
	{
		image.raycastTarget = true;
	}
}
