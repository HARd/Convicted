using UnityEngine;
using UnityEngine.UI;
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


		public static T GetFirstComponentInChildren<T>(this GameObject go, Predicate<T> p = null)
		{
			if(p == null)
				p = (x => true);
			
			foreach(Transform transform in go.transform)
			{
				T component = transform.GetComponent<T>();
				if(component != null && p(component)) 
					return component;
			}
			return default(T);
		}

		public static T GetFirstComponentInChildren<T>(this MonoBehaviour mb, Predicate<T> p = null)
		{
			return mb.gameObject.GetFirstComponentInChildren<T>(p);
		}

		public static List<T> GetChildrenComponents<T>(this GameObject go, Predicate<T> p = null)
		{
			if(p == null)
				p = (x => true); 
			
			List<T> list = new List<T>();
			foreach(Transform transform in go.transform)
			{
				T component = transform.GetComponent<T>();
				if(component != null && p(component)) 
					list.Add(component);
			}
			return list;
		}

		public static List<T> GetChildrenComponents<T>(this MonoBehaviour mb, Predicate<T> p = null)
		{
			return mb.gameObject.GetChildrenComponents<T>(p);
		}

		public static List<T> GetChildrenComponents<T>(this Transform mb, Predicate<T> p = null)
		{
			return mb.gameObject.GetChildrenComponents<T>(p);
		}

		public static Canvas GetParentCanvas(this Transform t)
		{
			Canvas canvas = t.GetComponent<Canvas>();
			return (canvas == null) ? t.parent.GetParentCanvas() : canvas;
		}
	}
}
