using UnityEngine;
using UnityEngine.UI;
using ObjectsExtensionMethods;

public class EventActionController : BaseActionController
{
	[SerializeField]
	Text chanceText;

	[SerializeField]
	Image statImage;


	public override void Clear()
	{
		base.Clear();

		statImage.gameObject.SetActive (false);
		chanceText.gameObject.SetActive (false);
	}

	public override void Draw(Action action)
	{
		base.Draw(action);

		if(!DrawOutcomeChance())
			DrawChanceGeneralEvent();
	}

	void DrawChance(float chance)
	{
		if (chance <= -999 || chance >= 100)
			chance = 100;
		else if (chance <= 0 && chance >= -999)
			chance = 0;

		statImage.gameObject.SetActive (true);
		chanceText.gameObject.SetActive (true);
		chanceText.text = "[" + Localization.Instance.GetLocale (1213) + ": " + Mathf.FloorToInt(chance) + "%]";
	}

	bool DrawOutcomeChance()
	{
		Outcome outcome = action.GetFirstComponentInChildren<Outcome>();

		if (outcome != null) 
		{
			statImage.sprite = outcome.GetIcon();
			if(statImage.sprite != null)
			{
				DrawChance(outcome.ModifyChance());
				return true;
			}
		}
		return false;
	}

	void DrawChanceGeneralEvent()
	{
		RandomEventController randomEventController = action.GetComponent<RandomEventController>();
		GeneralEvent revent = (randomEventController != null) ? randomEventController.GetEvent(0) : null;

		if (revent != null && randomEventController.Count == 1) 
		{
			float chance = 0f;
			statImage.sprite = revent.GetIcon(ref chance);
			if(statImage.sprite != null)
				DrawChance(chance);
		}
	}
}
