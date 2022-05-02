using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalLazerScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetButton("Fire2"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, speed);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
