using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance = null;

	public GameObject UIManager;

	public int startKnifeAmount;

	public GameObject winPanel;
	public GameObject loosePanel;
	public TMPro.TextMeshProUGUI levelLabel;

	public event Action OnKnifeAmountChange;
	public event Action OnAppleAmountChange;

	public List<Level> levels;

	private int currentKnifeAmount;
	private static int currentLevelNum = 0;


	private void Awake()
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
		startKnifeAmount = levels[currentLevelNum].startKnifeAmount;
		InitUI();
		InitLog();
		InitKnifeAmount();
		InitAppleAmount();

		levelLabel.text += currentLevelNum + 1;
	}
	public void InitLog()
	{
		Instantiate(levels[currentLevelNum].logModel, Log.instance.transform);
		Log.instance.InitPieces();

		Log.instance.speed = levels[currentLevelNum].logSpeed;
		Log.instance.curve = levels[currentLevelNum].logRollingCurve;
		Log.instance.appleChance = levels[currentLevelNum].appleChance;
		Log.instance.stickedKnivesAmount = levels[currentLevelNum].stickedKnivesAmount;

	}

	public void InitKnifeAmount() => SetCurrentKnifeAmount(startKnifeAmount);

	public void InitAppleAmount() => OnAppleAmountChange.Invoke();

	public void InitUI() => UIManager.SetActive(true);

	public void IncApple() => SetCurrentAppleAmount(GetCurrentAppleAmount() + 1);

	public void ReloadScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

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

	public void LoadNext()
	{
		if (currentLevelNum < levels.Count - 1)
		{
			currentLevelNum++;
			ReloadScene(); // replace with dialog window
		}
		else
		{
			currentLevelNum = 0;
			DisplayWinPanel();
		}
	}

	public void LoadLevel(int level)
    {
		if (currentLevelNum < levels.Count)
		{
			currentLevelNum = level;
			ReloadScene();
		}
	}

	public void DisplayWinPanel()
	{
		winPanel.SetActive(true);
	}

	public IEnumerator DisplayLoosePanel()
	{
		yield return new WaitForSeconds(1f);
		currentLevelNum = 0;
		loosePanel.SetActive(true);
	}


	public IEnumerator LoadNextLvl()
	{
		yield return new WaitForSeconds(1);
        instance.LoadNext();
	}

	public IEnumerator LoadNextLvl(int lvl)
	{
		yield return new WaitForSeconds(1);
		instance.LoadLevel(lvl);
	}

	public void ToStartScreen()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
	}

}



