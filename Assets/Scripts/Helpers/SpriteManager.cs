﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager 
{
	Dictionary<string, Sprite> spritePool;   

	private static SpriteManager spriteManager;

	public static SpriteManager Instatce
    {
        get
        {
			if (spriteManager == null) 
            {
				spriteManager = new SpriteManager();
            }

			return spriteManager;
        }
    }

	private SpriteManager()
    {
		spritePool = new Dictionary<string, Sprite>();
    }

	public Sprite GetSprite(string assetName)
    {
		Sprite sprite;

		if(spritePool.ContainsKey(assetName))
			sprite = spritePool[assetName];
        else
        {
			sprite = Resources.Load<Sprite>(assetName);

			if (sprite != null)
				spritePool.Add(assetName, sprite);
			else
				Debug.LogErrorFormat("Load {0} Error", assetName);
        }

		return sprite;
    }

	public void Add(string name,Sprite sprite)
    {
		spritePool.Add (name, sprite);
    }
}
 
