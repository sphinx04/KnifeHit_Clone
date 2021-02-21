using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AppleCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI appleAmountText;

    private void OnEnable() => LevelManager.instance.OnAppleAmountChange += UpdateAppleAmountText;

    private void OnDisable() => LevelManager.instance.OnAppleAmountChange -= UpdateAppleAmountText;

    public void UpdateAppleAmountText() => appleAmountText.SetText(LevelManager.instance.GetCurrentAppleAmount().ToString());
}
