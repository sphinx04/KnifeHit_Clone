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
        Vibration.Init(); //replace later
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Knife>())
        {
            HitKnife();
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
    }

    public void HitKnife()
    {
        Stop();
        Vibration.VibratePeek();
        rb.gravityScale = 5;
        Log.instance.Stop();
    }
}