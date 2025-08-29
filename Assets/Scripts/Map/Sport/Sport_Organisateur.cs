using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sport_Organisateur : Pnj_Ressource
{
    Saving saving;

    protected new void Start()
    {
        base.Start();
        saving = JSON_Manager.LoadData<Saving>("Save");
        
        if(QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Sport))
        {
            string text = "Participer au tournoi de sport.";
            QuestManager.SetTextOffCurrentQuest(text);
        }
        
        if(QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.S_Ressource))
        {
            string text = "Retourner voir le Gérant du tournoi pour récupérer votre ressource.";
            QuestManager.SetTextOffCurrentQuest(text);
        }
    }

    protected override bool TakeRessource()
    {
        if (!saving.statMinigame.ContainsKey("MiniGame_TirBut"))
            return false;
        if (!saving.statMinigame.ContainsKey("MiniGame_Marathon"))
            return false;
        if (!saving.statMinigame.ContainsKey("MiniGame_Boxe"))
            return false;
        //Debug.Log(QuestManager.GetCurrentQuest() + " " + QuestManager.GetQUESTS(quest));
        return QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(quest);
    }

    protected override void ValidateQuestRessource()
    {
        Debug.Log("Validate A_Ressource");
        QuestManager.ValidateQuest(quest);
    }

    protected override bool ToDestroy()
    {
        if (!(npc.idxOffSetDialogue == 3)) 
            return false;
        if (QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(quest))
            return true;

        return false;
    }

}
