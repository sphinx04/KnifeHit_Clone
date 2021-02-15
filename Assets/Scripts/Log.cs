using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Log : MonoBehaviour
{
    public static Log instance;
    public float speed;
    public float stickDepth;
    public bool isRolling;
    private Rigidbody2D rb;

    private void Awake()
    {
        instance = this;
        isRolling = true;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Knife knife = collision.GetComponent<Knife>();
        if (knife.isAimed && isRolling)
        {
            knife.Stick();
            collision.transform.parent = transform;
            collision.transform.position = transform.position + new Vector3(0, stickDepth - transform.localScale.y / 2, 0);
        }
    }

    public void Stop()
    {
        isRolling = false;
    }
    void Update()
    {
        if (isRolling)
        {
            transform.Rotate(0, 0, speed);
        }
    }
}
