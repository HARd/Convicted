/*
	Puzzle наследник Minigame.
	Автор: Гарбуз В.П.
*/


//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Minigames;
//using ObjectsExtensionMethods;
//
//namespace LocalMinigames
//{
//	public class Puzzle : Minigame
//	{
//		List<PuzzleDragTarget> puzzleDragTargetList = new List<PuzzleDragTarget>();
//
//		public void AddTarget(PuzzleDragTarget puzzleDragTarget)
//		{
//			puzzleDragTargetList.Add(puzzleDragTarget);
//		}
//
//		public void TargetComplete()
//		{
//			Save();
//			OnChangeFinished();
//		}
//
//		public override void Save()
//		{
//			List<string> saveGame = new List<string>();
//			foreach (var puzzleDragTarget in puzzleDragTargetList)
//			{
//				string str = puzzleDragTarget.ID + " " + puzzleDragTarget.IsComplete.ToString() + " " + puzzleDragTarget.dragObject.GetComponent<PuzzlePartRotator>().CurrentTurn.ToString();
//				saveGame.Add(str);
//			}
//			PlayerDB.Data.SetValue(saveKey, saveGame.ToArray());
//		}
//
//		public override void Load() 
//		{ 
//			if (PlayerDB.Data.HasKey(saveKey))
//			{
//				string[] strArray = PlayerDB.Data.GetArrayValue(saveKey);
//				foreach(var str in strArray)
//				{
//					string[] s = str.Split(new Char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
//					PuzzleDragTarget puzzleDragTarget = puzzleDragTargetList.Find(p => p.ID == s[0]);
//					puzzleDragTarget.IsComplete = Convert.ToBoolean(s[1]);
//					puzzleDragTarget.dragObject.GetComponent<PuzzlePartRotator>().setTurn(Convert.ToInt32(s[2]));
//					if(puzzleDragTarget.IsComplete)
//						puzzleDragTarget.SetDropObject();
//				}
//			}
//		}
//
//		public override bool IsCorrect()
//		{
//			return !puzzleDragTargetList.Exists(p => p.IsComplete == false);
//		}
//
//		protected override void OnSetActive()
//		{
//			foreach (var puzzleDragTarget in puzzleDragTargetList)
//			{
//				puzzleDragTarget.SetRespondable(IsActive);
//				puzzleDragTarget.dragObject.SetRespondable(IsActive);
//			}
//		}
//
//		public override void PlaySkipAnimation() 
//		{ 
//			float time = 0f;
//			foreach (var puzzleDragTarget in puzzleDragTargetList)
//			{
//				if(!puzzleDragTarget.IsComplete)
//				{
//					puzzleDragTarget.IsComplete = true;
//					PuzzlePartRotator rotator = puzzleDragTarget.dragObject.GetComponent<PuzzlePartRotator>();
//					if(!rotator.IsTurnCorrect)
//					{
//						rotator.CurrentTurn = rotator.correctTurn;
//					}
//					StartCoroutine(SkipAnimCoroutine(puzzleDragTarget, time));
//					time += 0.2f;
//				}
//			}
//		}
//
//		IEnumerator SkipAnimCoroutine(PuzzleDragTarget puzzleDragTarget, float time)
//		{
//			yield return new WaitForSeconds(time);
//			puzzleDragTarget.dragObject.GetComponent<PuzzlePartRotator>().Rotate();
//			puzzleDragTarget.dragObject.SendMessage("OnPartDropObject", puzzleDragTarget.dragObject, SendMessageOptions.DontRequireReceiver);
//		}
//	}
//}