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

        //QuestManager.SetQuest((int)QUESTS.Ecole);
        //
        //npc.SetPLayerPrefs(0);
        //npc.idxOffSetDialogue = 0;

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
        if (!(npc.idxOffSetDialogue == 3))
            return false;
        if (QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(quest))
            return true;

        return false;
    }
}
