/*
	PuzzleButtonDragTarget - обеспечивает реакцию на ISingleAction.
	Автор: Гарбуз В.П.
*/


//using UnityEngine;
//using System;
//
//using ObjectsExtensionMethods;
//
//namespace LocalMinigames
//{
//	public class PuzzleButtonDragTarget : MonoBehaviour
//	{
//		[SerializeField]
//		Puzzle puzzle;
//
//		bool isFocusIn = false;
//		bool isFocusOut = true;
//
//		void OnDropObject(DragObject drag) //SendMessage handler
//		{
//			ISingleAction singleAction = drag.GetComponent<ISingleAction>();
//			if(singleAction != null)
//			{
//				drag.ReTake();
//				singleAction.Execute();
//				puzzle.Save();
//			}
//		}
//
//		void OnDragObjectOver(DragObject drag) //SendMessage handler
//		{
//			if(!isFocusIn && isFocusOut)
//			{
//				isFocusOut = false;
//				SendMessage("OnFocusIn");
//			}
//			isFocusIn = true;
//		}
//
//		void LateUpdate()
//		{
//			if(!isFocusIn && !isFocusOut)
//			{
//				isFocusOut = true;
//				SendMessage("OnFocusOut");
//			}
//			isFocusIn = false;
//		}
//	}
//}
