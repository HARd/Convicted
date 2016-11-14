using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Crafting : MonoBehaviour 
{
//	private static Crafting _instance;
//	public static Crafting Instance { get { return _instance ?? (_instance = FindObjectOfType<Crafting>()); } }
//	protected Crafting() { _instance = null; }
//
//	public List<Recipe> recipeList = new List<Recipe>();
//
//	GameObject CraftingMainPanel;
//	
//	List<GameObject> recipeBoxList = new List<GameObject>();
//	List<Text> recipeTextList = new List<Text>();
//
//	GameObject ArrowButtonF;
//	GameObject ArrowButtonB;
//
//	public int current_page = 0;
//	
//	void Start () {
//
//		CraftingMainPanel = GameObject.Find("CraftingMainPanel");
//
//		foreach(Transform _object in CraftingMainPanel.transform)
//		{
//			if(_object.name == "Recipe")
//			{
//				recipeBoxList.Add(_object.gameObject);
//				foreach(Transform __object in _object)
//				{
//					if(__object.name == "RecipeText") recipeTextList.Add(__object.GetComponent<Text>());
//				}
//				_object.gameObject.SetActive(false);
//			}
//		}
//
//		ArrowButtonF = GameObject.Find("ArrowButtonForward");
//		ArrowButtonB = GameObject.Find("ArrowButtonBack");
//
//		CraftingMainPanel.SetActive(false);
//
//	}
//	
//	public void OpenCraftingWindow()
//	{
//		if(current_page == 0) ArrowButtonB.SetActive(false);
//		else ArrowButtonB.SetActive(true);
//		if(current_page >= Mathf.FloorToInt(recipeList.Count/4))
//		{
//			current_page = Mathf.FloorToInt(recipeList.Count/4);
//			ArrowButtonF.SetActive(false);
//		}
//		else ArrowButtonF.SetActive(true);
//
//		foreach(Text text in recipeTextList)
//		{
//			text.text = "";
//		}
//		foreach(GameObject recipe in recipeBoxList)
//		{
//			recipe.SetActive(false);
//		}
//
//		int j = 0;
//		for(int i = current_page*4;i < recipeList.Count;i++)
//		{
//			recipeBoxList[j].SetActive(true);
//			if(recipeList[i].type == "item")
//			{
//				recipeTextList[j].text = Localization.Instance.GetLocale(PlayerInfo.Instance.inventory[recipeList[i].output_id].text_id);
//			} 
//			else if(recipeList[i].type == "weapon")
//			{
//				recipeTextList[j].text = Localization.Instance.GetLocale(PlayerInfo.Instance.weaponList[recipeList[i].output_id].text_id);
//			} 
//			recipeBoxList[j].gameObject.GetComponent<ActionClick>().id = recipeList[i].output_id;
//			recipeBoxList[j].gameObject.GetComponent<ActionClick>().item_name = recipeList[i].type;
//			List<GameObject> ingrBoxList = new List<GameObject>();
//			GameObject CraftingButton = null;
//			Text MindReqText = null;
//			foreach(Transform ingridient_main in recipeBoxList[j].transform)
//			{
//				if(ingridient_main.name == "Ingridient")
//				{
//					ingrBoxList.Add(ingridient_main.gameObject);
//					ingridient_main.GetComponent<Toggle>().isOn = false;
//					ingridient_main.gameObject.SetActive(false);
//				}
//				else if(ingridient_main.name == "ItemImage")
//				{
//					if(recipeList[i].type == "item") 
//						//ingridient_main.GetComponent<Image>().sprite = GUIController.Instance.ItemImageList[PlayerInfo.Instance.inventory[recipeList[i].output_id].image_id];
//						ingridient_main.GetComponent<Image>().sprite = SpriteManager.Instatce.GetSprite("Tools/" + PlayerInfo.Instance.inventory[recipeList[i].output_id].name + "/inv");
//					else if(recipeList[i].type == "weapon") 
//						//ingridient_main.GetComponent<Image>().sprite = GUIController.Instance.ItemImageList[PlayerInfo.Instance.weaponList[recipeList[i].output_id].image_id];
//						ingridient_main.GetComponent<Image>().sprite = SpriteManager.Instatce.GetSprite(PlayerInfo.Instance.weaponList[recipeList[i].output_id].image_path);
//				} 
//				else if(ingridient_main.name == "RecipeText")
//				{
//					if(recipeList[i].type == "item") ingridient_main.GetComponent<Text>().text = Localization.Instance.GetLocale(PlayerInfo.Instance.inventory[recipeList[i].output_id].text_id);
//					else if(recipeList[i].type == "weapon") ingridient_main.GetComponent<Text>().text = Localization.Instance.GetLocale(PlayerInfo.Instance.weaponList[recipeList[i].output_id].text_id);
//				}
//				else if(ingridient_main.name == "CraftButton")
//				{
//					CraftingButton = ingridient_main.gameObject;
//					foreach(Transform text in ingridient_main)
//					{
//						if(text.name == "CraftingButtonText") text.GetComponent<Text>().text = Localization.Instance.GetLocale(862);
//					}
//				}
//				else if(ingridient_main.name == "MindRequirementText")
//				{
//					MindReqText = ingridient_main.GetComponent<Text>();
//					MindReqText.gameObject.SetActive(false);
//				}
//			}
//
//			int ingr_count = 0;
//			int l = 0;
//			foreach(string ingridient in recipeList[i].ingridients)
//			{
//				ingrBoxList[l].SetActive(true);
//				ingrBoxList[l].GetComponent<ActionClick>().item_name = ingridient;
//				foreach(Transform text in ingrBoxList[l].transform)
//				{
//					if(text.name == "IngridientText")
//					{
//						text.GetComponent<Text>().text = PlayerInfo.Instance.GetItemText(ingridient);
//						foreach(Item item in PlayerInfo.Instance.inventory)
//						{
//							if(item.name == ingridient && Inventory.Instance.HasTool(item.name))
//							{
//								ingrBoxList[l].GetComponent<Toggle>().isOn = true;
//								ingr_count++;
//							} 
//						}
//						foreach(Weapon weapon in PlayerInfo.Instance.weaponList)
//						{
//							if(weapon.name == ingridient && Inventory.Instance.HasTool(weapon.name))
//							{
//								ingrBoxList[l].GetComponent<Toggle>().isOn = true;
//								ingr_count++;
//							} 
//						}
//					}
//				}
//				l++;
//			}
//
//			if(ingr_count == recipeList[i].ingridients.Count && PlayerInfo.Instance.mind >= recipeList[i].mindReq)
//			{
//				CraftingButton.SetActive(true);
//				CraftingButton.GetComponent<ActionClick>().id = recipeList[i].output_id;
//			}
//			else if(PlayerInfo.Instance.mind < recipeList[i].mindReq)
//			{
//				MindReqText.gameObject.SetActive(true);
//				CraftingButton.SetActive(false);
//				MindReqText.text = Localization.Instance.GetLocale(71) + " " + recipeList[i].mindReq;
//			}
//			else
//			{
//				CraftingButton.SetActive(false);
//				MindReqText.gameObject.SetActive(true);
//				MindReqText.text = Localization.Instance.GetLocale (866);
//			} 
//
//			j++;
//			if(j == recipeTextList.Count) break;
//		}
//	}
//
//
//	public void OnCraftButton(int output_id)
//	{
//		Recipe selected_recipe = new Recipe();
//		foreach(Recipe recipe in recipeList)
//		{
//			if(recipe.output_id == output_id)
//			{
//				selected_recipe = recipe;
//			}
//		}
//
//		string output_item = "";
//		if(selected_recipe.type == "item") output_item = PlayerInfo.Instance.inventory[output_id].name;
//		else if(selected_recipe.type == "weapon") output_item = PlayerInfo.Instance.weaponList[output_id].name;
//
//		foreach(string ingridient in selected_recipe.ingridients)
//		{
//			PlayerInfo.Instance.EquipItem(ingridient,false,false);
//			//PlayerInfo.Instance.EquipWeapon(ingridient,false);
//		}
//		
//		if(selected_recipe.type == "item") PlayerInfo.Instance.EquipItem(output_item,true,false);
//		if(selected_recipe.type == "weapon") PlayerInfo.Instance.EquipWeapon(output_item,true);
//
//		OpenCraftingWindow();
//
//	}
//
//	public void onArrowButton(int value)
//	{
//		current_page += value;
//		if(current_page < 0) current_page = 0;
//		OpenCraftingWindow();
//	}

}
