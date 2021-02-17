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
    private Rigidbody2D rb;
    public AnimationCurve curve;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        isRolling = true;
        rb = GetComponent<Rigidbody2D>();
        if (isAppleGenerating())
        {
            GenerateApple();
        }
        print(stickedKnivesAmount);
        GenerateKnives();

        RotateLog(Random.Range(0, 180));
    }

    public void Stop()
    {
        isRolling = false;
    }

    float percent = 0f;

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
        return Random.Range(0f, 1f) <= appleChance;
    }

    private void GenerateApple()
    {
        Instantiate(appleObject, transform).transform.position = new Vector3(0, transform.localScale.y / 2 + 0.05f, -10);
        RotateLog(180 + 180 / stickedKnivesAmount);
    }

    private void GenerateKnives()
    {
        if (stickedKnivesAmount > 0)
        {
            for (int i = 1; i <= stickedKnivesAmount; i++)
            {
                print("init");
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
}
