using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeIcons : MonoBehaviour
{
    [SerializeField]
    private Sprite knifeTransparent;
    [SerializeField]
    private Sprite knifeFilled;
    [SerializeField]
    private GameObject[] knifeIcons;
    public Color color;

    private void OnEnable() => LevelManager.instance.OnKnifeAmountChange += UpdateIcons;
    private void OnDisable() => LevelManager.instance.OnKnifeAmountChange -= UpdateIcons;

    void Start()
    {
        knifeIcons = new GameObject[LevelManager.instance.startKnifeAmount];

        for (int i = 0; i < LevelManager.instance.startKnifeAmount; i++)
        {
            //решает проблему дублирования
            GameObject p; 
            knifeIcons[i] = Instantiate(p  = new GameObject(), transform);

            knifeIcons[i].AddComponent<Image>().sprite = knifeFilled;
            knifeIcons[i].GetComponent<Image>().color = color;

            Destroy(p);
        }
    }

    public void UpdateIcons()
    {
        for (int i = 0; i < LevelManager.instance.startKnifeAmount - LevelManager.instance.GetCurrentKnifeAmount(); i++)
        {
            knifeIcons[i].GetComponent<Image>().sprite = knifeTransparent;
        }
    }
}
