using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance = null;

	public Level levelInfo;
	public GameObject UIManager;


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance == this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		InitUI();
		InitLog();
		InitKnifeSpawner();
	}

	public void InitLog()
	{
		Instantiate(levelInfo.logModel, Log.instance.transform);

		Log.instance.speed = levelInfo.logSpeed;
		Log.instance.curve = levelInfo.logRollingCurve;
		Log.instance.appleChance = levelInfo.appleChance;
		Log.instance.stickedKnivesAmount = levelInfo.stickedKnivesAmount;
	}

	public void InitKnifeSpawner()
	{
		KnifeSpawner.instance.SetCurrentKnifeAmount(levelInfo.startKnifeAmount);
	}

	public void InitUI()
	{
		UIManager.SetActive(true);
		print("INIT UI");
	}
}



