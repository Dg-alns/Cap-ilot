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
            if(sauvegarde.HaveMiniGame("MiniGame_Balance") && sauvegarde.HaveMiniGame("MiniGame_Card"))
            {
                QuestManager.ValidateQuest(QUESTS.Alimentation);
            }
        }
    }
}
