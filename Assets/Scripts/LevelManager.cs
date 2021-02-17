﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

	public Level levelInfo;

	void Start()
	{
		if (instance == null)
		{ 
			instance = this; 
		}
		else if (instance == this)
		{
			Destroy(gameObject);
		}

		InitLog();
	
	}
	public void InitLog()
    {
		Instantiate(levelInfo.logModel);

		Log.instance.speed = levelInfo.logSpeed;
		Log.instance.curve = levelInfo.logRollingCurve;
		Log.instance.appleChance = levelInfo.appleChance;
		Log.instance.stickedKnivesAmount = levelInfo.stickedKnivesAmount;
	}
}



