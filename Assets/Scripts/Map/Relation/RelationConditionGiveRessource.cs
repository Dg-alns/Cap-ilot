using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationConditionGiveRessource : MonoBehaviour
{


    LoadNexScene loadNexScene;

    private void Start()
    {
        //QuestManager.SetQuest(QUESTS.Sport);

        if (QuestManager.GetPlayerPref() != QuestManager.GetQUESTS(QUESTS.Relation))
            return;

        loadNexScene = FindAnyObjectByType<LoadNexScene>();

        //if (loadNexScene.GetPreviousSceneName().Contains("MiniGame"))
        {
            bool conditionNextQuest = ConditionToGiveRessource();
            if (conditionNextQuest)
            {
                QuestManager.NextQuest();
            }
        }
    }

    bool ConditionToGiveRessource()
    {
        //Saving save = null;
        //if (save.statMinigame.ContainsKey("MiniGame_Funambule"))
        //    return false;
        return true;
    }
}
