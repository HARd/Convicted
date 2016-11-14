using UnityEngine;
using System;

namespace ObjectsExtensionMethods
{
	public class Changer : MonoBehaviour
	{
		public float requiredTime;
		public float changingTime = 0;

		GameObject finalMsgObject;
		string finalMsgName;
		object finalMsgValue;
		SendMessageOptions finalMsgOptions;

		public event EventHandler Finished;

		protected virtual void OnFinished()
		{
			if (Finished != null)
				Finished(this, EventArgs.Empty);
		}

		public virtual void Launch()
		{
			changingTime = 0;
			enabled = true;
		}

		public void Stop()
		{
			enabled = false;
		}


		public void Finish()
		{
			changingTime = 0;
			Stop();
			OnFinished();
			if (finalMsgObject != null && finalMsgName != null)
				finalMsgObject.SendMessage(finalMsgName, finalMsgValue, finalMsgOptions);
		}

		public void SetFinalMessage(GameObject gameObject, string methodName, object value = null, SendMessageOptions options = SendMessageOptions.RequireReceiver)
		{
			finalMsgObject = gameObject;
			finalMsgName = methodName;
			finalMsgValue = value;
			finalMsgOptions = options;
		}

	}

	public class Mover : Changer
	{
		public Vector2 Value;
		public Vector2 startValue;
		public Vector2 finalValue;
		public bool worldSpace = false;

		protected virtual void RecalcValue()
		{
			Value = startValue + (finalValue - startValue) * changingTime / requiredTime;
		}

		void Update()
		{
			changingTime += Time.deltaTime;
			RecalcValue();

			if (changingTime >= requiredTime)
				Value = finalValue;

			if (worldSpace)
				transform.position = Value;
			else
				transform.localPosition = Value;

			if (Value == finalValue)
				Finish();
		}

	}

	public class BezierMover : Mover
	{
		public Vector2 delta0Handle = new Vector2(0f, 0f);
		public Vector2 delta1Handle = new Vector2(0f, 0f);

		float x0, y0;
		float x1, y1;

		float xH0, yH0;
		float xH1, yH1;

		public override void Launch()
		{
			x0 = startValue.x;
			y0 = startValue.y;

			x1 = finalValue.x;
			y1 = finalValue.y;

			xH0 = x0 + delta0Handle.x;
			yH0 = y0 + delta0Handle.y;

			xH1 = x1 + delta1Handle.x;
			yH1 = y1 + delta1Handle.y;

			base.Launch();
		}

		protected override void RecalcValue()
		{

			float t = changingTime / requiredTime;
			float oT = 1 - t;

			float t2 = t * t;
			float oT2 = oT * oT;

			float a0 = oT2 * oT;
			float a1 = 3 * t * oT2;
			float a2 = 3 * t2 * oT;
			float a3 = t2 * t;

			float x = a0 * x0 + a1 * xH0 + a2 * xH1 + a3 * x1;
			float y = a0 * y0 + a1 * yH0 + a2 * yH1 + a3 * y1;

			Value = new Vector2(x, y);
		}
	}

	public class Scaler : Changer
	{
		public Vector2 Value;
		public Vector2 startValue;
		public Vector2 finalValue;

		void Update()
		{
			changingTime += Time.deltaTime;
			Value = startValue + (finalValue - startValue) * changingTime / requiredTime;

			if (changingTime >= requiredTime)
				Value = finalValue;

			transform.localScale = new Vector3(Value.x, Value.y, 1f);

			if (Value == finalValue)
				Finish();
		}

	}


	public class Rotator : Changer
	{
		public float Value;
		public float startValue;
		public float finalValue;
		public bool worldSpace = false;
		public bool rotateClockwise = true;

		float angleDistance;

		public override void Launch()
		{
			angleDistance = GetAngleDistance(finalValue, startValue, rotateClockwise);
			base.Launch();
		}

