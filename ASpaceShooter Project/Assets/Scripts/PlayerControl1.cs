using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Timers;
using System.Xml.Serialization;
using Cinemachine;
using Unity.Mathematics;
using UnityEditor.Experimental;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Random = UnityEngine.Random;

public class PlayerControl1 : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    
    
    [SerializeField] float rotationSpeed = 10;
    private float yRotationFactor = 10;
    private float xMove, yMove;
    public AudioManager audiomanager;
    [SerializeField] private ParticleSystem turbo;
    [SerializeField] CinemachineVirtualCamera vcam;
    
    
    
    [SerializeField] private float minspeed;
    [SerializeField] private float maxspeed;

    public int currentHealth;
    [SerializeField] private int maxHealth;
    public HealthBar healthBar;
    

    [SerializeField] private ParticleSystem[] guns;
    private bool isControlEnabled = true;
    private float timer;
    public bool isRadiated;
    [SerializeField] GameObject DeathEffetcs;
    [SerializeField] GameObject EngineEffetcs;
    [SerializeField] private ParticleSystem getDamageEffect;


    [SerializeField] private GameObject defeatedCanvas;
    [SerializeField] private GameObject winScreen;







    private void Start()
        {
            currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        healthBar.SetMaxHealth(maxHealth);
       // scoreCount = GetComponent<Score>();
        }

    private void FixedUpdate()
    {
        if (isControlEnabled)
        {
            Move();
            Rotate();
            FireGuns();
            Turbo();
            OutOfWorldBoarders();
            AutoAim();
            OnPlayerDeath();
        }

    }

    void AutoAim()
{
float distanceToClosestEnemy = Mathf.Infinity;
GameObject closestEnemy = null;
GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

foreach (GameObject currentEnemy in allEnemies)
{
    float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
    if (distanceToEnemy < distanceToClosestEnemy)
    {
        distanceToClosestEnemy = distanceToEnemy;
        closestEnemy = currentEnemy;
    }
}

//        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
// print(closestEnemy);
if (CrossPlatformInputManager.GetButton("Fire2"))
{
    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(closestEnemy.transform.position - this.transform.position), Time.time * 0.3f);
    audiomanager.Play("TargetLocked");
}
}


void Move()
{
if (Input.GetKey(KeyCode.Space))
{
    speed++;
    speed = Mathf.Clamp(speed, minspeed, maxspeed);
    




}
else
{
    speed--;
    speed = Mathf.Clamp(speed, minspeed, maxspeed);
    
    
}
xMove = Input.GetAxis("Horizontal");
yMove = Input.GetAxis("Vertical");
rb.velocity = transform.forward*speed;
if (CrossPlatformInputManager.GetButton("Fire2") && rb.constraints == RigidbodyConstraints.None)
{
    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    audiomanager.PlayOneShot("Stabilized");
}

}

void Rotate()
{
var xMouse = Input.GetAxis("Mouse X");
var yMouse = Input.GetAxis("Mouse Y");
transform.Rotate(-yMouse*rotationSpeed*Time.deltaTime, xMouse*rotationSpeed*Time.deltaTime, -xMove*rotationSpeed*Time.deltaTime, Space.Self);
if (Input.GetKey(KeyCode.W))
{
  
    transform.Rotate(rotationSpeed*Time.deltaTime, 0, 0, Space.Self);
}
if (Input.GetKey(KeyCode.S))
{
  
    transform.Rotate(-rotationSpeed*Time.deltaTime, 0, 0, Space.Self);
}
// print("xMouse" + xMouse);
// print("xMove" + xMove);


}


void FireGuns()
{

   if (CrossPlatformInputManager.GetButtonDown("Fire1"))
   {

       foreach (ParticleSystem gun in guns)
       {
           gun.Play();
       }

   }
   else if (CrossPlatformInputManager.GetButtonUp("Fire1"))
   {

       foreach (ParticleSystem gun in guns)
       {
           gun.Stop();
       }

   
}
}

void Turbo()
{
if (Input.GetKeyDown(KeyCode.Space))
{
   turbo.Play();
   CameraShakenessStart();
   audiomanager.Play("EngineStarts");
   audiomanager.Play("EngineRunning");
}

if (Input.GetKeyUp(KeyCode.Space))
{
   turbo.Stop();
   audiomanager.Stop("EngineRunning");
}
}

void CameraShakenessStart()
{
vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 5;
Invoke(nameof(CameraShakenessStop), 1f);
}
void CameraShakenessStop()
{
vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
}

private void OnParticleCollision(GameObject other)
{
currentHealth--;
healthBar.SetHealth(currentHealth);
audiomanager.PlayOneShot("ShieldSound");
getDamageEffect.Play();
}

public void OutOfWorldBoarders()
{
if (Time.time - timer > 2 && isRadiated)
{
   currentHealth -= 15;
   timer = Time.time;
}
healthBar.SetHealth(currentHealth);
}
void OnPlayerDeath()
{
if (currentHealth <= 0)
{
   foreach (ParticleSystem gun in guns)
   {
       gun.Stop();
   }
   print("ControlOff");
   audiomanager.PlayOneShot("PlayerDeath");
   isControlEnabled = false;
   rb.constraints = RigidbodyConstraints.None;
   var giveForce = new Vector3(Random.Range(15,50),Random.Range(15,50),Random.Range(15,50));
   rb.AddForceAtPosition(new Vector3(50, 50,50), new Vector3(rb.position.x + Random.Range(1,50), rb.position.y + Random.Range(1,50), rb.position.z + Random.Range(1,50)));
   DeathEffetcs.SetActive(true);
   EngineEffetcs.SetActive(false);
   defeatedCanvas.SetActive(true);
   audiomanager.Stop("Theme");
   audiomanager.Play("PlayerDeathMusic");
}
}

private void OnCollisionEnter(Collision other)
{
if (other.gameObject.tag == "Ground")
{
   audiomanager.PlayOneShot("ShieldSound");
   rb.constraints = RigidbodyConstraints.None;
   //rb.AddForce(transform.up*20000);
   transform.Rotate(0, 180, 0);
   print("ForceWaAdded");
   getDamageEffect.Play();
}

}

void WinScreen()
{
print("win");
}




}



