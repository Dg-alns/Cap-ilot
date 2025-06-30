using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    int score = 0;
    public Text scoreText;

    void Awake()
    {
        instance = this;
    }

    public void AddPoint()
    {
        score++;
        UpdateUI();
    }

    public void LosePoint()
    {
        score--;
        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
    }
    public int GetScore()
    {
        return score;
    }

}
