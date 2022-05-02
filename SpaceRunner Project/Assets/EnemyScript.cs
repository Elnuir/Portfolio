using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    private Rigidbody enemy;
    public float enemySpeed;
    public GameObject enemyLazer;
    public float shotDelay;
    float nextShotTime = 0;
    public GameObject shotPoint;
    public GameObject enemyDeath;
    public GameObject assClamp;
    public bool isTriggered;
    private GameController controller;
    
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        assClamp = GameObject.Find("assClamp");
        enemy = GetComponent<Rigidbody>();
        enemy.velocity = -transform.forward*enemySpeed;
    }


    void Update()
    {
        if (Time.time > nextShotTime)
        {
            if(!isTriggered)
             Instantiate(enemyLazer, shotPoint.transform.position, transform.rotation);
            else
            {
                var a = Instantiate(enemyLazer, shotPoint.transform.position, transform.rotation);
                a.transform.RotateAround(a.transform.position, a.transform.right, 180);
            }

            nextShotTime = Time.time + shotDelay;
        }

        if (isTriggered)
        {
            enemy.velocity =
                Vector3.ClampMagnitude((assClamp.transform.position - transform.position) *2f,
                    20f); //-transform.forward*enemySpeed;
            //transform.LookAt(assClamp.transform);
            transform.localScale = new Vector3(1, 1, -1);
            var tilt = 2;
            transform.rotation = Quaternion.Euler(tilt* GetComponent<Rigidbody>().velocity.z, transform.rotation.eulerAngles.y, -GetComponent<Rigidbody>().velocity.x*tilt);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyFlip")
        {
           // transform.rotation = Quaternion.Euler(180,0,0);
            isTriggered = true;
            return;
        }
        if (other.tag == "Grinder")
        {
            return;
        }
        if (other.tag == "Shield")
        {
            return;
        }
        if (other.tag == "ShieldPlayer")
        {
           
            PlayerScript a = other.transform.parent.GetComponent<PlayerScript>();
            a.shieldPoints -= 1;
            a.ShieldCheck();
            Instantiate(enemyDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        if (other.tag == "Player")
        {
            controller.RespawnDelay();
        }
        Instantiate(enemyDeath, transform.position, Quaternion.identity);
        GameController.score += 50;
        Destroy(gameObject);
        Destroy(other.gameObject);
        
    }
}
