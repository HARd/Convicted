using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IllustrationLoader : PrefabLoader
{
	[SerializeField]
	string resourceSpritePath;

	[SerializeField]
	string url;

	[SerializeField]
	bool nativeSize = false;

	protected override void DoAction()
	{
		base.DoAction();
		IllustrationScreen screen = InstantiatedPrefab.GetComponent<IllustrationScreen>();
		screen.url = url;
		Image image = screen.bg.GetComponent<Image>();
		image.sprite = Resources.Load<Sprite>(resourceSpritePath);
		InstantiatedPrefab.transform.SetParent(ScreenManager.Instance.transform, worldPositionStays);

		if(nativeSize)
			image.SetNativeSize();
	}
}
