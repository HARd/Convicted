using UnityEngine;
using System.Collections;

[System.Serializable]
public class DataSort : System.IComparable<DataSort>
{
	public int id;
	public int data;
	
	public int CompareTo(DataSort value)
	{
		return this.data.CompareTo(value.data);
	}
}
