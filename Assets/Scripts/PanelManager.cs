using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PanelManager : MonoBehaviour 
{
	private static PanelManager _instance;
	public static PanelManager Instance { get { return _instance ?? (_instance = FindObjectOfType<PanelManager>()); } }
	protected PanelManager() { _instance = null; }


	public ActionPanel ActionPanel;
	public EventPanelController EventPanel;
	public CraftingPanelController CraftingPanel;
	public QuestPanelController QuestPanel;
	public CharInfoController CharInfoPanel;

	public Button ReturnButton;
	public Button CharInfoButton;
	public Button QuestLogButton;
	public Button CraftingButton;
	public Button InventoryButton;

	public Text questLogCountText;

	[SerializeField]
	Image imageReturnButton;
	[SerializeField]
	Sprite spritArrow;
	[SerializeField]
	Sprite spritDoor;

	//public GameObject smokeAnimation;

	GameObject currentPanel;


	public void RefreshCurrentPanel()
	{
		imageReturnButton.sprite = spritArrow;
		if (CharInfoPanel.gameObject.activeSelf)
			currentPanel = CharInfoPanel.gameObject;
		else if (QuestPanel.gameObject.activeSelf)
			currentPanel = QuestPanel.gameObject;
		else if (CraftingPanel.gameObject.activeSelf)
			currentPanel = CraftingPanel.gameObject;
		else
		{
			currentPanel = null;
			ActionPanel.gameObject.SetActive(true);
			imageReturnButton.sprite = spritDoor;
		}

		if(EventManager.Instance.current_event.tag == "GeneralEvent")
			EventManager.Instance.LaunchGeneralEvent(true);
	}

	public void CloseButton()
	{
		if(!IsActionPanel())
		{
			currentPanel.SetActive(false);
			RefreshCurrentPanel();
		}
		else if(!EventPanel.gameObject.activeSelf)
		{
			GUIController.Instance.CreateExiteMainMenuDialog();
		}
	}

	public void CallCharPanel()
	{
		AudioManager.Instance.Play(1);
		if(!CharInfoPanel.gameObject.activeSelf)
		{
			CloseAllPanel();
			CharInfoPanel.gameObject.SetActive(true);
			ActionPanel.gameObject.SetActive(true);
			RefreshCurrentPanel();
			CharInfoPanel.DrawCharWindow();
		}
		else if(CharInfoPanel.gameObject.activeSelf) 
		{
			CharInfoPanel.gameObject.SetActive(false);
			RefreshCurrentPanel();
		}
	}

	public void CallQuestPanel() 
	{
		AudioManager.Instance.Play(1);
		if(!QuestPanel.gameObject.activeSelf) 
		{
			CloseAllPanel();
			QuestPanel.gameObject.SetActive(true);
			RefreshCurrentPanel();
			PanelManager.Instance.QuestPanel.OpenQuestLog();
			RefreshCurrentPanel ();
		} 
		else 
		{
			QuestPanel.gameObject.SetActive(false);
			RefreshCurrentPanel();
		}
	}

	public void CallEventPanel(bool value) 
	{
		EventPanel.gameObject.SetActive(value);
		ReturnButton.gameObject.SetActive(!value);
	}

	void CloseAllPanel()
	{
		ActionPanel.gameObject.SetActive(false);
		CharInfoPanel.gameObject.SetActive(false);
		QuestPanel.gameObject.SetActive(false);
		CraftingPanel.gameObject.SetActive(false);
	}

	public void CallCraftingPanel() 
	{
		AudioManager.Instance.Play(1);
		if(!CraftingPanel.gameObject.activeSelf) 
		{
			CloseAllPanel();
			CraftingPanel.gameObject.SetActive(true);
			RefreshCurrentPanel();
			CraftingPanel.OpenCraftingWindow();
		}
		else
		{
			CraftingPanel.gameObject.SetActive(false);
			RefreshCurrentPanel();
		}
	}


	public void RemoveQuest(string item_name) 
	{
		AudioManager.Instance.Play(0);
		if (!PlayerInfo.Instance.isTutorial)
			PanelManager.Instance.QuestPanel.RemoveQuest(item_name);
	}

//	public void ReturnToMenu() 
//	{
//		AudioManager.Instance.Play(1);
//		SaveLoadXML.DeleteGameXML();
//		SceneManager.LoadScene(1);
//	}

	public void ClickDown(GameObject _object) 
	{
		if(WorldTime.Instance.TimeSpeed == 1) 
			_object.GetComponent<Text>().color = new Color32(255,175,0,255);
	}

	public void ClickUp(GameObject _object) 
	{
		_object.GetComponent<Text>().color = new Color32(50,50,50,255);
	}

	void Update()
	{
//		if(currentPanel == null) 
//			ReturnButton.gameObject.SetActive(false);
//		else 
//			ReturnButton.gameObject.SetActive(true);

		if(StoryManager.Instance != null)
			questLogCountText.text = (QuestPanel.questLog.Count + StoryManager.Instance.activeStoreCounter).ToString();
	}

	public bool IsActionPanel()
	{
		return currentPanel == null;
	}

	public void Endgame()
	{
		PlayerInfo.Instance.CharacterComplete();
	}
}
