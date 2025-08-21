using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sport_Organisateur : Pnj_Ressource
{
    //private DialogueNPC npc;
    //private NPCManager npcManager;

    //private void Start()
    //{
    //    //if (QuestManager.GetQUESTS(QUESTS.Sport) >= QuestManager.GetCurrentQuest())
    //    //{
    //    //    QuestManager.SetQuest(QUESTS.Sport);
    //    //}

    //    npcManager = FindFirstObjectByType<NPCManager>();
    //    npc = GetComponent<DialogueNPC>();

    //    npc.SetPLayerPrefs(0);
    //    //npc.idxOffSetDialogue = 0;
    //}

    // Start is called before the first frame update
    //void Update()
    //{
    //    switch (npc.idxOffSetDialogue)
    //    {
    //        case (int)OffSet_Dialogue_Surveillant.StartDialogue:
    //            if (!npcManager.dialogueNpc)
    //                return;
    //            if (npcManager.dialogueNpc.npcId == npc.npcId)
    //            {
    //                npc.idxOffSetDialogue = (int)OffSet_Dialogue_Surveillant.WaitingForTest;
    //                npc.SetPLayerPrefs(npc.idxOffSetDialogue);
    //            }
    //            break;

    //        case (int)OffSet_Dialogue_Surveillant.WaitingForTest:
    //            if (QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.S_Ressource))
    //            {
    //                npc.idxOffSetDialogue = (int)OffSet_Dialogue_Surveillant.GiveRessources;
    //                npc.SetPLayerPrefs(npc.idxOffSetDialogue);
    //            }
    //            break;

    //        case (int)OffSet_Dialogue_Surveillant.GiveRessources:
    //            if (!npcManager.dialogueNpc)
    //                return;
    //            if (npcManager.dialogueNpc.npcId == npc.npcId)
    //            {
    //                npc.idxOffSetDialogue = (int)OffSet_Dialogue_Surveillant.Destroy;
    //                npc.SetPLayerPrefs(npc.idxOffSetDialogue);
    //            }
    //            break;

    //        case (int)OffSet_Dialogue_Surveillant.Destroy:
    //            if (npcManager.dialogueNpc)
    //                return;
    //            QuestManager.NextQuest();
    //            gameObject.SetActive(false);
    //            break;
    //        default:
    //            Debug.LogError("idxDialogue impossible");
    //            break;
    //    }
    //}

    protected override bool TakeRessource()
    {
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
