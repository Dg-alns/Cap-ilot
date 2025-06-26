using System.Collections;
using UnityEngine;

public class Marathon : MonoBehaviour
{
    public PisteManagement pisteManagement;
    public GestionEndurance gestionEndurance;
    public Timer timer;

    public Animator startingLight;

    float baseSpeed;
    float currentSpeed;
    float MaxSpeed;

    float patClik = 0.4f;
    private void Start()
    {
        baseSpeed = pisteManagement.baseSpeed;
        currentSpeed = baseSpeed;

        MaxSpeed = currentSpeed * 2.5f;

        timer.SetNSeconds(1f);

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
        if (startingLight.GetBool("GoLight"))
        {
            pisteManagement.StopMovementPist();
            timer.stop = true;
            return;
        }

        if(startingLight.GetBool("GoHide"))
        {
            pisteManagement.StartMovementPist();
            timer.stop = false;
            startingLight.gameObject.SetActive(false);
        }


        if (pisteManagement.Finish)
        {
            timer.stop = true;
            return;
        }

        if(gestionEndurance.restartEndurance)
        {

            currentSpeed = baseSpeed;
            pisteManagement.currentSpeed = currentSpeed;
            pisteManagement.UpdateSpeed(currentSpeed);
            gestionEndurance.restartEndurance = false;
        }


        if (gestionEndurance.HaveNoneEndurance)
        {
            if (currentSpeed != 0)
            {
                currentSpeed = 0;
                pisteManagement.currentSpeed = currentSpeed;
                pisteManagement.UpdateSpeed(currentSpeed);
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (gestionEndurance.tackeRavito)
            {
                gestionEndurance.tackeRavito = false;
                return;
            }

            UpSpeed();
            gestionEndurance.UseEndurance();

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
