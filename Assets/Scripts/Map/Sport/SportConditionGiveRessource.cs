using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportConditionGiveRessource : MonoBehaviour
{
    LoadNexScene loadNexScene;
    // Start is called before the first frame update
    void Start()
    {
        //QuestManager.SetQuest(QUESTS.Sport);

        if (QuestManager.GetPlayerPref() != QuestManager.GetQUESTS(QUESTS.Sport))
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
        //if (save.statMinigame.ContainsKey("MiniGame_TirBut"))
        //    return false;
        //if (save.statMinigame.ContainsKey("MiniGame_Marathon"))
        //    return false;
        //if (save.statMinigame.ContainsKey("MiniGame_Boxe"))
        //    return false;
        return true;
    }
}