		public static float GetAngleDistance(float angle1, float angle2, bool rotateClockwise)
		{
			float angleDistance = angle1 - angle2;

			if (!rotateClockwise && angleDistance < 0)
				angleDistance = 360 - Mathf.Abs(angleDistance);
			else if (rotateClockwise && angleDistance > 0)
				angleDistance = -(360 - Mathf.Abs(angleDistance));

			return angleDistance;
		}

		void Update()
		{
			changingTime += Time.deltaTime;
			Value = startValue + angleDistance * changingTime / requiredTime;

			if (changingTime >= requiredTime)
				Value = finalValue;

			if (worldSpace)
				transform.eulerAngles = new Vector3(0f, 0f, Value);
			else
				transform.localEulerAngles = new Vector3(0f, 0f, Value);

			if (Value == finalValue)
				Finish();

		}

	}

	public abstract class FloatValueChanger : Changer
	{
		public float Value;
		public float startValue;
		public float finalValue;

		void Update()
		{
			changingTime += Time.deltaTime;
			Value = startValue + (finalValue - startValue) * changingTime / requiredTime;

			if (changingTime >= requiredTime)
				Value = finalValue;

			UpdateValue();

			if (Value == finalValue)
				Finish();
		}

		protected abstract void UpdateValue();
	}

//	public class OpacityChanger : FloatValueChanger
//	{
//		protected override void UpdateValue()
//		{
//			gameObject.SetBranchOpacity(Value);
//		}
//	}

	public class AudioSourceVolumeChanger : FloatValueChanger
	{
		public AudioSource audioSource;

		protected override void UpdateValue()
		{
			audioSource.volume = Value;
		}
	}

	public static class TransformExtensionsClass
	{
		public static Changer MoveTo(this Transform transform, float X, float Y, float time, bool worldSpace = false)
		{
			Mover changer = transform.GetComponent<Mover>();
			if (changer == null)
				changer = transform.gameObject.AddComponent<Mover>();

			changer.startValue = (Vector2)(worldSpace ? transform.position : transform.localPosition);
			changer.finalValue = new Vector2(X, Y);
			changer.requiredTime = time;
			changer.worldSpace = worldSpace;

			changer.Launch();

			return changer;
		}

		public static Changer BezierMoveTo(this Transform transform, float X, float Y, float time,
											Vector2 delta0Handle = default(Vector2), Vector2 delta1Handle = default(Vector2), bool worldSpace = false)
		{
			BezierMover changer = transform.GetComponent<BezierMover>();
			if (changer == null)
				changer = transform.gameObject.AddComponent<BezierMover>();

			changer.startValue = (Vector2)(worldSpace ? transform.position : transform.localPosition);
			changer.finalValue = new Vector2(X, Y);
			changer.requiredTime = time;
			changer.worldSpace = worldSpace;

			changer.delta0Handle = delta0Handle;
			changer.delta1Handle = delta1Handle;

			changer.Launch();

			return changer;
		}

		public static Changer ScaleTo(this Transform transform, Vector2 scale, float time)
		{
			Scaler changer = transform.GetComponent<Scaler>();
			if (changer == null)
				changer = transform.gameObject.AddComponent<Scaler>();

			changer.startValue = (Vector2)transform.localScale;
			changer.finalValue = scale;
			changer.requiredTime = time;

			changer.Launch();

			return changer;
		}

		public static Changer MoveTo(this MonoBehaviour monoBehaviour, float X, float Y, float time, bool worldSpace = false)
		{
			return monoBehaviour.transform.MoveTo(X, Y, time, worldSpace);
		}

		public static Changer BezierMoveTo(this MonoBehaviour monoBehaviour, float X, float Y, float time,
										   Vector2 delta0Handle = default(Vector2), Vector2 delta1Handle = default(Vector2), bool worldSpace = false)
		{
			return monoBehaviour.transform.BezierMoveTo(X, Y, time, delta0Handle, delta1Handle, worldSpace);
		}

		public static Changer ScaleTo(this MonoBehaviour monoBehaviour, Vector2 scale, float time)
		{
			return monoBehaviour.transform.ScaleTo(scale, time);
		}

