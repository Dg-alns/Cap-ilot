using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BalanceManager : MonoBehaviour
{
    public static BalanceManager instance;
    public int nbCookie = 0;
    public int nbInsuline = 0;
    int diff = 0;

    public void AddCookie()
    {
        nbCookie++;
    }

    public void LoseCookie()
    {
        nbCookie = Mathf.Max(0, nbCookie - 1);
    }

    public void AddInsuline()
    {
        nbInsuline++;
    }

    public void LoseInsuline()
    {
        nbInsuline = Mathf.Max(0, nbCookie - 1);
    }

    int GetDiff()
    {  
        diff = nbCookie - nbInsuline;
        return diff; 
    }

    void Update()
    {
        if (GetDiff() != 0)
        {
            if (nbCookie > nbInsuline)
                transform.rotation = Quaternion.Euler(0, 0, 10-GetDiff());
            else
                transform.rotation = Quaternion.Euler(0, 0, 10+ GetDiff());
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 3);
        }
    }
}