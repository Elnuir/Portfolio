using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

public class Enemy1 : MonoBehaviour
{
    private Vector3 distanceToPlayer;
    public GameObject player;
    private Rigidbody enemy;
    [SerializeField] private float distanceStop;
    [SerializeField] private float enemySpeed;
    [SerializeField] private ParticleSystem getDamageEffect;
    [SerializeField] private GameObject explosionDeathEffect;
     public int enemyHealth;
    [SerializeField] private float chaseDistance;
    [SerializeField] private bool isPatroling;
    [SerializeField] private ParticleSystem[] enemyBullets;
    public AudioManager audioManager;
    public Score score;
    [SerializeField] private int howMuchPoints;
    [SerializeField] bool isAttack;
    private Vector3 destinationPoint;
    private Vector3 goToDirection;
    private float distanceToTheDestinaton;
    private PlayerControl1 control;
    [SerializeField] bool isBigEnemy;

    void Start()
    {
        enemy = GetComponent<Rigidbody>();
        control = player.GetComponent<PlayerControl1>();
    }

    void Update()
    {
        
        AttackPlayer();
        distanceToPlayer = player.transform.position - enemy.transform.position;
        if (distanceToPlayer.magnitude <= chaseDistance)
        {
            isAttack = true;
            isPatroling = false;
            ChasePlayer();
        }
        else
        {
            isAttack = false;
            isPatroling = true;
            Patroling();
        }
        
        if (control.currentHealth <= 0)
        {
            isAttack = false;
        }
    }
    


void ChasePlayer()
    {
        transform.LookAt(player.transform);
        if (distanceToPlayer.magnitude < distanceStop)
        {
            enemy.velocity = (player.transform.position - enemy.transform.position) * 0f;
        }
        else if (distanceToPlayer.magnitude >= distanceStop)
        {
            enemy.velocity = (player.transform.position - enemy.transform.position) * enemySpeed;
        }

    }

    void AttackPlayer()
    {
        
        if (isAttack)
        {
            foreach (var enemyBullet in enemyBullets)
            {  
                if (!enemyBullet.isPlaying)
                enemyBullet.Play();
            }
            
        }
        else if (!isAttack)
        {
            foreach (var enemyBullet in enemyBullets)
            {
                if (enemyBullet.isPlaying)
                enemyBullet.Stop();
            }
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        enemyHealth--;
        getDamageEffect.Play();
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        score.ScoreHit(howMuchPoints);
        audioManager.Play("PlayerDeath");
        explosionDeathEffect.SetActive(true);
        foreach (var enemyBullet in enemyBullets)
        {
            enemyBullet.Stop();
        }
        if (isBigEnemy == false)
            GetComponent<MeshRenderer>().enabled = false;
        if (isBigEnemy)
            GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Destroy(gameObject, 2f);
        control.currentHealth += 5;
        control.healthBar.SetHealth(control.currentHealth += 5);
    }

    void Patroling()
    {
        if (distanceToTheDestinaton < 5)
            isPatroling = false;
        distanceToTheDestinaton = Vector3.Distance(transform.position, destinationPoint);
        if (isPatroling)
            return;
        isPatroling = true;
        destinationPoint = new Vector3(transform.localPosition.x + Random.Range(-20,20), transform.localPosition.y, transform.localPosition.z + Random.Range(-20,20));
        goToDirection = destinationPoint - transform.position;
        enemy.velocity = goToDirection*0.2f;
        transform.LookAt(destinationPoint);

    }
    
    

}
