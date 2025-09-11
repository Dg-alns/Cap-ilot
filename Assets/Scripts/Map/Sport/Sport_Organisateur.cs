using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sport_Organisateur : Pnj_Ressource
{
    [SerializeField] Sauvegarde save;

    protected new void Start()
    {
        base.Start();
        
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
        if (!save.StatMinigame.ContainsKey("MiniGame_TirBut"))
            return false;
        if (!save.StatMinigame.ContainsKey("MiniGame_Marathon"))
            return false;
        if (!save.StatMinigame.ContainsKey("MiniGame_Boxe"))
            return false;

        if(QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Sport))
            QuestManager.ValidateQuest(QUESTS.Sport);

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
