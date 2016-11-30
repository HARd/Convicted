
using UnityEngine;
using System.Collections;

public class AnimatorDestroer : MonoBehaviour
{
	[SerializeField]
	bool whenOnGeneralEvent;

	[SerializeField]
	Animator animator;

	[SerializeField]
	float delay = 0f;

	public System.Action onDestroy;

	void Start()
	{
		if (whenOnGeneralEvent)
			animator.enabled = false;
		else
		{
			Destroy(gameObject, delay);
			AudioManager.Instance.Play(3); 
		}
	}

	void Update()
	{
		if(!animator.enabled && !PanelManager.Instance.EventPanel.gameObject.activeSelf)
		{
			animator.enabled = true;
			Destroy(gameObject, delay);
			AudioManager.Instance.Play(3); 
		}
	}

	void OnDestroy()
	{
		onDestroy();
	}
}
