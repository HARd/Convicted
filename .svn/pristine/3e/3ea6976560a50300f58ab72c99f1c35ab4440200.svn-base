/*
	Статический класс-обертка для доступа к функциям бинарного плагина.
	Смотри VideoDecoder
	Автор: Кущ А.Е.
*/


using System;
using System.Runtime.InteropServices;

public enum VideoPluginEror
{
	NoError 					= 0,
	CreateGraphicsDeviceFailed 	= 1,
	OpenVideoFailed 			= 2,
	WrongVideoWidth 			= 3,
	WrongVideoHeight 			= 4,
	DecodeVideoFrameFailed 		= 5,
	InvalidVideoPath			= 6
};

public static class VideoDecoderPlugin
{
	#if UNITY_IPHONE && !UNITY_EDITOR
		private const string PLATFORM_DLL = "__Internal";
	#else
		private const string PLATFORM_DLL = "VideoDecoder";
	#endif

	[DllImport(PLATFORM_DLL)]
	public static extern IntPtr GetRenderEventFunc();

	[DllImport(PLATFORM_DLL)]
	public static extern int MyGetDeviceType();

	[DllImport(PLATFORM_DLL)]
	public static extern void ResetVideoPluginSettings();

	[DllImport(PLATFORM_DLL)]
	public static extern IntPtr CreateVideoContext(string path, bool isAlphaChannel, long offset, long lenght);

	[DllImport(PLATFORM_DLL)]
	public static extern void DestroyVideoContext(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern bool DecodeFrame(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern bool DecodeFrameAtTime(IntPtr context, float time);

	[DllImport(PLATFORM_DLL)]
	public static extern bool DecodeFirstFrame(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern bool DecodeLastFrame(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern int GetVideoFrameWidth(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern int GetVideoFrameHeight(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern IntPtr GetYTexture(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern IntPtr GetUTexture(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern IntPtr GetVTexture(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern IntPtr GetATexture(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern void SetYTexture(IntPtr context, IntPtr texture);

	[DllImport(PLATFORM_DLL)]
	public static extern void SetUTexture(IntPtr context, IntPtr texture);

	[DllImport(PLATFORM_DLL)]
	public static extern void SetVTexture(IntPtr context, IntPtr texture);

	[DllImport(PLATFORM_DLL)]
	public static extern void SetATexture(IntPtr context, IntPtr texture);

	[DllImport(PLATFORM_DLL)]
	public static extern float GetTimePosition(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern float GetVideoDuration(IntPtr context);

	[DllImport(PLATFORM_DLL)]
	public static extern VideoPluginEror GetLastPluginError();

	[DllImport(PLATFORM_DLL)]
	public static extern void ClearPluginError();
}
