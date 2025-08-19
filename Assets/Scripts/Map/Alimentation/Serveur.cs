using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serveur : Pnj_Ressource
{
    protected override bool TakeRessource()
    {
        return QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.A_Ressource);
    }

    protected override void ValidateQuestRessource()
    {
        Debug.Log("Validate A_Ressource");
        QuestManager.ValidateQuest(QUESTS.A_Ressource);
    }

    protected override bool ToDestroy()
    {
        if (QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(QUESTS.A_Ressource))
            return true;

        return false;
    }
}
