using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEmitterRight : MonoBehaviour
{
    public GameObject shield;
    
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
        if (Time.time > nextLaunchTime)
        {

            //пора запускаться
            Vector3 shieldPosition = new Vector3(transform.position.x, 0, Random.Range(-transform.localScale.z/2, transform.localScale.z/2));
          

            GameObject spawned = Instantiate(shield, shieldPosition, Quaternion.identity);
            spawned.GetComponent<Rigidbody>().velocity = new Vector3(-Random.Range(30,50),0,0);
            
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
}
