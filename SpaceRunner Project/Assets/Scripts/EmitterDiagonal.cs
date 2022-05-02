using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterDiagonal : MonoBehaviour
{
    public GameObject enemy1;
    
    public float minDelay, maxDelay;
    private float nextLaunchTime = 3;
    private int number;

    // Update is called once per frame
    void Update()
    {
        if (!GameController.isStarted)
        {
            return;
        }
        number = Random.Range(1, 3);
        if (Time.time > nextLaunchTime)
        {
            switch (number)
            {
                case 1:
                    //пора запускаться
                    Vector3 enemyPosition = new Vector3(transform.localScale.x / 2, 0, transform.position.z);

                    Instantiate(enemy1, enemyPosition, Quaternion.Euler(0, 45, 0));
                    break;
                case 2:
                    Vector3 enemyPosition2 = new Vector3(-transform.localScale.x / 2, 0, transform.position.z);

                    Instantiate(enemy1, enemyPosition2, Quaternion.Euler(0, -45, 0));
                    break;
            }

            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}