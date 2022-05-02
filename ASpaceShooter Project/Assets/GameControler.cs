using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    
    public GameObject menuCanvas;
    public GameObject subMenuCanvas;
    public GameObject AreYouSure;
    private bool isPaused;
    private bool areYouSure;
    public Button yesButton;
    public Button noButton;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
    }


    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Escape) && !areYouSure) {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            menuCanvas.SetActive (isPaused);
        }

        if (AreYouSure.activeSelf)
        {
            areYouSure = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AreYouSure.SetActive(false);
                subMenuCanvas.SetActive(true);
                areYouSure = false;
                yesButton.onClick.RemoveAllListeners();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        menuCanvas.SetActive(false);

    }
    public void ArYouSureMenu()
   {
       yesButton.onClick.AddListener(CheckLoadMenu);
   }
   public void ArYouSureExit()
   {
       yesButton.onClick.AddListener(CheckQuit);
   }
   public void CheckLoadMenu()
   {
       SceneManager.LoadScene(0);
       print("menu");
   }
   public void CheckQuit()
   {
       Application.Quit();
       print("Quit");
   }

   public void RemovingListeners()
   {
       yesButton.onClick.RemoveAllListeners();
   }

   public void Restart()
   {
       Time.timeScale = 1;
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void TimeScaleSetter()
   {
       Time.timeScale = 1;
   }

   public void NextLevel()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void ToMainMenu()
   {
       Time.timeScale = 1;
       SceneManager.LoadScene(0);
   }


}
