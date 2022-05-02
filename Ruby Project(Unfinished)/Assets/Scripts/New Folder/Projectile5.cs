using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile5 : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2D.AddForce(direction*force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Robot5 e = other.gameObject.GetComponent<Robot5>();
        if (e != null)
        {
            e.Fix();
        }
        Destroy(gameObject);
    }
}
