using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemplateSaveMinigame
{
    public int _nbStar;
    public int _bestScore;
    public bool _showInfo;
    public TemplateSaveMinigame(int nbStar,int bestScore, bool showInfo)
    {
        _nbStar = nbStar;
        _bestScore = bestScore;
        _showInfo = showInfo;
    }

    public void CheckNewScore(Score score, bool reverse = false)
    {
        if (reverse)
        {
            if (_bestScore > score.MiniGamePoint)
            {
                _bestScore = score.MiniGamePoint;
                CheckNewStar(score.nbStars);
            }
            return;
        }

        if (_bestScore < score.MiniGamePoint)
        { 
            _bestScore = score.MiniGamePoint;
            CheckNewStar(score.nbStars);
        }
    }

    private void CheckNewStar(int nbStars)
    {
        if(nbStars > _nbStar)
        {
            _nbStar = nbStars;
        }
    }

    public void CheckNewInfo(bool info)
    {
        if(info != _showInfo)
        {
            _showInfo = info;
        }
    }
}
