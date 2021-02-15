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
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(6565);

        Stop();
        rb.gravityScale = 5;
        Log.instance.Stop();
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
    public void Stick()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Stop();
        sticked = true;
    }
}