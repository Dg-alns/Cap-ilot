using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportConditionGiveRessource : MonoBehaviour // Todo passer avecn npcRessource
{
    LoadNexScene loadNexScene;
    // Start is called before the first frame update
    void Start()
    {
        //QuestManager.SetQuest(QUESTS.Sport);

        if (QuestManager.GetCurrentQuest() != QuestManager.GetQUESTS(QUESTS.Sport))
            return;

        loadNexScene = FindAnyObjectByType<LoadNexScene>();

        //if (loadNexScene.GetPreviousSceneName().Contains("MiniGame"))
        {
            bool conditionNextQuest = ConditionToGiveRessource();
            if (conditionNextQuest)
            {
                //QuestManager.ValidateQuest(QUESTS.Sport);
            }
        }
    }

    bool ConditionToGiveRessource()
    {
        Saving save = JSON_Manager.LoadData("Save");
        if (save.statMinigame.ContainsKey("MiniGame_TirBut"))
            return false;
        if (save.statMinigame.ContainsKey("MiniGame_Marathon"))
            return false;
        if (save.statMinigame.ContainsKey("MiniGame_Boxe"))
            return false;
        return true;
    }
}
