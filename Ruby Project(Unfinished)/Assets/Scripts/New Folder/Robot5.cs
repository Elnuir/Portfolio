using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Robot5 : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public bool vertical;
    private int direction = 1;
    public float speed;
    public float timeMoving;
    private float movingTimer;
    private Animator animator;
    private bool broken = true;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        movingTimer = timeMoving;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        movingTimer -= Time.deltaTime;
        if (movingTimer < 0)
        {
            direction = -direction;
            movingTimer = timeMoving;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        if (!broken)
        {
            return;
        }
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + speed * direction * Time.deltaTime;
        }
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + speed * direction * Time.deltaTime;
        }
        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        controller5 controller = other.gameObject.GetComponent<controller5>();
        controller.ChangeHealth(-1);
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
