using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectsExtensionMethods
{
	public static class ObjectsExtensionMethodsClass
	{
		public static bool BroadCastSinglMessage<T>(this GameObject go, string methodName, Predicate<T> p)
		{
			foreach(Transform transform in go.transform)
			{
				T component = transform.GetComponent<T>();
				if(component != null && p(component)) 
				{
					transform.SendMessage(methodName);
					return true;
				}
			}
			return false;
		}

		public static bool BroadCastSinglMessage<T>(this MonoBehaviour comp, string methodName, Predicate<T> p)
		{
			return comp.gameObject.BroadCastSinglMessage<T>(methodName, p);
		}


		public static T GetFirstComponentInChildren<T>(this GameObject go)
		{
			foreach(Transform transform in go.transform)
			{
				T component = transform.GetComponent<T>();
				if(component != null) 
					return component;
			}
			return default(T);
		}

		public static T GetFirstComponentInChildren<T>(this MonoBehaviour mb)
		{
			return mb.gameObject.GetFirstComponentInChildren<T>();
		}

		public static List<T> GetChildrenComponents<T>(this GameObject go)
		{
			List<T> list = new List<T>();
			foreach(Transform transform in go.transform)
			{
				T component = transform.GetComponent<T>();
				if(component != null) 
					list.Add(component);
			}
			return list;
		}

		public static List<T> GetChildrenComponents<T>(this MonoBehaviour mb)
		{
			return mb.gameObject.GetChildrenComponents<T>();
		}
	}
}
