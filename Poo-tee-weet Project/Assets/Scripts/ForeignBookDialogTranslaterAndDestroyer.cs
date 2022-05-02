using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeignBookDialogTranslaterAndDestroyer : MonoBehaviour
{
    public GameObject[] deciphered;
    public GameObject[] ciphered;
    
    void Update()
    {
        if (gameObject.activeSelf)
        {
            foreach (var text in ciphered)
            {
                text.gameObject.SetActive(false);
                print("Translated");
            }
            foreach (var text in deciphered)
            {
                text.gameObject.SetActive(true);
                print("Translated");
            }
            
            Destroy(gameObject, 12f); ;
        }
    }

    
}
