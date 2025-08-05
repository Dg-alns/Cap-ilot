using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phare : MonoBehaviour
{
    public GameObject diabete;

    public GameObject phare;
    public GameObject TopPhare;
    public Sprite phareClean;


    void Start()
    {
        if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
        {
            diabete.SetActive(true);
        }


        if (PlayerPrefs.HasKey("ReparationPhare"))
        {
            if (PlayerPrefs.GetInt("ReparationPhare") == 1) {
                phare.GetComponent<SpriteRenderer>().sprite = phareClean;
                TopPhare.SetActive(false);
            }
        }
    }
}
