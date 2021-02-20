using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnifeCount : MonoBehaviour
{
    [SerializeField]
    private KnifeSpawner spawner;
    [SerializeField]
    private TextMeshProUGUI text;

    private void OnEnable() => KnifeSpawner.instance.OnKnifeAmountChange += UpdateValue;
    private void OnDisable() => spawner.OnKnifeAmountChange -= UpdateValue;

    public void UpdateValue() => text.SetText(spawner.GetCurrentKnifeAmount().ToString());
}
