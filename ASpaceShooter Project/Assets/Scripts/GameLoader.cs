using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    void Start()
    {
        Invoke("FirstLevelLoad", 11f);
    }

    // Update is called once per frame
    void FirstLevelLoad()
    {
        SceneManager.LoadScene(3);
    }
}
