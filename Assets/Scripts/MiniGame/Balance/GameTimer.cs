using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class GameTimer : MonoBehaviour
{
    public float totalTime = 90f;
    public TextMeshProUGUI timerText;
    public GameObject winScreen;

    private float timeLeft;
    private bool isRunning = true;

    void Start()
    {
        timeLeft = totalTime;
        winScreen.SetActive(false);
    }

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;
        int secondsLeft = Mathf.CeilToInt(timeLeft);
        timerText.text = secondsLeft.ToString();

        if (timeLeft <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        isRunning = false;

        if (timeLeft == 0)
        {
            winScreen.SetActive(true);
        }
        else
        {
           //looseScreen a faire
        }
    }
}
