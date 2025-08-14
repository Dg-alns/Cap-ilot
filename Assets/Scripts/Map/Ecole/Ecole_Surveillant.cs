using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum OffSet_Dialogue_Surveillant
{
    StartDialogue = 0,
    WaitingForTest = 1,
    GiveRessources = 2,
    Destroy = 3,
}
public class Ecole_Surveillant : MonoBehaviour
{
    private DialogueNPC npc;
    private NPCManager npcManager;

    private void Start()
    {
        npcManager = FindFirstObjectByType<NPCManager>();
        npc = GetComponent<DialogueNPC>();

        //npc.SetPLayerPrefs(2);
        //npc.idxOffSetDialogue = 2;
    }

    // Start is called before the first frame update
    void Update()
    {
        switch (npc.idxOffSetDialogue)
        {
            case (int)OffSet_Dialogue_Surveillant.StartDialogue:
                if(!npcManager.dialogueNpc)
                    return;
                if (npcManager.dialogueNpc.npcId == npc.npcId)
                {
                    npc.idxOffSetDialogue = (int)OffSet_Dialogue_Surveillant.WaitingForTest;
                    npc.SetPLayerPrefs(npc.idxOffSetDialogue);
                }
                break;

            case (int)OffSet_Dialogue_Surveillant.WaitingForTest:
                if (QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.E_Ressource))
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
                gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("idxDialogue impossible");
                break;
        }
    }
}
