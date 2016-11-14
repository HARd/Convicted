/*
	Компонент, обеспечиваеющий выравнивание мест под тулзы в инвентаре (ToolHolder),
	распределение их по позициям, адекватное поведения при добавлении и удалении тулзов.
	Может быть заменен другим при сохранении прочей логики инвентаря.
	Автор: Кущ А.Е.
*/


using UnityEngine;
using UnityEngine.UI;
using ObjectsExtensionMethods;

public class ChildrenOrganizer : MonoBehaviour
{
	[SerializeField]
	ContentSizeFitter scrollRect; 

	[SerializeField]
	Vector2 distanceToNextChild = new Vector2(74f, 0f);
	[SerializeField]
	float speed = 5;
	[SerializeField]
	float tmpSpeedMultiplier = 1;

	int _itemsOffset = 0;
	public int ItemsOffset
	{
		get { return _itemsOffset; }
		set
		{
			_itemsOffset = value;
			SendMessageUpwards("OnChildrenOrganizerItemOffsetChanged", this, SendMessageOptions.DontRequireReceiver);
		}
	}

	[SerializeField]
	int visibleWindowSize = 6;

	bool hasMovingChilds = false;
	public bool IsMoving { get { return hasMovingChilds; } }

	void Update()
	{
		RecalculatePositions();
	}

	public void SetChildrenPositions()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			child.localPosition = (ItemsOffset + i) * distanceToNextChild;
			child.gameObject.SetActive(IsChildPartiallyVisibleInWindow(child.localPosition));
		}
	}

	void RecalculatePositions()
	{
		int size = transform.childCount;

		if (size + ItemsOffset < visibleWindowSize && ItemsOffset < 0)
			ItemsOffset += 1;

		bool hasMovingChilds = false;
		for (int i = 0; i < size; i++)
		{
			Transform child = transform.GetChild(i);
			Vector2 requiredPosition = (ItemsOffset + i) * distanceToNextChild;
			if ((Vector2)child.localPosition == requiredPosition)
				continue;
			else
			{
				child.localPosition = Vector2.MoveTowards(child.localPosition, requiredPosition, Time.deltaTime * speed * tmpSpeedMultiplier);
				child.gameObject.SetActive(IsChildPartiallyVisibleInWindow(child.localPosition));
				hasMovingChilds = true;
			}

		}

		if (hasMovingChilds && !this.hasMovingChilds)
			OnStartMoving();
		else if (!hasMovingChilds && this.hasMovingChilds)
			OnStopMoving();

		this.hasMovingChilds = hasMovingChilds;

	}

	const float epsilon = 0.0001f;
	bool IsChildPartiallyVisibleInWindow(Vector3 localPosition)
	{
		float x = localPosition.x;
		return x > -distanceToNextChild.x + epsilon && x < distanceToNextChild.x * visibleWindowSize - epsilon;
	}

	public bool IsChildFullyVisibleInWindow(Vector3 localPosition)
	{
		float x = localPosition.x;
		return x >= 0 && x <= distanceToNextChild.x * (visibleWindowSize - 1);
	}

	void OnStartMoving()
	{
		//this.SetRespondable(false);
		BroadcastMessage("OnChildrenOrganizerStartedMoving", this, SendMessageOptions.DontRequireReceiver);
		//SendMessageUpwards("OnChildrenOrganizerStartedMoving", this, SendMessageOptions.DontRequireReceiver);
	}

	void OnStopMoving()
	{
		//this.SetRespondable(true);
		tmpSpeedMultiplier = 1;
		//SendMessageUpwards("OnChildrenOrganizerStoppedMoving", this, SendMessageOptions.DontRequireReceiver);
		BroadcastMessage("OnChildrenOrganizerStoppedMoving", this, SendMessageOptions.DontRequireReceiver);
	}

	public void Add(Transform childTransform)
	{
		//childTransform.SetParent(transform);
		childTransform.SetParent(transform, false);

		if (transform.childCount == 1)
		{
			childTransform.localPosition = Vector3.zero;
			return;
		}

		Transform mysLastChild = transform.GetChild(transform.childCount - 2);
		childTransform.localPosition = mysLastChild.localPosition + (Vector3)distanceToNextChild;

		//childTransform.gameObject.SetActive(IsChildPartiallyVisibleInWindow(childTransform.localPosition));
		//scrollRect.;
		//print("-- scrollRect.horizontalFit " + scrollRect.horizontalFit.ToString());
	}

	public bool CanBeShifted(int value)
	{
		int newOffset = ItemsOffset + value;

		if (newOffset > 0)
			return false;
		else if (value < 0 && transform.childCount + newOffset < visibleWindowSize) //we can not shift tools to the left if not all window is filled with tools
			return false;
		else
			return true;
	}

	public void Shift(int value)
	{
		if (CanBeShifted(value))
			ItemsOffset += value;
	}

	public int GetRequiredShiftToWindow(int itemIndex)
	{
		if (itemIndex < -ItemsOffset)
			return -ItemsOffset - itemIndex;

		if (itemIndex >= -ItemsOffset + visibleWindowSize)
			return -ItemsOffset + visibleWindowSize - itemIndex - 1;

		return 0;
	}

	public void ScrollToWindow(int itemIndex)
	{
		int requiredShift = GetRequiredShiftToWindow(itemIndex);
		tmpSpeedMultiplier = System.Math.Abs(requiredShift);
		tmpSpeedMultiplier = tmpSpeedMultiplier > 1 ? tmpSpeedMultiplier : 1;
		Shift(requiredShift);
	}
}
