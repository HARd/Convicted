/*
	PuzzlePart - обеспечивает логику PuzzlePart.
	Автор: Гарбуз В.П.
*/


//using UnityEngine;
//
//using ObjectsExtensionMethods;
//
//namespace LocalMinigames
//{
//	public class PuzzlePart : MonoBehaviour
//	{
//		[SerializeField]
//		public GameObject target;
//		[SerializeField]
//		float moveTime = 0.2f;
//
//
//		void Awake()
//		{
//			target.SetVisible(false);
//		}
//
//		void OnPartDropObject(DragObject drag) //SendMessage handler
//		{
//			drag.enabled = false;
//			transform.parent = target.transform;
//			this.MoveTo(0, 0, moveTime).SetFinalMessage(gameObject, "OnMoveToFinish");
//		}
//
//		public void SetPartDropObject() //SendMessage handler
//		{
//			OnMoveToFinish();
//		}
//
//		void OnMoveToFinish()
//		{
//			target.SetVisible(true);
//			gameObject.SetActive(false);
//		}
//	}
//}
