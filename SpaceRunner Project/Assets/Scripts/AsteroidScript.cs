using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;
    public float speed;
    public float rotationSpeed;
    private Rigidbody asteroid;
    private GameController controller;
    
    private float mult;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<GameController>();
        mult = Random.Range(1f, 3f);
        asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere*rotationSpeed;
        asteroid.velocity = new Vector3(0,0,-speed * mult);
        asteroid.transform.localScale /= mult;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            return;
        }

        if (other.tag == "Grinder")
        {
            return;
        }

        if (other.tag == "Shield")
        {
            return;
        }

        if (other.tag == "EnemyFlip")
        {
            return;
        }
        if (other.tag == "ShieldPlayer")
        {
           
            PlayerScript a = other.transform.parent.GetComponent<PlayerScript>();
            a.shieldPoints -= 1;
            a.ShieldCheck();
            Instantiate(asteroidExplosion, asteroid.transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
            controller.RespawnDelay();
        }
        
        
        GameObject newExplosion = Instantiate(asteroidExplosion, asteroid.transform.position, Quaternion.identity);
        newExplosion.transform.localScale /= mult;

        GameController.score += 10;
        
     Destroy(gameObject); //Уничтожает текущий игоровой объект (астероид)
     Destroy(other.gameObject); //Уничтожаем то, с чем столкнулись
    }
}
