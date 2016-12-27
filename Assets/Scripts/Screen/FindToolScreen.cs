using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ObjectsExtensionMethods;

public class FindToolScreen : ScreenBase
{
	[SerializeField]
	GameObject bg;

	[SerializeField]
	Image image;

	[SerializeField]
	Text headerText;

	[SerializeField]
	Text text;


	bool blocker = false;
	bool equip = true;
	Image screenImage;

	void Start()
	{
		screenImage = GetComponent<Image>();
		bg.SetActive(false);
		screenImage.enabled = false;

	}

	void Update()
	{
		if(!screenImage.enabled && !PanelManager.Instance.EventPanel.gameObject.activeSelf)
		{
			screenImage.enabled = true;
			bg.SetActive(true);
			AudioManager.Instance.Play(3); 
		}
	}

	public void SetScreenView(Sprite sprite, string text, bool equip)
	{
		image.sprite = sprite;
		this.text.text = text;
		headerText.text = Localization.Instance.GetLocale((equip) ? 2512 : 2513);
		this.equip = equip;
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		if(!blocker)
		{
			blocker = true;
			if(equip)
			{
				image.transform.SetParent(transform.GetParentCanvas().transform, true);
				image.ScaleTo(new Vector2(0.2f, 0.2f), 0.4f);
				image.MoveTo(330f, -600f, 0.4f).SetFinalMessage(gameObject, "OnArrivedToHome", null, SendMessageOptions.DontRequireReceiver);
				bg.SetActive(false);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}

	void OnArrivedToHome()
	{
		Destroy(gameObject);
		Destroy(image.gameObject);
	}
}
