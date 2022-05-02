using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public UnityEngine.UI.Text scoreLabel;
    public UnityEngine.UI.Button startButton;
    public static int score = 0;
    public static bool isStarted = false;
    public GameObject playerShip;
    public List<GameObject> lifes;
    
    public GameObject gameOver;
    public UnityEngine.UI.Text yourScore;
    

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(delegate { menu.SetActive(false); isStarted = true;});//что делать при нажатии на кнопку
        
        
    }

    // Update is called once per frame
    void Update()
    {
        var a = scoreLabel.text = "Score: " + score;
    }
    public void RespawnDelay()
    {
        if (lifes.Count > 0)
        {
            GameObject.Destroy(lifes[0]);
            lifes.RemoveAt(0);
            
            // life1.SetActive(false);
            Invoke(nameof(Respawn), 2f);
        }
        else
        {
            gameOver.SetActive(true);
            yourScore.text = "Your Score: " + score;
            yourScore.gameObject.SetActive(true);
            Debug.Log(yourScore.gameObject);
            Invoke(nameof(Reload), 5);
        }
    }

    void Respawn()
    { 
        var spawned = Instantiate(playerShip, new Vector3(0, 0, 0), Quaternion.identity);
        spawned.transform.GetChild(0).gameObject.SetActive(true);
       spawned.tag = "Grinder";
       Invoke(nameof(DeactivateShield), 3f);
       
    }

    void DeactivateShield()
    {
        GameObject.FindObjectOfType<PlayerScript>().transform.GetChild(0).gameObject.SetActive(false);

        foreach (var a in   FindObjectsOfType<PlayerScript>())
        {
            a.transform.GetChild(0).gameObject.SetActive(false);
        }

      
        GameObject.FindObjectOfType<PlayerScript>().tag = "Player";
    }

    void Reload()
    {
        SceneManager.LoadScene(0);
        GameController.isStarted = false;
        GameController.score = 0;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

    }
}
