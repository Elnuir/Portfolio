using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEnter : MonoBehaviour
{
    [SerializeField] private GameObject block;

    private void OnTriggerEnter(Collider other)
    {
        block.SetActive(true);
    }
}
