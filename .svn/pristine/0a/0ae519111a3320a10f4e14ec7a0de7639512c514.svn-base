using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IllustrationLoader : PrefabLoader
{
	[SerializeField]
	string resourceSpritePath;

	[SerializeField]
	bool nativeSize = false;

	protected override void DoAction()
	{
		base.DoAction();
		Image image = InstantiatedPrefab.GetComponent<IllustrationScreen>().bg.GetComponent<Image>();
		image.sprite = Resources.Load<Sprite>(resourceSpritePath);
		InstantiatedPrefab.transform.SetParent(ScreenManager.Instance.transform, worldPositionStays);

		if(nativeSize)
			image.SetNativeSize();
	}
}
