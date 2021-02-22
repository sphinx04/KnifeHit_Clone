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
    public ParticleSystem logParticles;
    public ParticleSystem appleParticles;
    public ParticleSystem knifeParticles;

    private void Awake()
    {
        Vibration.Init();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (isAimed)
            rb.velocity = new Vector2(0, speed * 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //KNIFE
        if (collision.transform.GetComponent<Knife>() && isAimed)
        {
            EmitParticles(knifeParticles);
            HitKnife();
            Vibration.VibratePeek();
            StartCoroutine(LevelManager.instance.DisplayLoosePanel());
            //StartCoroutine(LevelManager.instance.LoadNextLvl(0));
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
                //LOG
                if (LevelManager.instance.GetCurrentKnifeAmount() > 0)
                {
                    EmitParticles(logParticles);
                    HitLog();
                    KnifeSpawner.instance.SpawnKnife();
                    Vibration.VibratePop();
                    Log.instance.ShakeLog();
                }
                //WIN
                else
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    isAimed = false;
                    Log.instance.Explode();
                    Vibration.Vibrate(1000);
                    enabled = false;
                    StartCoroutine(LevelManager.instance.LoadNextLvl());
                }
            }
            //APPLE
            if (apple)
            {
                EmitParticles(appleParticles);
                apple.Hit();
            }
        }
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
        transform.parent = Log.instance.transform;
        transform.position = Log.instance.transform.position + new Vector3(0, Log.instance.stickDepth - Log.instance.transform.localScale.y / 2, 0);
        Log.instance.knives.Add(this);
    }

    public void HitKnife()
    {
        Stop();
        rb.gravityScale = 5;
        Log.instance.Stop();
        GetComponentsInChildren<CircleCollider2D>()[0].enabled = false; //это ужасно
        GetComponentsInChildren<CircleCollider2D>()[1].enabled = false;
    }

    public void FreeFall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0.5f;
        rb.AddForce(new Vector2(Random.Range(-200, 200), 100));
        rb.AddTorque(Random.Range(-5f, 5f));
        transform.parent = null;
        Destroy(gameObject, 2f);
    }

    public void EmitParticles(ParticleSystem particles)
    {
        Destroy(Instantiate(particles, transform.position, new Quaternion()), 1);
    }
}