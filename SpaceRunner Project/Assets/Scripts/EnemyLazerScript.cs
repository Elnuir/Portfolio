using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerScript : MonoBehaviour
{
    public float speed;
    public GameObject playerExplosion;
    private GameController controller;
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        GetComponent<Rigidbody>().velocity = -transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grinder")
        {
            return;
        }
        if (other.tag == "EnemyFlip")
        {
            return;
        }
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
            controller.RespawnDelay();
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
            Destroy(gameObject);
            return;
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
