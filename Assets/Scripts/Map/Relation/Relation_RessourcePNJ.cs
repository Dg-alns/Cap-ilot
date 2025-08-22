using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relation_RessourcePNJ : Pnj_Ressource
{
    [SerializeField] List<AlreadyTalkedNPC> npcTalked = new List<AlreadyTalkedNPC>(4);
    //private DialogueNPC npc;
    //private NPCManager npcManager;
    Saving saving;

    protected new void Start()
    {
        QuestManager.SetQuest((int)QUESTS.R_Ressoucre);
        base.Start();
        saving = JSON_Manager.LoadData<Saving>(Application.dataPath + "/Json/Save.json");


        npcManager = FindFirstObjectByType<NPCManager>();
        npc = GetComponent<DialogueNPC>();

        if (ToDestroy())
            gameObject.SetActive(false);


        npc.SetPLayerPrefs(0);
        npc.idxOffSetDialogue = 0;
    }

    protected override void FirstTalk()
    {
        foreach (AlreadyTalkedNPC nPC in npcTalked)
        {
            nPC.SetTalked(false);
        }
    }

    protected override bool TakeRessource()
    {
        if (!saving.statMinigame.ContainsKey("MiniGame_Funambule"))
            return false;

        foreach (AlreadyTalkedNPC npcT in npcTalked)
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
        //QuestManager.SetQuest((int)quest + 1);
    }

    protected override bool ToDestroy()
    {
        if (QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(quest))
            return true;

        return false;
    }
}
