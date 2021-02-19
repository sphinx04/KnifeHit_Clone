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
        GameObject apple = Instantiate(appleObject);
        apple.transform.position = new Vector3(0, transform.localScale.y / 2 + 0.05f, -10);
        apple.transform.parent = transform;
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

    public void Explode()
    {
        Transform logModel = transform.GetChild(0);
        for (int i = 0; i < logModel.childCount; i++)
        {
            logModel.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(100f, new Vector3(), 3f);
            logModel.GetChild(i).GetComponent<MeshCollider>().enabled = true;
            logModel.GetChild(i).GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)));
            logModel.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            logModel.GetChild(i).parent = null;

            foreach (Knife knife in knives)
            {
                knife.FreeFall();
            }
            foreach (Apple apple in apples)
            {
                apple.FreeFall();
            }
        }
    }
}