		public static Changer Move(this Transform transform, float X, float Y, float time, bool worldSpace = false)
		{
			Vector2 position = worldSpace ? transform.position : transform.localPosition;
			return transform.MoveTo(position.x + X, position.y + Y, time, worldSpace);
		}

		public static Changer Move(this MonoBehaviour monoBehaviour, float X, float Y, float time, bool worldSpace = false)
		{
			return monoBehaviour.transform.Move(X, Y, time, worldSpace);
		}


		public static Changer RotateTo(this Transform transform, float angle, float time, bool worldSpace = false, bool rotateClockwise = true)
		{
			Rotator changer = transform.GetComponent<Rotator>();
			if (changer == null)
				changer = transform.gameObject.AddComponent<Rotator>();

			changer.startValue = worldSpace ? transform.eulerAngles.z : transform.localEulerAngles.z;
			changer.finalValue = angle;
			changer.requiredTime = time;
			changer.worldSpace = worldSpace;
			changer.rotateClockwise = rotateClockwise;

			changer.Launch();

			return changer;
		}

		public static Changer RotateTo(this MonoBehaviour monoBehaviour, float angle, float time, bool worldSpace = false, bool rotateClockwise = true)
		{
			return RotateTo(monoBehaviour.transform, angle, time, worldSpace, rotateClockwise);
		}

//		public static Changer ChangeOpacity(this GameObject gameObject, float startOpacity, float finalOpacity, float time)
//		{
//			OpacityChanger changer = gameObject.GetComponent<OpacityChanger>();
//			if (changer == null)
//				changer = gameObject.AddComponent<OpacityChanger>();
//
//			changer.startValue = startOpacity;
//			changer.finalValue = finalOpacity;
//			changer.requiredTime = time;
//
//			changer.Launch();
//
//			return changer;
//		}
//
		public static Changer ChangeVolume(this AudioSource audioSource, float startVolume, float finalVolume, float time)
		{
			AudioSourceVolumeChanger changer = Array.Find(audioSource.GetComponents<AudioSourceVolumeChanger>(), x => x.audioSource == audioSource);
			if (changer == null)
			{
				changer = audioSource.gameObject.AddComponent<AudioSourceVolumeChanger>();
				changer.audioSource = audioSource;
			}

			audioSource.volume = startVolume;
			changer.startValue = startVolume;
			changer.finalValue = finalVolume;
			changer.requiredTime = time;

			changer.Launch();

			return changer;
		}

		public static Changer ChangeVolumeTo(this AudioSource audioSource, float volume, float time)
		{
			return ChangeVolume(audioSource, audioSource.volume, volume, time);
		}

//		public static Changer ChangeOpacity(this SpriteRenderer renderer, float startOpacity, float finalOpacity, float time)
//		{
//			return renderer.gameObject.ChangeOpacity(startOpacity, finalOpacity, time);
//		}
//
//		public static Changer ChangeOpacity(this MonoBehaviour monoBehaviour, float startOpacity, float finalOpacity, float time)
//		{
//			return monoBehaviour.gameObject.ChangeOpacity(startOpacity, finalOpacity, time);
//		}
//
//		public static Changer ChangeOpacityTo(this GameObject gameObject, float finalOpacity, float time)
//		{
//			return gameObject.ChangeOpacity(gameObject.GetBranchOpacity(), finalOpacity, time);
//		}
//
//		public static Changer ChangeOpacityTo(this SpriteRenderer renderer, float finalOpacity, float time)
//		{
//			return renderer.ChangeOpacity(renderer.gameObject.GetBranchOpacity(), finalOpacity, time);
//		}
//
//		public static Changer ChangeOpacityTo(this MonoBehaviour monoBehaviour, float finalOpacity, float time)
//		{
//			return monoBehaviour.gameObject.ChangeOpacity(monoBehaviour.gameObject.GetBranchOpacity(), finalOpacity, time);
//		}

	}
}
