using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnifeCount : MonoBehaviour
{
    public GameObject knifeAmountIcons;
    [SerializeField]
    private TextMeshProUGUI knifeAmountText;

    private void Start()
    {
        knifeAmountIcons.SetActive(true);
    }

    private void OnEnable() => LevelManager.instance.OnKnifeAmountChange += UpdateKnifeAmountText;

    private void OnDisable() => LevelManager.instance.OnKnifeAmountChange -= UpdateKnifeAmountText;

    public void UpdateKnifeAmountText() => knifeAmountText.SetText(LevelManager.instance.GetCurrentKnifeAmount().ToString());
}
