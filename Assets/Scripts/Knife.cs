using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public bool isAimed = false;
    public float speed;
    public bool sticked;
    private Rigidbody2D rb;
    public TrailRenderer trail;

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
            {
                if (KnifeSpawner.instance.GetCurrentKnifeAmount() > 0)
                {
                    HitLog();
                    KnifeSpawner.instance.SpawnKnife();
                }
                else
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    Log.instance.Explode();
                }
            }
            if (apple)
            {
                apple.Hit();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAimed)
            rb.velocity = new Vector2(0, speed * 10f);
    }

    public void Throw()
    {
        isAimed = true;
    }

    public void Stop()
    {
        isAimed = false;
        enabled = false;
        trail.enabled = false;
    }
    public void HitLog()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Stop();
        sticked = true;
        Vibration.VibratePop();
        transform.parent = Log.instance.transform;
        transform.position = Log.instance.transform.position + new Vector3(0, Log.instance.stickDepth - Log.instance.transform.localScale.y / 2, 0);
        Log.instance.knives.Add(this);
    }
    public void HitApple()
    {
        //inc num
    }

    public void HitKnife()
    {
        Stop();
        Vibration.VibratePeek();
        rb.gravityScale = 5;
        Log.instance.Stop();
        GetComponentsInChildren<CircleCollider2D>()[0].enabled = false; //это ужасно
        GetComponentsInChildren<CircleCollider2D>()[1].enabled = false;
    }

    public void FreeFall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0.5f;
        rb.AddForce(new Vector2(Random.Range(-200, 200), 150));
        rb.AddTorque(Random.Range(-5f, 5f));
        transform.parent = null;
    }
}