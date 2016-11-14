/*
	Компонент кнопки прокрутки органайзера тулзов в инвентаре.
*/


using UnityEngine;
using UnityEngine.UI;

public class InventoryScrollButtonResponder : MonoBehaviour
{
	[SerializeField]
	ScrollRect scrollRect;

	[SerializeField]
	float fillSpeed = 0.1f;

	float currentLevel;
	float requeredLevel;

	public void OnPress() //SendMessage handler
	{
		requeredLevel = scrollRect.horizontalNormalizedPosition + Mathf.Clamp(1f/Inventory.Instance.GetHolderCount()*2f, 0f, 1f)*Mathf.Sign(fillSpeed);
		currentLevel = scrollRect.horizontalNormalizedPosition;
		//print(requeredLevel);
	}

	void Update()
	{
		if (currentLevel == requeredLevel)
			return;

		if (Mathf.Abs(currentLevel - requeredLevel) < 0.005f)
		{
			currentLevel = requeredLevel;
		}
		else

//		if (currentLevel < requeredLevel)
			currentLevel += Time.deltaTime * fillSpeed;

//		if (currentLevel > requeredLevel)
//			currentLevel = requeredLevel;

		scrollRect.horizontalNormalizedPosition = currentLevel;
	}
}


