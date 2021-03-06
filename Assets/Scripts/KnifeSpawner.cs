﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public static KnifeSpawner instance = null;

    public GameObject knifeObject;
    private Knife knife;
    private bool isReady;
    public float throwDelay = 0.1f;

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
        StartCoroutine(ThrowReload(throwDelay));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Log.instance.isRolling && isReady)
        {
            ThrowKnife();
        }
    }

    public void ThrowKnife()
    {
        if (LevelManager.instance.GetCurrentKnifeAmount() > 0)
        {
            isReady = false;
            knife = Instantiate(knifeObject, transform).GetComponent<Knife>();
            knife.isAimed = true;
            transform.GetChild(0).gameObject.SetActive(false);
            LevelManager.instance.SetCurrentKnifeAmount(LevelManager.instance.GetCurrentKnifeAmount() - 1);
            StartCoroutine(ThrowReload(throwDelay));

        }
    }

    public void SpawnKnife()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    IEnumerator ThrowReload(float t)
    {
        yield return new WaitForSeconds(t);
        isReady = true;
    }
}
