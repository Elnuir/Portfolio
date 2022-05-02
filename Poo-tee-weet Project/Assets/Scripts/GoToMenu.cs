using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseCanvas.activeSelf)
            pauseCanvas.SetActive(true);
            else if (pauseCanvas.activeSelf)
                pauseCanvas.SetActive(false);
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
