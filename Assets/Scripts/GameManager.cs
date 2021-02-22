using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int ApplesAmount { get; set; }


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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //PlayerPrefs.SetInt("maxLvl", 0);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelScene");
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public int GetCurrentAppleAmount()
    {
        return PlayerPrefs.GetInt("apples");
    }

    public int GetMaxLvlNum()
    {
        return PlayerPrefs.GetInt("maxLvl");
    }
}
