using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopital : MonoBehaviour
{
    public LoadNexScene nexScene;

    void Start()
    {
        if(QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.Phare))
            gameObject.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnManagement.SetFirstInScene(1);
        nexScene.LoadNextScene("Chambre");
    }
}
