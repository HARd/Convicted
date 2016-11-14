/*
	Компонент управляющий плагином декодирования видео.
	Функции из бинарного плагины вызываются через статический класс-обертку VideoDecoderPlugin
	Создает текстуры и устанавливает их в скопированный при необходимости материал.
	Выполняют работу по инициализации, управлению, синхронного со звуком декодирования видео.
	Автор: Кущ А.Е.
*/


using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class VideoDecoder : MonoBehaviour
{
	Texture2D yTexture;
	Texture2D uTexture;
	Texture2D vTexture;
	Texture2D aTexture;
	IntPtr videoContext;

	float elapsedTime;

	[SerializeField]
	string videoPath;

	[SerializeField]
	bool hasAlphaChannel;

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	bool useAudioTime;

	[SerializeField]
	Material material;

	[SerializeField]
	bool copyMeterial;

	Material workingMaterial;

	public enum StartMode { None, ShowFirstFrame, Play, ShowLastFrame }
	[SerializeField]
	StartMode OnStartMode;

	[SerializeField]
	bool isCycled;

	[SerializeField]
	float speed = 1f;

	long videoFileOffset = 0;
	long videoFileLenght = 0;

	public string VideoPath { get { return videoPath; } set { videoPath = value; } }
	public bool HasAlphaChannel { get { return hasAlphaChannel; } set { hasAlphaChannel = value; } }

	bool GetAndroidPath(string ApkPath, string fileName, out long offset, out long length)
	{
		offset = 0;
		length = 0;
		AndroidJavaObject assetFileDescriptor = null;

		if (Application.dataPath.EndsWith("apk"))
		{
			using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					using (var assetManager = activity.Call<AndroidJavaObject>("getAssets"))
					{
						assetFileDescriptor = assetManager.Call<AndroidJavaObject>("openFd", videoPath);
					}
				}
			}
		}

		if (assetFileDescriptor != null && assetFileDescriptor.GetRawObject() != IntPtr.Zero)
		{
			offset = assetFileDescriptor.Call<long>("getStartOffset");
			length = assetFileDescriptor.Call<long>("getLength");

			assetFileDescriptor.Dispose();
			assetFileDescriptor = null;

			Debug.LogError("Open file: " + fileName + " in Apk at path: " + ApkPath);
			Debug.LogError("offset: " + offset + " lenght: " + length);
		}
		else
		{
			Debug.LogError("Couldn't open file: " + fileName + " in Apk at path: " + ApkPath);
			return false;
		}

		return true;
	}

	public string FullVideoPath
	{
		get
		{
			string path;
			switch (Application.platform)
			{
				case RuntimePlatform.Android:
					//path = "jar:file://" + Application.dataPath + "!/assets/" + videoPath;
					path = Application.dataPath;
					GetAndroidPath(path, videoPath, out videoFileOffset, out videoFileLenght);

					break;
				default:
					path = Application.streamingAssetsPath + "/" + videoPath;
					break;
			}



			return path;
		}
	}
	public AudioSource AudioSource { get { return audioSource; } set { audioSource = value; } }
	public bool UseAudioTime { get { return useAudioTime; } set { useAudioTime = value; } }

	public bool IsCycled { get { return isCycled; } set { isCycled = value; } }
	public float Speed { get { return speed; } set { speed = value; } }

	public bool CopyMeterial { get { return copyMeterial; } set { copyMeterial = value; } }
	public Material WorkingMaterial
	{
		get
		{
			if (workingMaterial == null)
				workingMaterial = copyMeterial ? new Material(material) : material;

			return workingMaterial;
		}
	}
	public Material Material { get { return material; } set { material = value; } }

	public IntPtr Context { get { return videoContext; } }
	public int Width { get; private set; }
	public int Height { get; private set; }
	public float Duration { get; private set; }
	public float Position { get { return videoContext != IntPtr.Zero ? VideoDecoderPlugin.GetTimePosition(videoContext) : -1f; } }

	public bool IsVideoLoaded { get { return videoContext != IntPtr.Zero; } }
	public bool IsPlaying { get; private set; }
	public bool HasAudio { get; private set; }

	public delegate void PlayingActionHandler(VideoDecoder sender);
	public event PlayingActionHandler PlayingCompleted;
	public event PlayingActionHandler PlayingStarted;
	public event PlayingActionHandler PlayingRestarted;

	public void Start()
	{
		switch (OnStartMode)
		{
			case StartMode.Play:
				Play();
				break;
			case StartMode.ShowFirstFrame:
				ShowFirstFrame();
				break;
			case StartMode.ShowLastFrame:
				ShowLastFrame();
				break;
		}
		VideoDecodersManager.Instance.Add(this);
	}

	public void Play()
	{
		StartPlaying();

		if (PlayingStarted != null)
			PlayingStarted(this);
	}

	void StartPlaying()
	{
		ShowFirstFrame();

		if (HasAudio)
		{
			AudioSource.Stop();
			AudioSource.Play();
		}

		elapsedTime = 0;
		IsPlaying = true;
	}

	public void Stop()
	{
		IsPlaying = false;
		if (HasAudio)
			AudioSource.Stop();
	}

	public void ShowFirstFrame()
	{
		if (!IsVideoLoaded)
			LoadVideo();

		if (IsVideoLoaded)
			VideoDecoderPlugin.DecodeFirstFrame(videoContext);
	}

	public void ShowLastFrame()
	{
		if (!IsVideoLoaded)
			LoadVideo();

		if (IsVideoLoaded)
			VideoDecoderPlugin.DecodeLastFrame(videoContext);
	}

	void LoadVideo()
	{
		videoContext = VideoDecoderPlugin.CreateVideoContext(FullVideoPath, HasAlphaChannel, videoFileOffset, videoFileLenght);

		if (VideoDecoderPlugin.GetLastPluginError() != VideoPluginEror.NoError)
		{
			Debug.LogError("CreateVideoContext has an error = " + VideoDecoderPlugin.GetLastPluginError());
			Stop();
			videoContext = IntPtr.Zero;
			return;
		}

		Width = VideoDecoderPlugin.GetVideoFrameWidth(videoContext);
		Height = VideoDecoderPlugin.GetVideoFrameHeight(videoContext);
		Duration = VideoDecoderPlugin.GetVideoDuration(videoContext);
		HasAudio = AudioSource != null && AudioSource.clip != null;

		AllocateTextures();

		ShowFirstFrame();
		GL.IssuePluginEvent(VideoDecoderPlugin.GetRenderEventFunc(), 773);
	}

	void Restart()
	{
		StartPlaying();

		if (PlayingRestarted != null)
			PlayingRestarted(this);
	}

	public void Update()
	{
		if (!IsPlaying || !IsVideoLoaded)
			return;

		elapsedTime = UseAudioTime ? AudioSource.time : elapsedTime + Time.deltaTime * speed;

		bool syncAudioSourceStopped = UseAudioTime && !AudioSource.isPlaying;

		if (IsCycled && (elapsedTime > Duration || syncAudioSourceStopped))
			Restart();

		bool nextFrameSuccesfullyDecoded = VideoDecoderPlugin.DecodeFrameAtTime(videoContext, elapsedTime);
		if (!nextFrameSuccesfullyDecoded || syncAudioSourceStopped)
		{
			if (IsCycled)
				Restart();
			else
				FinishPlaying();
		}
	}

	void FinishPlaying()
	{
		IsPlaying = false;
		if (PlayingCompleted != null)
			PlayingCompleted(this);
	}

	void AllocateTextures()
	{
		yTexture = new Texture2D(Width, Height, TextureFormat.Alpha8, false);
		uTexture = new Texture2D(Width / 2, Height / 2, TextureFormat.Alpha8, false);
		vTexture = new Texture2D(Width / 2, Height / 2, TextureFormat.Alpha8, false);
		yTexture.wrapMode = TextureWrapMode.Clamp;
		uTexture.wrapMode = TextureWrapMode.Clamp;
		vTexture.wrapMode = TextureWrapMode.Clamp;

		yTexture.Apply();
		uTexture.Apply();
		vTexture.Apply();

		VideoDecoderPlugin.SetYTexture(videoContext, yTexture.GetNativeTexturePtr());
		VideoDecoderPlugin.SetUTexture(videoContext, uTexture.GetNativeTexturePtr());
		VideoDecoderPlugin.SetVTexture(videoContext, vTexture.GetNativeTexturePtr());

		if (HasAlphaChannel)
		{
			aTexture = new Texture2D(Width, Height, TextureFormat.Alpha8, false);
			aTexture.wrapMode = TextureWrapMode.Clamp;
			aTexture.Apply();
			VideoDecoderPlugin.SetATexture(videoContext, aTexture.GetNativeTexturePtr());
		}

		UpdateMaterialsTextures();
	}

	void DestroyTextures()
	{
		RemoveMaterialsTextures();

		DestroyTexture(ref yTexture);
		DestroyTexture(ref uTexture);
		DestroyTexture(ref vTexture);
	}

	void DestroyContext()
	{
		if (videoContext != IntPtr.Zero)
		{
			VideoDecoderPlugin.DestroyVideoContext(videoContext);
			videoContext = IntPtr.Zero;
		}
	}

	void DestroyTexture(ref Texture2D texture)
	{
		if (texture != null)
		{
			Destroy(texture);
			texture = null;
		}
	}

	void OnDestroy()
	{
		IsPlaying = false;
		DestroyTextures();
		DestroyContext();

		VideoDecodersManager videoDecodersManager = VideoDecodersManager.Instance;
		if (videoDecodersManager != null)
			videoDecodersManager.Remove(this);
	}

	void UpdateMaterialsTextures()
	{
		WorkingMaterial.SetTexture("_YTex", yTexture);
		WorkingMaterial.SetTexture("_UTex", uTexture);
		WorkingMaterial.SetTexture("_VTex", vTexture);
		if (HasAlphaChannel)
			WorkingMaterial.SetTexture("_ATex", aTexture);
	}

	void RemoveMaterialsTextures()
	{
		WorkingMaterial.SetTexture("_YTex", null);
		WorkingMaterial.SetTexture("_UTex", null);
		WorkingMaterial.SetTexture("_VTex", null);
		if (HasAlphaChannel)
			WorkingMaterial.SetTexture("_ATex", null);
	}
}
