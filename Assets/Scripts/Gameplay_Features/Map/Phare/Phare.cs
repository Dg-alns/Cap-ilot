using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phare : MonoBehaviour
{
    public GameObject phare;
    public GameObject TopPhare;
    public Sprite phareClean;

    void Start()
    {
        if (PlayerPrefs.HasKey("ReparationPhare"))
        {
            if (PlayerPrefs.GetInt("ReparationPhare") == 1 || QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Maison)) {
                phare.GetComponent<SpriteRenderer>().sprite = phareClean;
                TopPhare.SetActive(false);
            }
        }
    }
}
