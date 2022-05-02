using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public Animator gunAnimator;
    public GameObject armWithgun;
    public Rigidbody2D rigidbody2D;
    private bool isInvincible;
    float invincibleTimer;
    public int health { get { return currentHealth; }}
    public int currentHealth;
    public float timeInvincible = 2.0f;
    public int maxHealth = 5;
    public HealthBar healthBar;
    


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
      horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButton("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            jump = false;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (rigidbody2D.velocity.magnitude <= 0.01f)
            {
                armWithgun.SetActive(true);
                animator.Play("PlayerShoot");
                gunAnimator.Play("PlayerShoot");
                print("Boom");
            }

            else if (rigidbody2D.velocity.magnitude >= 0.01f)
            {
                armWithgun.SetActive(false);
                animator.StopPlayback();
                animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            armWithgun.SetActive(false);
            animator.Play("PlayerIdle");

        }
        if (rigidbody2D.velocity.magnitude >= 0.01f)
        {
            armWithgun.SetActive(false);
            animator.StopPlayback();
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
          
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (currentHealth <= 0)
        {
            PlayerDeath();
        }

        

    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove*Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
    public void ChangeHealth(int amount)
    {
        healthBar.SetHealth(currentHealth);
        if (amount < 0)
        {
            
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
        }
        healthBar.SetHealth(currentHealth);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void PlayerDeath()
    {
        animator.Play("PlayerDead");
        
        Invoke(nameof(RestartLevel), 1);
        Destroy(gameObject, 7);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
