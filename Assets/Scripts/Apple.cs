using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;
    private Rigidbody2D rb;

    private void Start()
    {
        Log.instance.apples.Add(this);
        rb = GetComponent<Rigidbody2D>();
    }
    public void Hit()
    {
        print("apple");
        for(int i = 0; i < sprites.Count; i++)
        {
            GameObject piece = new GameObject();
            piece.transform.position = transform.position;
            piece.transform.rotation = transform.rotation;
            piece.AddComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            piece.GetComponent<Rigidbody2D>().AddForce(new Vector2((1 - i * 2) * 50f, 100f));
            piece.AddComponent<SpriteRenderer>().sprite = sprites[i];
        }
        Destroy(gameObject);

        Log.instance.apples.Remove(this);
    }
    public void FreeFall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.gravityScale = 1;
        rb.AddForce(new Vector2(Random.Range(-30, 30), 50));
        rb.AddTorque(Random.Range(-5f, 5f));
        transform.parent = null;
    }
}
