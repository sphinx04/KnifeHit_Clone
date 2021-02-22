using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyRestart : MonoBehaviour
{

public void Restart(int price)
    {
        int currentAppleAmount = LevelManager.instance.GetCurrentAppleAmount();
        if (currentAppleAmount >= price)
        {
            LevelManager.instance.SetCurrentAppleAmount(currentAppleAmount - price);
            LevelManager.instance.ReloadScene();
        }
    }
}
