using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance = null;

	public Level levelInfo;
	public GameObject UIManager;

	private int currentKnifeAmount;

	public int startKnifeAmount;

    public int GetCurrentAppleAmount()
    {
		return PlayerPrefs.GetInt("apples");
	}

    public void SetCurrentAppleAmount(int value)
    {
		PlayerPrefs.SetInt("apples", value);
		OnAppleAmountChange.Invoke();
	}

    public int GetCurrentKnifeAmount()
	{
		return currentKnifeAmount;
	}

	public void SetCurrentKnifeAmount(int value)
	{
		currentKnifeAmount = value;
		OnKnifeAmountChange.Invoke();
	}

	public event Action OnKnifeAmountChange;
	public event Action OnAppleAmountChange;

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
		InitKnifeAmount();
		InitAppleAmount();
	}

	public void InitLog()
	{
		Instantiate(levelInfo.logModel, Log.instance.transform);
		Log.instance.InitPieces();

		Log.instance.speed = levelInfo.logSpeed;
		Log.instance.curve = levelInfo.logRollingCurve;
		Log.instance.appleChance = levelInfo.appleChance;
		Log.instance.stickedKnivesAmount = levelInfo.stickedKnivesAmount;

	}

	public void InitKnifeAmount()
	{
		startKnifeAmount = levelInfo.startKnifeAmount;
		SetCurrentKnifeAmount(startKnifeAmount);
	}

	public void InitAppleAmount()
	{
		OnAppleAmountChange.Invoke();
	}

	public void InitUI()
	{
		UIManager.SetActive(true);
		print("INIT UI");
	}

	public void IncApple()
    {
		SetCurrentAppleAmount(GetCurrentAppleAmount() + 1);
	}
}



