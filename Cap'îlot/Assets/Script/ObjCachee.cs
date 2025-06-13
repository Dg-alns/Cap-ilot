using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ObjCachee : MonoBehaviour
{
    public Timer timer;
    public GameObject diabetes;
    public GameObject VisualWinning;
    private int mScore;
    private int mWinnigScore;

    public int point = 200;

    public void SetScore(int score) { mWinnigScore = score; }

    private void Start()
    {
        VisualWinning.SetActive(false);
    }


    public void AddScore()
    {
        mScore += point;
        CheckWin();
    }
    private bool CheckWin()
    {
        if (mScore == mWinnigScore)
        {
            timer.stop = true;
            VisualWinning.SetActive(true);
            VisualWinning.GetComponent<Animator>().SetBool("TEST", true);
        }
        return true;
    }

}
