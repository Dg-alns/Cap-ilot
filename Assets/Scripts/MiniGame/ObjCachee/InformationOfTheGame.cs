using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InformationOfTheGame : MonoBehaviour
{
    public Timer timer;
    public GameObject GameObjectToStart;
    public Score score;

    public Canvas canvas;
    public TextMeshProUGUI myScore;
    public Toggle toggle;

    [Header ("Text Stars")]
    public StarsBeforeGameManager Bronze;
    public StarsBeforeGameManager Argent;
    public StarsBeforeGameManager Or;

    private void Start()
    {
        // Bronze
        Color bronze = new Color(1, 0.3f, 0.2f);

        // Argent
        Color argent = new Color(0.75f, 0.75f, 0.75f);

        // Or
        Color or = new Color(1, 1, 0);


        if (timer != null)
            timer.stop = true;

        if (score == null)
        {
            myScore.gameObject.SetActive(false);

            Bronze.gameObject.SetActive(false);
            Argent.gameObject.SetActive(false);
            Or.gameObject.SetActive(false);

            if(toggle != null)
                toggle.isOn = true;

        }
        else { 

            myScore.text = "Meilleur score : " + score.save.GetBestScore(SceneManager.GetActiveScene().name).ToString();

            if (toggle != null)
                toggle.isOn = score.save.GetCanShowInfo(SceneManager.GetActiveScene().name);

            int nbStars = score.save.GetnbStars(SceneManager.GetActiveScene().name);
            Bronze.InitText(score.MinBronze.ToString());
            Argent.InitText(score.MinArgent.ToString());
            Or.InitText(score.MinOr.ToString());


            switch (nbStars)
            {
                case 0:
                    break;
                case 1:
                    {
                        Bronze.IsDefeatScore(bronze);
                        break;
                    }
                case 2:
                    {
                        Bronze.IsDefeatScore(bronze);
                        Argent.IsDefeatScore(argent); 
                        break;
                    }
                case 3:
                    {
                        Bronze.IsDefeatScore(bronze);
                        Argent.IsDefeatScore(argent);
                        Or.IsDefeatScore(or); 
                        break;
                    }
            }
        }  
    }

    public void StartClassique()
    {
        canvas.gameObject.SetActive(false);
    }

    public void StartGameObjCachee(GameObject save)
    {
        GameObjectToStart.GetComponent<Diabète>().Currentspeed = GameObjectToStart.GetComponent<Diabète>().Basespeed;
        if(toggle == null)
            save.GetComponent<Sauvegarde_Minigame>().SetCanShowInfo(false);

        save.GetComponent<Sauvegarde_Minigame>().SetCanShowInfo(toggle.isOn);
        canvas.gameObject.SetActive(false);
        timer.stop = false;
    }

    public void StartGameMemory(GameObject save)
    {
        GameObjectToStart.GetComponent<Diabete_Memorie>().ActiveDiabète();
        if (toggle == null)
            save.GetComponent<Sauvegarde_Minigame>().SetCanShowInfo(false);

        save.GetComponent<Sauvegarde_Minigame>().SetCanShowInfo(toggle.isOn);
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
