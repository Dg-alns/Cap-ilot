using UnityEngine;

public class InformationOfTheGame : MonoBehaviour
{
    public Timer timer;
    public Diabète diabete;

    public Canvas canvas;

    public void StartGameObjCachee()
    {
        diabete.Currentspeed = diabete.Basespeed;

        canvas.gameObject.SetActive(false);
        timer.stop = false;
    }
}
