using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
    public Sauvegarde_Minigame sauvegarde;

    void Start()
    {
        if(QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Alimentation))
        {
            Debug.Log("QUEST Restaurant");
            if(/*sauvegarde.HaveMiniGame("MiniGame_Balance") &&*/ sauvegarde.HaveMiniGame("MiniGame_Card")) // TODO Adapter MiniGameBalance poru le jeu
            {
                Debug.Log("Validate");
                QuestManager.ValidateQuest(QUESTS.Alimentation);
            }
        }
    }
}
