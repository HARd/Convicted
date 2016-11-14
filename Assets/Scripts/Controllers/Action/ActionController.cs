using UnityEngine;
using UnityEngine.UI;

public class ActionController : BaseActionController
{
	[SerializeField]
	Text HealthIconText;

	[SerializeField]
	GameObject HealthIcon;

	public override void Clear()
	{
		base.Clear();

		HealthIconText.text = "";
		HealthIcon.SetActive (false);
	}

	public override void DrawHealth()
	{
		HealthIconText.text = " +";
		HealthIconText.gameObject.SetActive (true);
		HealthIcon.SetActive (true);
	}
}
