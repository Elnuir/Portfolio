using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    Rigidbody2D _rigidbody2D;
    public float chaseRadius;
    public GameObject player;
    private bool m_FacingRight;
    Animator animator;
    public int enemyHealth;
    private bool isAlive = true;
    public GameObject NormalCamera;
    public GameObject BossCamera;
    public GameObject BlockExitCollider;
    public GameObject BossConfiner;
    public GameObject WinCanvas;
    public GameObject GoBackCanvas;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((player.transform.position - transform.position).magnitude) < chaseRadius)
        {
            _rigidbody2D.velocity = (player.transform.position - transform.position) * speed * Time.deltaTime;
            animator.SetFloat("Speed", 1);
        }
        else if (Mathf.Abs((player.transform.position - transform.position).magnitude) > chaseRadius)
        {
            _rigidbody2D.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
        }
        if (_rigidbody2D.velocity.x < 0 == m_FacingRight)
            Flip();
    }   
    private void Flip()
    {
		
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
            animator.SetTrigger("Attack");
        }
        if (other.gameObject.tag == "Bullet")
        {
            if (enemyHealth <= 0)
            {
                isAlive = false;
                animator.Play("BossDeath");
                _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                NormalCamera.SetActive(true);
                BossCamera.SetActive(false);
                BlockExitCollider.SetActive(false);
                BossConfiner.SetActive(false);
                var a = FindObjectOfType<ReadableForeignBook>();
                if (a == null)
                {
                    WinCanvas.SetActive(true);
                }
                else if (a != null)
                {
                    GoBackCanvas.SetActive(true);
                }
                Destroy(gameObject, 2);
            }

            if (isAlive)
            {
                enemyHealth -= 1;
                animator.SetTrigger("Hurt");
            }
        }
    }

}
