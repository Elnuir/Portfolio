using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EmitterScript : MonoBehaviour
{
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    private int number; 
    

    public float minDelay, maxDelay; //задержка между запусками

    private float nextLaunchTime = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (!GameController.isStarted)
        {
            return;
        }
         number = Random.Range(1, 4);
        if (Time.time > nextLaunchTime)
        {
            
            //Пора запускаться
            Vector3 asteroidPosition = new Vector3(
                Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), 0, transform.position.z);


            switch (number)
            {
                case 1:
                    Instantiate(asteroid1, asteroidPosition, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(asteroid2, asteroidPosition, Quaternion.identity);
                    break;
                    case 3:
                    Instantiate(asteroid3, asteroidPosition, Quaternion.identity);
                    break;
            }
            
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }

    }
}
