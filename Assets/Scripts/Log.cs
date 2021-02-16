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
    [SerializeField]
    private AnimationCurve curve;

    private void Awake()
    {
        instance = this;
        isRolling = true;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform obj = collision.transform.parent;
        Knife knife = obj.GetComponent<Knife>();
        if (knife.isAimed && isRolling)
        {
            knife.HitLog();
            obj.parent = transform;
            obj.position = transform.position + new Vector3(0, stickDepth - transform.localScale.y / 2, 0);
        }
    }

    public void Stop()
    {
        isRolling = false;
    }

    float percent = 0f;

    void Update()
    {
        percent += Time.deltaTime;
        print(percent);
        if (isRolling)
        {
            transform.Rotate(0, 0, curve.Evaluate(percent) * speed);
        }
    }
}
