using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum OffSet_RessourceDialogue
{
    StartDialogue = 0,
    WaitingForTest = 1,
    GiveRessources = 2,
    Destroy = 3,
}

public class RessourceNPC : NPC
{
    
    [SerializeField] QUESTS ileFirstQuest = QUESTS.None;
    [SerializeField] QUESTS ressourceQuest;

    private NPCManager npcManager;

    // Start is called before the first frame update
    void Start()
    {
        if (QuestManager.GetQUESTS(ileFirstQuest) >= QuestManager.GetPlayerPref())
        {
            QuestManager.SetQuest(ileFirstQuest);
        }

        npcManager = FindFirstObjectByType<NPCManager>();
        //npc = GetComponent<DialogueNPC>();

        SetPLayerPrefs(0);
        idxOffSetDialogue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (idxOffSetDialogue)
        {
            case (int)OffSet_RessourceDialogue.StartDialogue:
                // Check if it's the end of the start dialogue to go for WaitingTest
                if (!npcManager.dialogueNpc)
                    return;
                if (npcManager.dialogueNpc.npcId == npcId)
                {
                    idxOffSetDialogue = (int)OffSet_RessourceDialogue.WaitingForTest;
                    SetPLayerPrefs(idxOffSetDialogue);
                }
                break;

            case (int)OffSet_RessourceDialogue.WaitingForTest:
                if (QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(ressourceQuest))
                {
                    idxOffSetDialogue = (int)OffSet_RessourceDialogue.GiveRessources;
                    SetPLayerPrefs(idxOffSetDialogue);
                }
                break;

            case (int)OffSet_RessourceDialogue.GiveRessources:
                if (!npcManager.dialogueNpc)
                    return;
                if (npcManager.dialogueNpc.npcId == npcId)
                {
                    idxOffSetDialogue = (int)OffSet_RessourceDialogue.Destroy;
                    SetPLayerPrefs(idxOffSetDialogue);
                }
                break;

            case (int)OffSet_RessourceDialogue.Destroy:
                if (npcManager.dialogueNpc)
                    return;
                QuestManager.NextQuest();
                gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("idxDialogue impossible");
                break;
        }
    }
}
