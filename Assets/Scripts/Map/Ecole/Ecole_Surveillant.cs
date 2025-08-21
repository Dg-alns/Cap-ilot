using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecole_Surveillant : Pnj_Ressource
{
    AlreadyTalkedNPC talkedNPC;
    Ecole_Map ecole_Map;
    protected new void Start()
    {
        base.Start();

        talkedNPC = GetComponent<AlreadyTalkedNPC>();
        ecole_Map = FindAnyObjectByType<Ecole_Map>();

        if (talkedNPC.alreadyTalked)
        {
            ecole_Map.ActivePanneau();
        }
    }

    protected override bool TakeRessource()
    {
        if (talkedNPC.alreadyTalked)
        {
            ecole_Map.ActivePanneau();
        }

        return QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(quest);
    }

    protected override void ValidateQuestRessource()
    {
        Debug.Log("Validate A_Ressource");
        QuestManager.ValidateQuest(quest);
    }

    protected override bool ToDestroy()
    {
        if (QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(quest))
            return true;

        return false;
    }
}
