using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;

    [SerializeField] private Transform parent;
    [SerializeField] private int scoreToAdd = 5;
    private Score scoreBoard;
    [SerializeField] private int hits = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreBoard  = FindObjectOfType<Score>();
        AddNonTriggerCollider();
    }

    // Update is called once per frame
    void AddNonTriggerCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scoreToAdd);
        hits--;
        if (hits <= 0)
            Death();
    }

    void Death()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
