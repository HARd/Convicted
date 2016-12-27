using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class MgBase : ScreenBase
{
//	[SerializeField]
//	Button buttonReturn;

	public bool isComplite {get; protected set;}
	
	public virtual void Reset(){}

	public void OnReturnButton()
	{
		CreateExitDialog();
	}

	public void CreateExitDialog()
	{
		AudioManager.Instance.Play(1);
		ScreenManager.Instance.CreateScreen("HintPanel");
		ScreenManager.Instance.current.GetComponent<Hint>().ShowDialog(Localization.Instance.GetLocale(997), Localization.Instance.GetLocale(76), (confirm)=>
			{
				if(confirm)
					Destroy(gameObject);
			});
	}
}
