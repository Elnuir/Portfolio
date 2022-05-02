using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSection : MonoBehaviour
{
    [SerializeField] private GameObject TutorialCanvas;
    private void OnTriggerEnter(Collider other)
    {
        TutorialCanvas.SetActive(true);
        Destroy(gameObject);
    }
}
