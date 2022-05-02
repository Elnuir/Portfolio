using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableForeignBook : MonoBehaviour
{
    public GameObject dialogHintWindow;
    public GameObject ForeignBookFrame;
    private bool dialogWindowShowed;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dialogHintWindow.SetActive(true);
            if (Input.GetKey(KeyCode.E) && dialogWindowShowed == false)
            {
                ForeignBookFrame.SetActive(true);
                dialogWindowShowed = true;
                dialogHintWindow.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            dialogHintWindow.SetActive(false);
            dialogWindowShowed = false;
        }
    }
}
