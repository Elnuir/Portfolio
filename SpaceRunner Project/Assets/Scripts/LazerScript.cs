using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public float speed;
    

    
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetButton("Fire1"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
        }
        else if (Input.GetButton("Fire2"))
        {
            transform.localScale /= 2;
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
