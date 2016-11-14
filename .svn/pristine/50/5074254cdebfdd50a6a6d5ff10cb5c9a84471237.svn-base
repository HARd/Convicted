/*
	Инкапсулирует функции динамической загрузки ресурсов разніз типов.
	из Resources и StreamingAssets
	Автор: Кущ А.Е.
*/


using UnityEngine;
using System.Collections;

using System;
using System.IO;

public static class ResourcesLoader
{
	public static string LoadFileText(string name)
	{
		return File.ReadAllText(name, System.Text.Encoding.UTF8);
	}

	public static string LoadResourceText(string name)
	{
		TextAsset textAsset = Resources.Load(name) as TextAsset;
		return textAsset.text;
	}

	public static string LoadStreamingText(string name)
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, name);

		if (filePath.Contains("://"))
		{
			WWW www = new WWW(filePath);
			while (!www.isDone) { Debug.Log(www.progress); }
			return www.text;
		}
		else
			return File.ReadAllText(filePath);
	}

	public static Texture2D LoadStreamingTexture(string name)
	{
		string filePath = "file://" + Path.Combine(Application.streamingAssetsPath, name);

		WWW www = new WWW(filePath);
		while (!www.isDone) ;
		return www.texture;
	}

	public static byte[] LoadStreamingBinaryFile(string name)
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, name);

		if (filePath.Contains("://"))
		{
			WWW www = new WWW(filePath);
			while (!www.isDone) ;
			return www.bytes;
		}
		else
			return File.ReadAllBytes(filePath);
	}

	public static Sprite LoadStreamingSprite(string name)
	{
		Texture2D texture = LoadStreamingTexture(name);
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

	}

	public static bool HasStreamingText(string name)
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, name);

		if (filePath.Contains("://"))
		{
			WWW www = new WWW(filePath);
			while (!www.isDone) { Debug.Log(www.progress); }
			return string.IsNullOrEmpty(www.error);
		}
		else
			return File.Exists(filePath);
	}

}
