using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;

    private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        
    }
    public void ScoreHit(int scorePerHit)
    {
        score += scorePerHit;
        scoreText.text = "Score: " + score.ToString();
    }
}
