using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IngridientController : MonoBehaviour 
{
	[SerializeField]
	Text ingridientText;

	public void Clear()
	{
		ingridientText.text = "";
		GetComponent<Toggle>().isOn = false;
		gameObject.SetActive(false);
	}

	public bool DrawIngridient(string ingridient)
	{
		gameObject.SetActive(true);
		ingridientText.text = PlayerInfo.Instance.GetItemText(ingridient);
		bool hasTool = Inventory.Instance.HasTool(ingridient);
		GetComponent<Toggle>().isOn = hasTool;
		return hasTool;
	}
}
