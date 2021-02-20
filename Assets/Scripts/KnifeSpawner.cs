using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public static KnifeSpawner instance = null;

    public GameObject knifeObject;
    private int currentKnifeAmount;
    private Knife knife;

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
        SpawnKnife();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Log.instance.isRolling)
        {
            ThrowKnife();
        }
        if (Input.GetMouseButtonDown(1))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        

    }
    public void ThrowKnife()
    {
        if (GetCurrentKnifeAmount() > 0)
        {
            knife = Instantiate(knifeObject, transform).GetComponent<Knife>();
            knife.isAimed = true;
            transform.GetChild(0).gameObject.SetActive(false);
            SetCurrentKnifeAmount(GetCurrentKnifeAmount() - 1);
        }
    }

    public void SpawnKnife()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
