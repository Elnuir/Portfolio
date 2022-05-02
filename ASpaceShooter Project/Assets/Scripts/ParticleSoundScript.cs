using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSoundScript : MonoBehaviour
{
    private ParticleSystem bullets;
    public AudioSource audioSource;
    public AudioClip soundOfShooting;
    //[SerializeField] private string soundOfShooting;

    private float spawnTick;
    private bool  isEmmittingPrev;
    public float speedOfPlaying;
    void Start()
    {
        spawnTick = Time.time;
        bullets = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (isEmmittingPrev != bullets.isEmitting)
        {
            isEmmittingPrev = bullets.isEmitting;
            spawnTick = Time.time;
        }
        if (bullets.isEmitting &&  Time.time - spawnTick > speedOfPlaying)
        {
            audioSource.PlayOneShot(soundOfShooting);
            spawnTick = Time.time;
        }

    }
}
