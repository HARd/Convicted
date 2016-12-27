using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EditorBaseItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler 
{
	public Text textName;

	public Transform storyTransform;

	public Color selectColor;

	public Color enterColor;

	Color startColor;
	Image image;

	void Awake() 
	{
		image = GetComponent<Image>();
		startColor = image.color;
	}

	public virtual void OnPointerClick (PointerEventData eventData)
	{
		if(!IsSelected())
			SetColor(selectColor);
		
		#if UNITY_EDITOR
		Selection.activeGameObject = storyTransform.gameObject;
		#endif
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if(!IsSelected())
			SetColor(enterColor);
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		if(!IsSelected())
			ResetColor();
	}

	public void SetColor(Color color)
	{
		image.color = color;
	}

	public void SetStartColor(Color color)
	{
		startColor = color;
		ResetColor();
	}

	public void ResetColor()
	{
		image.color = startColor;
	}

	public void SetRespondable(bool value)
	{
		image.raycastTarget = value;
	}

	public bool IsSelected()
	{
		return image.color == selectColor;
	}

	static public EditorBaseItem CreatEditorChangeItem(GameObject story, GameObject prefab, Transform parent, string text = "")
	{
		GameObject action = Instantiate(prefab, parent, false) as GameObject;
		action.name = story.name;
		EditorBaseItem eAction = action.GetComponent<EditorBaseItem>();
		eAction.storyTransform = story.transform;
		eAction.textName.text = text;
		return eAction;
	}
}
