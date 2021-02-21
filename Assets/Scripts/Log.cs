using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Log : MonoBehaviour
{
    public float stickDepth;
    public bool isRolling;
    public static Log instance;
    public float speed;
    public float appleChance;
    public GameObject appleObject;
    public int stickedKnivesAmount;
    public AnimationCurve curve;
    float percent;
    private Rigidbody2D rb;

    //[HideInInspector]
    public List<Knife> knives;
    public List<Apple> apples;

    private List<Transform> pieces;

    private void Awake()
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
        isRolling = true;
        rb = GetComponent<Rigidbody2D>();
        if (isAppleGenerating())
        {
            GenerateApple();
        }
        GenerateKnives();

        InitPieces();

        RotateLog(UnityEngine.Random.Range(0, 180));
    }

    public void InitPieces()
    {
        pieces = new List<Transform>();

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            pieces.Add(transform.GetChild(0).GetChild(i));
        }
    }

    public void Stop()
    {
        isRolling = false;
    }


    void Update()
    {
        percent += Time.deltaTime;
        if (isRolling)
        {
            RotateLog(curve.Evaluate(percent) * speed);
        }
    }
    private bool isAppleGenerating()
    {
        return UnityEngine.Random.Range(0f, 1f) <= appleChance;
    }

    private void GenerateApple()
    {
        GameObject apple = Instantiate(appleObject);
        apple.transform.position = new Vector3(0, transform.localScale.y / 2 + 0.05f, -10);
        apple.transform.parent = transform;

        if (stickedKnivesAmount > 0)
            RotateLog(180 + 180 / stickedKnivesAmount);
    }

    private void GenerateKnives()
    {
        if (stickedKnivesAmount > 0)
        {
            for (int i = 1; i <= stickedKnivesAmount; i++)
            {
                GameObject knife = KnifeSpawner.instance.knifeObject;
                int stepAngle = 360 / stickedKnivesAmount;
                Instantiate(knife).GetComponent<Knife>().HitLog();
                RotateLog(stepAngle);
            }
        }
    }

    public void RotateLog(float angle)
    {
        transform.Rotate(0, 0, angle);
    }

    public void Explode()
    {
        Stop();

        foreach (Knife knife in knives)
        {
            knife.FreeFall();
        }
        foreach (Apple apple in apples)
        {
            apple.FreeFall();
        }
        foreach(Transform piece in pieces)
        {
            piece.GetComponent<MeshCollider>().enabled = true;
            piece.GetComponent<Rigidbody>().AddExplosionForce(100f, new Vector3(), 3f);
            piece.GetComponent<Rigidbody>().AddTorque(new Vector3(UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10)));
            piece.GetComponent<Rigidbody>().useGravity = true;
            piece.parent = null;
        }
    }
}
