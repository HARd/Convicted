/*
	Для совместной работы с VideoDecoder.
	Ведет список активных декодеров, считает статистику (для дебажного окна по видео),
	обеспечивает вызов GL.IssuePluginEvent
	Автор: Кущ А.Е.
*/


using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;

public class VideoDecodersManager : MonoBehaviour
{
	private VideoDecodersManager() { }

	static VideoDecodersManager _instance;
	public static VideoDecodersManager Instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<VideoDecodersManager>();

			return _instance;
		}
	}

	public void Awake()
	{
		VideoDecoderPlugin.ResetVideoPluginSettings();

		if (VideoDecoderPlugin.GetLastPluginError () != VideoPluginEror.NoError)
			Debug.LogError ("VideoDecoderPlugin has an error = " + VideoDecoderPlugin.GetLastPluginError ());
	}

	void OnEnable()
	{
		StartCoroutine("DecodeCoroutine");
	}

	void OnDisable()
	{
		StopCoroutine("DecodeCoroutine");
	}

	List<VideoDecoder> videoDecoders = new List<VideoDecoder>();

	public void Add(VideoDecoder videoDecoder)
	{
		videoDecoders.Add(videoDecoder);
	}

	public void Remove(VideoDecoder videoDecoder)
	{
		videoDecoders.Remove(videoDecoder);
	}

	public List<VideoDecoder> GetDecoders()
	{
		return videoDecoders;
	}

	private IEnumerator DecodeCoroutine()
	{
		while (true)
		{
			yield return new WaitForEndOfFrame();
			GL.IssuePluginEvent(VideoDecoderPlugin.GetRenderEventFunc(), 773);
		}
	}

	public ulong CountDecodingPixels()
	{
		ulong totalPlayingArea = 0;
		foreach (VideoDecoder videoDecoder in videoDecoders)
		{
			if (videoDecoder.IsPlaying && videoDecoder.IsPlaying && videoDecoder.isActiveAndEnabled)
				totalPlayingArea += (ulong)(videoDecoder.Width * videoDecoder.Height * (videoDecoder.HasAlphaChannel ? 2 : 1));
		}
		return totalPlayingArea;
	}
}
