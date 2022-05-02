using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
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
        number = Random.Range(1, 4);
        if (Time.time > nextLaunchTime)
        {
            //пора запускаться
            Vector3 enemyPosition = new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x /2), 0, transform.position.z);
            switch (number)
            {
                case 1:
                    Instantiate(enemy1, enemyPosition, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(enemy2, enemyPosition, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(enemy3, enemyPosition, Quaternion.identity);
                    break;
            }
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
