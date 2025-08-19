using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relation_RessourcePNJ : MonoBehaviour
{
    [SerializeField] List<Relation_TalkedNPC> npcTalked = new List<Relation_TalkedNPC>(4);
    private DialogueNPC npc;
    private NPCManager npcManager;
    Saving saving;

    private void Start()
    {
        if (QuestManager.GetQUESTS(QUESTS.Relation) >= QuestManager.GetPlayerPref())
        {
            QuestManager.SetQuest(QUESTS.Relation);
        }

        npcManager = FindFirstObjectByType<NPCManager>();
        npc = GetComponent<DialogueNPC>();

        npc.SetPLayerPrefs(0);
        //npc.idxOffSetDialogue = 0;
    }

    // Start is called before the first frame update
    void Update()
    {
        switch (npc.idxOffSetDialogue)
        {
            case (int)OffSet_Dialogue_Surveillant.StartDialogue:
                if (!npcManager.dialogueNpc)
                    return;
                if (npcManager.dialogueNpc.npcId == npc.npcId)
                {
                    npc.idxOffSetDialogue = (int)OffSet_Dialogue_Surveillant.WaitingForTest;
                    npc.SetPLayerPrefs(npc.idxOffSetDialogue);
                }
                break;

            case (int)OffSet_Dialogue_Surveillant.WaitingForTest:
                if (QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.R_Ressoucre))
                {
                    npc.idxOffSetDialogue = (int)OffSet_Dialogue_Surveillant.GiveRessources;
                    npc.SetPLayerPrefs(npc.idxOffSetDialogue);
                }
                break;

            case (int)OffSet_Dialogue_Surveillant.GiveRessources:
                if (!npcManager.dialogueNpc)
                    return;
                if (npcManager.dialogueNpc.npcId == npc.npcId)
                {
                    npc.idxOffSetDialogue = (int)OffSet_Dialogue_Surveillant.Destroy;
                    npc.SetPLayerPrefs(npc.idxOffSetDialogue);
                }
                break;

            case (int)OffSet_Dialogue_Surveillant.Destroy:
                if (npcManager.dialogueNpc)
                    return;
                QuestManager.NextQuest();
                gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("idxDialogue impossible");
                break;
        }

        if (QuestManager.GetPlayerPref() != QuestManager.GetQUESTS(QUESTS.Relation))
            return;
    }
    public void ConditionToGiveRessource()
    {
        Debug.Log("TEST");
        foreach(Relation_TalkedNPC npcT in npcTalked)
        {
            if (!npcT.alreadyTalked)
                return;
        }
        QuestManager.NextQuest();
    }
}
