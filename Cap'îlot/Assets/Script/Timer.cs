using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TextTime;

    float baseSeconds = 1;
    float DTseconds = 1;

    float nSec = 0;
    float baseNSec = 0;

    int SUniter = 0;
    int SDizaine = 0;
    int MUniter = 0;
    int MDizaine = 0;
    
    void Start()
    {
        if(TextTime != null) 
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

    public void SetNSeconds(int seconds ) { nSec = seconds; baseNSec = seconds; }
    public void RestartNSeconds( ) { nSec += baseNSec; }
    public bool ElapseNsecond()
    {
        nSec -= Time.deltaTime;

        if(nSec <= 0 )
            return true;

        return false;
    }

    public void UpdateTimer()
    {
        if (Elapse1second())
        {

            SUniter++;
            if (SUniter >= 10)
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
    }
}
