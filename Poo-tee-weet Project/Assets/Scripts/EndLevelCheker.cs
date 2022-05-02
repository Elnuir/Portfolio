using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCheker : MonoBehaviour
{
    public GameObject WinCanvas;
    void Update()
    {
        var a = FindObjectOfType<ReadableForeignBook>();
        var b = FindObjectOfType<Boss>();
        if (a == null && b == null)
        {
            WinCanvas.SetActive(true);
        }
    }
}
