using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("В секундах")] [SerializeField] private float LoadLevelDelay;

    [SerializeField] private GameObject explosionFX;

    private void OnTriggerEnter(Collider other)
    {
        print("Hit Trigger");
        StartDeath();
        explosionFX.SetActive(true);
        Invoke(nameof(RestartLevel), LoadLevelDelay);
    }

    void StartDeath()
    {
        SendMessage("OnPlayerDeath");
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
