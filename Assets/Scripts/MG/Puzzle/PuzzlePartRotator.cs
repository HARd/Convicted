/*
	PuzzlePart - обеспечивает поворот PuzzlePart объекта.
	Автор: Гарбуз В.П.
*/


//using UnityEngine;
//
//using ObjectsExtensionMethods;
//
//namespace LocalMinigames
//{
//	public class PuzzlePartRotator : MonoBehaviour, ISingleAction
//	{
//		[SerializeField]
//		float rotateTime = 0.2f;
//		[SerializeField]
//		int numberTurns = 3;
//		[SerializeField]
//		public int correctTurn = 0;
//
//		int angleTurn = 0;
//		int defoltAngle = 0;
//
//		int CurrentAngle
//		{
//			get 
//			{ 
//				int angle = _currentTurn*angleTurn + defoltAngle;
//				angle = (angle >= 360) ? angle-360 : angle;
//				return angle; 
//			}
//		}
//
//		int _currentTurn = 0;
//		public int CurrentTurn
//		{
//			get { return _currentTurn; }
//			set
//			{
//				_currentTurn = (value >=  numberTurns) ? 0 : value;
//			}
//		}
//
//
//		public bool IsTurnCorrect
//		{
//			get { return _currentTurn == correctTurn; }
//		}
//			
//		void ISingleAction.Execute()
//		{
//			CurrentTurn++;
//			Rotate();
//		}
//		public void Rotate()
//		{
//			this.RotateTo(CurrentAngle, rotateTime, false, false);
//		}
//
//		public void setTurn(int turn)
//		{
//			CurrentTurn = turn;
//			transform.localEulerAngles = new Vector3(0, 0, CurrentAngle);
//		}
//
//		void Awake()
//		{
//			angleTurn = 360/numberTurns;
//			defoltAngle = (int)transform.localEulerAngles.z;
//		}
//	}
//}
