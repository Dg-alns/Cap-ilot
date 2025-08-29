using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relation_RessourcePNJ : Pnj_Ressource
{
    [SerializeField] List<AlreadyTalkedNPC> npcTalked = new List<AlreadyTalkedNPC>(4);
    //private DialogueNPC npc;
    //private NPCManager npcManager;
    [SerializeField] Sauvegarde save;

    protected new void Start()
    {
        base.Start();


        if (QuestManager.GetCurrentQuest() <= QuestManager.GetQUESTS(QUESTS.R_Ressoucre))
            QuestManager.SetQuest((int)QUESTS.R_Ressoucre);

        if (QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Relation))
        {
            string text = "Discuter à l'ensemble des personne présente dans le parc.";
            QuestManager.SetTextOffCurrentQuest(text);
        }

        if (QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.R_Ressoucre))
        {
            string text = "Récupérer votre ressource.";
            QuestManager.SetTextOffCurrentQuest(text);
        }

        npcManager = FindFirstObjectByType<NPCManager>();
        npc = GetComponent<DialogueNPC>();

        if (ToDestroy())
            gameObject.SetActive(false);
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
        if (!save.StatMinigame.ContainsKey("MiniGame_Funambule"))
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
