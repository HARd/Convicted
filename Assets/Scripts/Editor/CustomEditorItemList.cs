using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(ItemList))]
public class CustomEditorItemList : Editor 
{
//	public ItemList myTarget;
//
//	SerializedObject itemList;
//	SerializedProperty list;
//
//	public void OnEnable()
//	{
//		itemList = new SerializedObject(target);
//		list = itemList.FindProperty("itemList");
//		myTarget = (ItemList)target;
//		for(int i = 0; i < list.arraySize; i++)
//			myTarget.itemList[i].OnEnable(list.GetArrayElementAtIndex(i));
//		//mySerProp.enumValueIndex = (int)(MyEnumType)EditorGUILayout.EnumPopup("My Enum:", (MyEnumType)Enum.GetValues(typeof(MyEnumType)).GetValue(mySerProp.enumValueIndex));
//	}
//
//
//	public override void OnInspectorGUI() 
//	{
//		itemList.Update();
//
//		list.arraySize = EditorGUILayout.IntField("Size", list.arraySize);
//		itemList.ApplyModifiedProperties();
//
//		for(int i = 0; i < list.arraySize; i++)
//		{
//			if (GUI.changed) 
//				myTarget.itemList[i].OnEnable(list.GetArrayElementAtIndex(i));
//			
//			myTarget.itemList[i].Draw();
//		}
//
//		if (GUI.changed) 
//			itemList.ApplyModifiedProperties();
//	}
}


