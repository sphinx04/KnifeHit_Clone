using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScreenUIManager : MonoBehaviour
{
    public TextMeshProUGUI appleCounter;
    public TextMeshProUGUI highestLvlCounter;

    // Start is called before the first frame update
    void Start()
    {
        appleCounter.text = GameManager.instance.GetCurrentAppleAmount().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
