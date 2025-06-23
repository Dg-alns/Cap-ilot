using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Marathon : MonoBehaviour
{
    public PisteManagement pisteManagement;
    public Timer timer;

    float baseSpeed;
    float currentSpeed;
    float MaxSpeed;

    float patClik = 0.4f;
    private void Start()
    {
        baseSpeed = pisteManagement.baseSpeed;
        currentSpeed = baseSpeed;

        MaxSpeed = currentSpeed * 2.5f;

        timer.SetNSeconds(3);

        StartCoroutine(CanDownSpeed());
    }

    void UpSpeed()
    {
        if(currentSpeed < MaxSpeed)
        {
            currentSpeed += patClik;
            pisteManagement.currentSpeed = currentSpeed;
            pisteManagement.UpdateSpeed(currentSpeed);

            timer.ResetNSecconds();
        }
    }

    void DownSpeed()
    {
        if(currentSpeed > baseSpeed)
        {
            currentSpeed -= patClik;
            pisteManagement.currentSpeed = currentSpeed;
            pisteManagement.UpdateSpeed(currentSpeed);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpSpeed();
        }
    }

    IEnumerator CanDownSpeed()
    {
        while (true)
        {
            if (timer.ElapseNsecond())
            {
                DownSpeed();
                timer.RestartNSeconds();
            }
            yield return null;
        }
        

    }
}
