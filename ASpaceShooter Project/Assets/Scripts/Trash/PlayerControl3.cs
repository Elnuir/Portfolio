using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl3 : MonoBehaviour
{
    private Rigidbody ship;
    public float tilt;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         float moveHorizontal = Input.GetAxis("Horizontal"); // -1 ... +1
         float moveVertical = Input.GetAxis("Vertical");
         ship.velocity = new Vector3(moveHorizontal, 0, moveVertical)*speed;
         
        
         ship.rotation =  Quaternion.Slerp(ship.rotation,
             Quaternion.Euler(0, ship.velocity.x*tilt , -ship.velocity.x*tilt), 0.2f);
                
    }
}
