using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCollectable : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerScript a = other.GetComponent<PlayerScript>();
            a.shield.SetActive(true);
     
            a.shieldPoints = 3;
            GameController.score += 100;

            Destroy(gameObject);
        }

    }
}
