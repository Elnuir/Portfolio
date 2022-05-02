using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStart : MonoBehaviour
{
    public GameObject Boss;
    public GameObject NormalCamera;
    public GameObject BossCamera;
    public Animator BossAnimator;
    public GameObject BlockExitCollider;
    public GameObject BossConfiner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NormalCamera.SetActive(false);
        BossCamera.SetActive(true);
        Boss.SetActive(true);
        BossAnimator.Play("BossApperance");
        BlockExitCollider.SetActive(true);
        BossConfiner.SetActive(true);
        Destroy(gameObject);
    }
}
