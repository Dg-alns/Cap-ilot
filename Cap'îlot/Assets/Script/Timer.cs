using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TextTime;

    float baseSeconds = 1;
    float DTseconds = 1;

    int SUniter = 0;
    int SDizaine = 0;
    int MUniter = 0;
    int MDizaine = 0;
    
    void Start()
    {
        TextTime.text = "00.00";
    }

    bool Elapse1second()
    {
        DTseconds -= Time.deltaTime;

        if(DTseconds <= 0 )
        {
            DTseconds += baseSeconds;
            return true;
        }

        return false;
    }

    void UpdateTimer()
    {
        SUniter++;
        if(SUniter >= 10 )
        {
            SUniter = 0;
            SDizaine++;
        }

        if (SDizaine >= 6)
        {
            SDizaine = 0;
            MUniter++;
        }

        if (MUniter >= 10)
        {
            MUniter = 0;
            MDizaine++;
        }

        TextTime.text = MDizaine.ToString() + MUniter.ToString() + "." + SDizaine.ToString() + SUniter.ToString();
    }


    void Update()
    {
        if(Elapse1second())
        {
            UpdateTimer();
        }

    }
}
