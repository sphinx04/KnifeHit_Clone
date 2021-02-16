using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public bool isAimed = true;
    public float speed;
    public bool sticked;
    private Rigidbody2D rb;

    private void Awake()
    {
        Vibration.Init();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Knife>())
        {
            HitKnife();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAimed && Log.instance.isRolling)
        {
            Apple apple = collision.GetComponent<Apple>();
            Log log = collision.GetComponent<Log>();
            if (log)
                HitLog();
            if (apple)
                apple.Hit();
        }
    }

    private void FixedUpdate()
    {
        if (isAimed)
            rb.velocity = new Vector2(0, speed * 10f);
    }

    public void Stop()
    {
        isAimed = false;
        GetComponent<BoxCollider2D>().enabled = false;
        enabled = false;
    }
    public void HitLog()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Stop();
        sticked = true;
        Vibration.VibratePop();
        transform.parent = Log.instance.transform;
        transform.position = Log.instance.transform.position + new Vector3(0, Log.instance.stickDepth - Log.instance.transform.localScale.y / 2, 0);
    }
    public void HitApple()
    {

    }

    public void HitKnife()
    {
        Stop();
        Vibration.VibratePeek();
        rb.gravityScale = 5;
        Log.instance.Stop();
    }
}