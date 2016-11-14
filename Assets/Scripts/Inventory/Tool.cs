using UnityEngine;
using ObjectsExtensionMethods;

public class Tool : MonoBehaviour//
{
	ToolHolder holder;
	public ToolHolder Holder { get { return holder; } }

	bool isDestroying = false;

	protected virtual void SetInvView(){}

	public string Name { get; set; }


	public void AttachToHolder(ToolHolder holder)
	{
		if (holder.Tool != null && holder.Tool != this)
		{
			Destroy(gameObject);
			return;
		}

		this.holder = holder;
		holder.Tool = this;
		holder.name = Name;
		SetInvView();
		transform.SetParent(holder.toolAncher, false);
	}

	public void Destroy()
	{
		if (isDestroying)
			return;

		isDestroying = true;
		transform.SetParent(null);
		holder.Remove();
	}

	public void OnFadingFinish()
	{
		Destroy(gameObject);
	}
}
