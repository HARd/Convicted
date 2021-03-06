﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ActionMGController : MonoBehaviour
{
	[SerializeField]
	string MgName;

	public GeneralEvent MgComplite;

	public GeneralEvent MgNotComplite;

	void action_mg()
	{
		MgBase mg = ScreenManager.Instance.CreateScreen(MgName).GetComponent<MgBase>();
		mg.onDestroy += () => {
			if(mg.isComplite)
				MgComplite.Launch();
			else
				MgNotComplite.Launch();
		};
	}
}
