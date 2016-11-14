/*
	Управляет анимацией, устанавливая параметр аниматора.
*/


using UnityEngine;
using System.Collections;

public class AnimatorContoller : MonoBehaviour
{
	[SerializeField]
	bool whenOnStart;

	[SerializeField]
	Animator animator;

	[SerializeField]
	float delay = 0f;

	public enum ControlMode { PlayAnimation, SetBool }

	[SerializeField]
	ControlMode mode;

	[SerializeField]
	string parameterName;

	bool parameterBool = true;

	void Start()
	{
		if (whenOnStart)
			Execute();
	}

	public void Execute(string param = "", bool b = true)
	{
		parameterBool = b;
		if(param != "")
			parameterName = param;
		if(delay == 0)
			DoAction();
		else
			StartCoroutine(PlayAction());
	}

	public void Execute(ControlMode mode, string param = "", bool b = true)
	{
		
		this.mode = mode;
		Execute(param, b);
	}


	void DoAction()
	{
		if (mode == ControlMode.PlayAnimation)
			animator.Play(parameterName);
		else
			animator.SetBool(parameterName, parameterBool);
	}


	IEnumerator PlayAction()
	{
		yield return new WaitForSeconds(delay);
		DoAction();
	}
}
