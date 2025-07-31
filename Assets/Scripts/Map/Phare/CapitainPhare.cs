using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitainPhare : MonoBehaviour
{
    public NextSceneDestination sceneDestination;

    public GameObject dialogueCapitain;
    public GameObject MiniGameCapitain;

    int FinishPhare = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("ReparationPhare"))
        {
            PlayerPrefs.SetInt("ReparationPhare", 0);
            FinishPhare = PlayerPrefs.GetInt("ReparationPhare");
        }
        else
        {
            PlayerPrefs.SetInt("ReparationPhare", 0);
            FinishPhare = PlayerPrefs.GetInt("ReparationPhare");
        }

        if (sceneDestination.GetPreviousScene().Contains("MiniGame_LightHouse"))
        {
            PlayerPrefs.SetInt("ReparationPhare", 1);
            FinishPhare = PlayerPrefs.GetInt("ReparationPhare");
        }

        ActiveCapitain();
    }

    void ActiveCapitain()
    {
        if(PlayerPrefs.GetInt("ReparationPhare") == 0)
        {
            MiniGameCapitain.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("ReparationPhare") == 1)
        {
            dialogueCapitain.SetActive(true);
        }
    }

    void ActiveSetOffDialogue()
    {
        //Condition avec les quetes todo Diego
    }
}
