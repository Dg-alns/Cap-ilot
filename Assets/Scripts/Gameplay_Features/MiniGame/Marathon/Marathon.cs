using System.Collections;
using UnityEngine;

public class Marathon : Minigame
{
    public PisteManagement pisteManagement;
    public GestionEndurance gestionEndurance;
    public Timer timer;

    public GameObject InfoBeforeGame;
    public Animator startingLight;

    float baseSpeed;
    float currentSpeed;
    float MaxSpeed;

    float patClik = 0.4f;

    bool isPause = false;

    Tools _tools;

    private void Start()
    {
        baseSpeed = pisteManagement.baseSpeed;
        currentSpeed = baseSpeed;

        MaxSpeed = currentSpeed * 2.5f;

        timer.SetNSeconds(1f);
        _tools = FindAnyObjectByType<Tools>();
    }

    public void ToStart()
    {
        StartCoroutine(CanDownSpeed());
        startingLight.gameObject.GetComponent<StartingLight>().LauchLight();
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
        if(InfoBeforeGame.activeSelf)
            return;

        if (isPause)
            return;

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

        if (Input.GetMouseButtonDown(0) && !_tools.IsPointerOverUIElement())
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
    public override void PauseMinigame()
    {
        isPause = true;
        timer.stop = true;
        pisteManagement.Pause();
        gestionEndurance.isPause = true;
    }

    public override void ResumeMinigame()
    {
        isPause = false;
        timer.stop = false;
        pisteManagement.Resume();
        gestionEndurance.isPause= false;
    }
}
