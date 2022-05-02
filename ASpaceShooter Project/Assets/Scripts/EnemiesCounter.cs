using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesCounter : MonoBehaviour
{
    int count = 0;
    private Text counterText;
    [SerializeField] private GameObject winScreen;


    private void Start()
    {
        counterText = GetComponent<Text>();
    }

    void Update()
    {
       var count = FindObjectsOfType<Enemy1>().Length;
       counterText.text = "Enemies: " + count.ToString();
       if (count == 0)
       {
           winScreen.SetActive(true);

       }

    }
}
