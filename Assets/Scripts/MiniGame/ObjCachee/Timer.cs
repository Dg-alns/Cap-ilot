using TMPro;
using UnityEngine;

struct TIME
{
    public int SecondesUniter;
    public int SecondesDizaine;
    public int MinuteUniter;
    public int MinuteDizaine;
}

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TextTime;

    float baseSeconds = 1;
    float DTseconds = 1;

    float nSec = 0;
    float baseNSec = 0;
    TIME time;

    public bool stop = false;
    
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

    public void SetNSeconds(float seconds ) { nSec = seconds; baseNSec = seconds; }
    public void RestartNSeconds( ) { nSec += baseNSec; }
    public void ResetNSecconds( ) { nSec = baseNSec; }
    public void RestartTimer()
    {
        baseSeconds = 1;
        DTseconds = 1;

        nSec = 0;
        baseNSec = 0;

        time.SecondesUniter = 0;
        time.SecondesDizaine = 0;
        time.MinuteUniter = 0;
        time.MinuteDizaine = 0;

        stop = false;
        TextTime.text = $"{time.MinuteDizaine}{time.MinuteUniter}.{time.SecondesDizaine}{time.SecondesUniter}";

    }

    TIME GetTime() { return time; }

    public int GetTimeInSecondes()
    {
        return time.MinuteDizaine * 600 + time.MinuteUniter * 60 + time.SecondesDizaine * 10 + time.SecondesUniter;
    }

    public bool ElapseNsecond()
    {
        nSec -= Time.deltaTime;

        if(nSec <= 0 )
            return true;

        return false;
    }

    void Update()
    {
        if (stop == false) {
            if (Elapse1second())
            {

                time.SecondesUniter++;
                if (time.SecondesUniter >= 10)
                {
                    time.SecondesUniter = 0;
                    time.SecondesDizaine++;
                }

                if (time.SecondesDizaine >= 6)
                {
                    time.SecondesDizaine = 0;
                    time.MinuteUniter++;
                }

                if (time.MinuteUniter >= 10)
                {
                    time.MinuteUniter = 0;
                    time.MinuteDizaine++;
                }

                TextTime.text = $"{time.MinuteDizaine}{time.MinuteUniter}.{time.SecondesDizaine}{time.SecondesUniter}";
            } 
        }
    }
}
