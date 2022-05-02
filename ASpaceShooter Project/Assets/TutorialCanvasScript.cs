using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialCanvas;
    void Update()
    {
        if (tutorialCanvas.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
