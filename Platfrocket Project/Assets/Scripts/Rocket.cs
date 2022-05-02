using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Text energyText;
    [SerializeField] private float energyTotal = 2000;
    [SerializeField] private int energyApply = 5;
    [SerializeField] float rotSpeed = 100;
    [SerializeField] float flySpeed = 100;
    [SerializeField] private AudioClip flySound;
    [SerializeField] private AudioClip boomSound;
    [SerializeField] private AudioClip finishSound;
    [SerializeField] private AudioClip energySound;
    [SerializeField] private ParticleSystem flyParticles;
    [SerializeField] private ParticleSystem boomParticles;
    [SerializeField] private ParticleSystem finishParticles;
    
    private bool collisionOff = false;

    private Rigidbody rigidBody;

    private AudioSource audioSource;

    enum State { Playing, Death, NextLevel };

    private State state = State.Playing;
    void Start()
    {
        energyText.text = energyTotal.ToString();
        state = State.Playing;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Playing)
        {
            Launch();
            Rotation();
        }

        if (Debug.isDebugBuild)
        {
            DebugKeys();
        }
    }

    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionOff = !collisionOff;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state!=State.Playing || collisionOff)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                Finish();
            break;
            default:
                Death();
                break; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            PlusEnergy(1000, other.gameObject);
            
        }
    }

    void PlusEnergy(int energyToAdd, GameObject batteryObj)
    {
        GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
        batteryObj.GetComponent<BoxCollider>().enabled = false;
        energyTotal += energyToAdd;
        energyText.text = energyTotal.ToString();
        Destroy(batteryObj);
    }

    void Death()
    {
        state = State.Death;
        audioSource.Stop();
        audioSource.PlayOneShot(boomSound);
        boomParticles.Play();
        flyParticles.Stop();
        Invoke("LoadFirstLevel", 2f);
    }

    void Finish()
    {
        state = State.NextLevel;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        finishParticles.Play();
        flyParticles.Stop();
        Invoke("LoadNextLevel", 2f);
    }

    void LoadNextLevel() //finish
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex + 1;
        
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 1;
        }
        
        SceneManager.LoadScene(nextLevelIndex);
    }

    void LoadFirstLevel() //lose
    {
        SceneManager.LoadScene(1);
    }

    void Launch()
    {
        if (Input.GetKey(KeyCode.Space) && energyTotal > 0)
        {
            energyTotal -= energyApply* Time.deltaTime;
            energyText.text = Mathf.RoundToInt(energyTotal).ToString();
            rigidBody.AddRelativeForce(Vector3.up * flySpeed * Time.deltaTime);
            if(audioSource.isPlaying == false)
                audioSource.PlayOneShot(flySound);
            
            if(!flyParticles.isPlaying)
                flyParticles.Play();
        }
        else
        {
            audioSource.Pause();
            
            if(flyParticles.isPlaying)
            flyParticles.Stop();
        }
        
    }

    void Rotation()
    {
         rigidBody.freezeRotation = true;
        float rotationSpeed = rotSpeed * Time.deltaTime;
        
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                RigidbodyConstraints.FreezePositionZ;
    }
}
