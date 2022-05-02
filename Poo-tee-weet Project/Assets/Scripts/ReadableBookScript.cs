using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableBookScript : MonoBehaviour
{
    public GameObject dialogHintWindow;
    private bool dialogWindowShowed;
    public GameObject BookFrame;
    public GameObject WordInTheEndOfLevel;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "Book")
            {
                dialogHintWindow.SetActive(true);
                if (Input.GetKey(KeyCode.E) && dialogWindowShowed == false)
                {
                    BookFrame.SetActive(true);
                    dialogWindowShowed = true;
                    WordInTheEndOfLevel.SetActive(true);
                }
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dialogHintWindow.SetActive(false);
        }
    }

    private void Update()
    {
        if (dialogWindowShowed && Input.GetKeyDown(KeyCode.R))
        {
            BookFrame.SetActive(false);
            dialogWindowShowed = false;
        }
    }
}
