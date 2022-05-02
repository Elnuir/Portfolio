using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject lazer;
    public GameObject lazerGun;
    public GameObject lazerGunAdditional1;
    public GameObject lazerGunAdditional2;
    public GameObject shield;
    public int shieldPoints = 0;
    public float shotDelay;
    public float shotDelayAdditional; //задержка
    private float nextShotTime = 0;
    public float tilt;
    public float speed;
    public float xMin, xMax, zMin, zMax;
    
    private Rigidbody ship;

    private AudioSource _audioSource;

    public AudioClip shieldsound;
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody>();
        // ship.velocity = new Vector3(20,0,20);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // -1 ... +1
        float moveVertical = Input.GetAxis("Vertical");
        ship.velocity = new Vector3(moveHorizontal, 0, moveVertical)*speed;

        float positionX = Mathf.Clamp(ship.position.x, xMin, xMax);
        float positionZ = Mathf.Clamp(ship.position.z, zMin, zMax);
        ship.position = new Vector3(positionX, 0, positionZ);
        
        ship.rotation = Quaternion.Euler(tilt* ship.velocity.z, 0, -ship.velocity.x*tilt);
        if (Time.time > nextShotTime && Input.GetButton("Fire1"))
        {
            Instantiate(lazer, lazerGun.transform.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }
        if (Time.time > nextShotTime && Input.GetButton("Fire2"))
        {
            Instantiate(lazer, lazerGunAdditional1.transform.position, Quaternion.Euler(0,45,0));
            Instantiate(lazer, lazerGunAdditional2.transform.position, Quaternion.Euler(0,-45,0));
            nextShotTime = Time.time + shotDelayAdditional;
            
        }
    }

    public void ShieldCheck()
    {
        
        if (shieldPoints == 0)
        {
            shield.SetActive(false);
        }
        
        Debug.Log(shieldPoints);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            _audioSource.PlayOneShot(shieldsound);
        }
    }
    
}
