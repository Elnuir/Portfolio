using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTEST : MonoBehaviour
{
    private Rigidbody rb;
    private bool isPatroling = false;
    private Vector3 destinationPoint;
    private Vector3 goToDirection;
    private float distanceToTheDestinaton;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        print(Vector3.Distance(transform.position, destinationPoint));

    }

    void Patrol()
    {
        if (distanceToTheDestinaton < 5)
            isPatroling = false;
        distanceToTheDestinaton = Vector3.Distance(transform.position, destinationPoint);
        if (isPatroling)
            return;
        isPatroling = true;
        destinationPoint = new Vector3(transform.localPosition.x + Random.Range(-20,20), transform.localPosition.y, transform.localPosition.z + Random.Range(-20,20));
        goToDirection = destinationPoint - transform.position;
        rb.velocity = goToDirection*0.2f;
        transform.LookAt(destinationPoint);
    }

}