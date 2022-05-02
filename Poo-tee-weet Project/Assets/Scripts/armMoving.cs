using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class armMoving : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float speed;
    private float horizontal;
    private float vertical;
    public float forceOfGravity;
    public float forceOfJump;
    public const float jumpDelay = 0.5f;
    private float jumpTick;


    
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        
        if (Input.GetButtonDown("Jump"))
        {
            if (Time.time - jumpTick > jumpDelay)
            {
               _rigidbody2D.AddForce(transform.up * forceOfJump);
                jumpTick = Time.time;
                print(Input.GetButtonDown("Jump"));
                
            }
        }
        
    }

    private void FixedUpdate()
    {
       _rigidbody2D.AddForce(-transform.up*forceOfGravity);
        Vector2 position =  _rigidbody2D.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        _rigidbody2D.position = position;
    }

}
