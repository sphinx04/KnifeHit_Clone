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
    private Rigidbody2D rb;
    public AnimationCurve curve;

    private void Awake()
    {
        instance = this;
        isRolling = true;
        rb = GetComponent<Rigidbody2D>();
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
            transform.Rotate(0, 0, curve.Evaluate(percent) * speed);
        }
    }
}
