using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CraftingPanelController : MonoBehaviour 
{
	[SerializeField]
	List<Recipe> recipeList = new List<Recipe>();

	[SerializeField]
	RecipeController[] recipeControllers;

	[SerializeField]
	GameObject ArrowButtonF;
	[SerializeField]
	GameObject ArrowButtonB;

	int current_page = 0;

	void Start() 
	{
		gameObject.SetActive(false);
	}

	public void OpenCraftingWindow()
	{
		if(current_page == 0) 
			ArrowButtonB.SetActive(false);
		else 
			ArrowButtonB.SetActive(true);
		
		if(current_page >= Mathf.FloorToInt(recipeList.Count/4))
		{
			current_page = Mathf.FloorToInt(recipeList.Count/4);
			ArrowButtonF.SetActive(false);
		}
		else 
			ArrowButtonF.SetActive(true);


		foreach(RecipeController recipe in recipeControllers)
			recipe.Clear();


		int j = 0;
		for(int i = current_page*4; i < recipeList.Count; i++)
		{
			recipeControllers[j++].DrawRecipe(recipeList[i]);
			if(j == recipeControllers.Length) 
				break;
		}
	}

	public void onArrowButton(int value)
	{
		AudioManager.Instance.Play(0);

		current_page += value;

		if(current_page < 0) 
			current_page = 0;
		
		OpenCraftingWindow();
	}
}
