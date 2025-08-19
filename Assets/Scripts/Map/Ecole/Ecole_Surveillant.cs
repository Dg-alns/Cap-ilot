using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecole_Surveillant : Pnj_Ressource
{
    protected override bool TakeRessource()
    {
        return QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.E_Ressource);

    }
}
