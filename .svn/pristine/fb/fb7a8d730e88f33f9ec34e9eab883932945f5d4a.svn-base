using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewGamePanel : MonoBehaviour 
{
	[SerializeField]
	CharacterTierController[] CharacterTiers;

	[SerializeField]
	Button ForwardArrow;

	[SerializeField]
	Button BackArrow;

	int page = 0;

	public void ReturnToMenu()
	{
		page = 0;
		AudioManager.Instance.Play(0);
		MainMenu.Instance.ReturnToMenu();
	}

	public void CharacterGeneration(int i)
	{
		AudioManager.Instance.Play(1);
		MainMenu.Instance.CharacterGeneration(i + (page*CharacterTiers.Length), false);
	}

	public void NewGame()
	{
		for(int i = 0; i < CharacterTiers.Length; i++)
			CharacterTiers[i].NewGame(i + (page*CharacterTiers.Length));

		if(page == 0) 
			BackArrow.gameObject.SetActive(false);
		else 
			BackArrow.gameObject.SetActive(true);

		//		if(page >= Mathf.FloorToInt(tierCost.Count/3))
		//		{
		//			page = Mathf.FloorToInt(tierCost.Count/3);
		//			ArrowF.SetActive(false);
		//		}
		//		else ArrowF.SetActive(true);

		if(page >= 1)
		{
			page = 1;
			ForwardArrow.gameObject.SetActive(false);
		}
		else 
			ForwardArrow.gameObject.SetActive(true);
	}

	public void ChangeNewGamePage(int value)
	{
		AudioManager.Instance.Play(0);
		page += value;
		if(page < 0) page = 0;
		MainMenu.Instance.NewGame();
	}
}
