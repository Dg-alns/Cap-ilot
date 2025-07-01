using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public int redCount = 0;
    public int blueCount = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddRed()
    {
        redCount++;
    }

    public void AddBlue()
    {
        blueCount++;
    }

    public int DiskDifference()
    {
        return blueCount - redCount;
    }
}
