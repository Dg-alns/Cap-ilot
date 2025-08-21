using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relation_RessourcePNJ : Pnj_Ressource
{
    [SerializeField] List<Relation_TalkedNPC> npcTalked = new List<Relation_TalkedNPC>(4);
    //private DialogueNPC npc;
    //private NPCManager npcManager;
    Saving saving;

    protected new void Start()
    {
        base.Start();
        saving = JSON_Manager.LoadData<Saving>(Application.dataPath + "/Json/Save.json");


        npcManager = FindFirstObjectByType<NPCManager>();
        npc = GetComponent<DialogueNPC>();

        if (ToDestroy())
            gameObject.SetActive(false);


        //npc.SetPLayerPrefs(0);
        //npc.idxOffSetDialogue = 0;
    }

    //// Start is called before the first frame update
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
    //                foreach (Relation_TalkedNPC npcT in npcTalked)
    //                {
    //                    npcT.SetTalked(false);
    //                }
    //            }
    //            break;

    //        case (int)OffSet_Dialogue_Surveillant.WaitingForTest:
    //            if (QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.R_Ressoucre))
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

    //    if (QuestManager.GetPlayerPref() != QuestManager.GetQUESTS(QUESTS.Relation))
    //        return;
    //}
    //public void ConditionToGiveRessource()
    //{
    //    Debug.Log("TEST");

    //    if (!saving.statMinigame.ContainsKey("MiniGame_Funambule"))
    //        return;

    //    foreach(Relation_TalkedNPC npcT in npcTalked)
    //    {
    //        if (!npcT.alreadyTalked)
    //            return;
    //    }
    //    QuestManager.NextQuest();
    //}

    protected override bool TakeRessource()
    {
        if (!saving.statMinigame.ContainsKey("MiniGame_Funambule"))
            return false;

        foreach (Relation_TalkedNPC npcT in npcTalked)
        {
            if (!npcT.alreadyTalked)
                return false;
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
