using UnityEngine;

public class InformationOfTheGame : MonoBehaviour
{
    public Timer timer;
    public GameObject GameObjectToStart;

    public Canvas canvas;

    private void Start()
    {
        if(timer != null)
            timer.stop = true;
    }

    public void StartClassique()
    {
        canvas.gameObject.SetActive(false);
    }

    public void StartGameObjCachee()
    {
        GameObjectToStart.GetComponent<Diabète>().Currentspeed = GameObjectToStart.GetComponent<Diabète>().Basespeed;

        canvas.gameObject.SetActive(false);
        timer.stop = false;
    }

    public void StartGameMemory()
    {
        GameObjectToStart.GetComponent<Diabete_Memorie>().ActiveDiabète();

        canvas.gameObject.SetActive(false);
        timer.stop = false;
    }

    public void StartMarathon()
    {
        gameObject.SetActive(false);
        GameObjectToStart.GetComponent<Marathon>().ToStart();
        timer.stop = false;
    }

    public void StarBoat()
    {
        gameObject.SetActive(false);
        GameObjectToStart.GetComponent<Boat_Game>().StartGame();
    }
    public void StartInjecInsu()
    {
        gameObject.SetActive(false);
        GameObjectToStart.SetActive(true);
    }
}
