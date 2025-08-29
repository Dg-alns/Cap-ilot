using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serveur : Pnj_Ressource
{
    protected override bool TakeRessource()
    {
        return QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(quest);
    }

    protected override void ValidateQuestRessource()
    {
        QuestManager.ValidateQuest(quest);
    }

    protected override bool ToDestroy()
    {
        if (QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(quest))
            return true;

        return false;
    }
}
