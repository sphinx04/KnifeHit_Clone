using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreenUIManager : MonoBehaviour
{
    public TextMeshProUGUI appleCounter;
    public TextMeshProUGUI maxLvlCounter;

    // Start is called before the first frame update
    void Start()
    {
        appleCounter.text = GameManager.instance.GetCurrentAppleAmount().ToString();
        maxLvlCounter.text = GameManager.instance.GetMaxLvlNum().ToString();
    }
}
