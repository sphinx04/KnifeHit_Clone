using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField]
    List<Sprite> sprites;
    public void Hit()
    {
        //transform.parent = null;
        print("apple");
        for(int i = 0; i < sprites.Count; i++)
        {
            GameObject piece = new GameObject();
            piece.transform.position = transform.position;
            piece.transform.rotation = transform.rotation;
            piece.AddComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            //piece.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            piece.GetComponent<Rigidbody2D>().AddForce(new Vector2((1 - i * 2) * 50f, 100f));
            piece.AddComponent<SpriteRenderer>().sprite = sprites[i];
        }
        Destroy(gameObject);
    }
}
