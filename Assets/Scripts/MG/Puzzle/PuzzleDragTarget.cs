/*
	PuzzleDragTarget - обеспечивает прием PuzzlePart DragObject.
	Автор: Гарбуз В.П.
*/

//
//using UnityEngine;
//
//using ObjectsExtensionMethods;
//
//namespace LocalMinigames
//{
//	public class PuzzleDragTarget : MonoBehaviour
//	{
//		[SerializeField]
//		public string ID;
//		[SerializeField]
//		public DragObject dragObject;
//		[SerializeField]
//		Puzzle puzzle;
//
//		public bool IsComplete {set;get;}
//
//		bool _isAvailable;
//		public bool IsAvailable
//		{
//			get { return _isAvailable; }
//			set
//			{
//				_isAvailable = value;
//				this.SetRespondable(_isAvailable);
//				this.SetVisible(_isAvailable);
//			}
//		}
//
//		void Awake()
//		{
//			puzzle.AddTarget(this);
//		}
//
//		void OnDropObject(DragObject drag) //SendMessage handler
//		{
//			if(dragObject == drag)
//			{
//				if(drag.GetComponent<PuzzlePartRotator>().IsTurnCorrect)
//				{
//					IsComplete = true;
//					IsAvailable = false;
//					puzzle.TargetComplete();
//					drag.SendMessage("OnPartDropObject", drag, SendMessageOptions.DontRequireReceiver);
//				}
//			}
//		}
//		public void SetDropObject()
//		{
//			IsAvailable = false;
//			dragObject.SendMessage("SetPartDropObject", dragObject, SendMessageOptions.DontRequireReceiver);
//		}
//	}
//}
