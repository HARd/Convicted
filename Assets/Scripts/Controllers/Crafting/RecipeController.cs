using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour 
{
	[SerializeField]
	Text recipeText;
	[SerializeField]
	Image itemImage;
	[SerializeField]
	GameObject craftButton;
	[SerializeField]
	Text craftingButtonText;
	[SerializeField]
	Text MindRequirementText;
	[SerializeField]
	IngridientController[] ingridientControllers;

	public Recipe recipe {set; get;}

	public void Clear()
	{
		recipeText.text = "";
		MindRequirementText.text = "";
		gameObject.SetActive(false);
		foreach(IngridientController ingridient in ingridientControllers)
			ingridient.Clear();
	}

	public void DrawRecipe(Recipe recipe)
	{
		this.recipe = recipe;
		gameObject.SetActive(true);
//		if(recipe.type == "item")
//		{
			recipeText.text = Localization.Instance.GetLocale(PlayerInfo.Instance.inventory[recipe.output_id].text_id);
			itemImage.sprite = SpriteManager.Instatce.GetSprite("Tools/" + PlayerInfo.Instance.inventory[recipe.output_id].name + "/inv");
			recipeText.text = Localization.Instance.GetLocale(PlayerInfo.Instance.inventory[recipe.output_id].text_id);
//		}
//		else if(recipe.type == "weapon")
//		{
//			recipeText.text = Localization.Instance.GetLocale(PlayerInfo.Instance.weaponList[recipe.output_id].text_id);
//			//itemImage.sprite = SpriteManager.Instatce.GetSprite(PlayerInfo.Instance.weaponList[recipe.output_id].image_path);
//			itemImage.sprite = SpriteManager.Instatce.GetSprite("Tools/" + PlayerInfo.Instance.weaponList[recipe.output_id].name + "/inv");
//			recipeText.text = Localization.Instance.GetLocale(PlayerInfo.Instance.weaponList[recipe.output_id].text_id);
//		}

		craftingButtonText.text = Localization.Instance.GetLocale(862);

		if(DrawIngridient(recipe) && PlayerInfo.Instance.mind >= recipe.mindReq)
			craftButton.SetActive(true);
		else if(PlayerInfo.Instance.mind < recipe.mindReq)
		{
			craftButton.SetActive(false);
			MindRequirementText.text = Localization.Instance.GetLocale(71) + " " + recipe.mindReq;
		}
		else
		{
			craftButton.SetActive(false);
			MindRequirementText.text = Localization.Instance.GetLocale (866);
		} 
	}

	bool DrawIngridient(Recipe recipe)
	{
		bool allIngridient = true;
		int i = 0;
		foreach(string ingridient in recipe.ingridients)
			if(!ingridientControllers[i++].DrawIngridient(ingridient))
				allIngridient = false;
		return allIngridient;
	}

	public void OnCraftButton()
	{
		AudioManager.Instance.Play(0);
		AudioManager.Instance.Play(4);
		string output_item = "";
		//if(recipe.type == "item") 
			output_item = PlayerInfo.Instance.inventory[recipe.output_id].name;
//		else if(recipe.type == "weapon") 
//			output_item = PlayerInfo.Instance.weaponList[recipe.output_id].name;

		foreach(string ingridient in recipe.ingridients)
			PlayerInfo.Instance.EquipItem(ingridient,false,false);

		//if(recipe.type == "item") 
			PlayerInfo.Instance.EquipItem(output_item,true,false);

//		if(recipe.type == "weapon") 
//			PlayerInfo.Instance.EquipWeapon(output_item,true);

		PanelManager.Instance.CraftingPanel.OpenCraftingWindow();
	}
}
