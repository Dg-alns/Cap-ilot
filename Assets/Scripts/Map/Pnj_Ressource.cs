using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum OffSet_Dialogue_Ressource
{
    StartDialogue = 0,
    WaitingForTest = 1,
    GiveRessources = 2,
    Destroy = 3,
}
public class Pnj_Ressource : MonoBehaviour
{
    protected DialogueNPC npc;
    protected NPCManager npcManager;
    [SerializeField] protected QUESTS quest;

    protected virtual void Start()
    {
        npcManager = FindFirstObjectByType<NPCManager>();
        npc = GetComponent<DialogueNPC>();

        if (ToDestroy())
        {
            Debug.Log("Destroy npc ressource : " + gameObject.name);
            gameObject.SetActive(false);

        }


    }

    // Start is called before the first frame update
    void Update()
    {
        switch (npc.idxOffSetDialogue)
        {
            case (int)OffSet_Dialogue_Ressource.StartDialogue:
                if(!npcManager.dialogueNpc)
                    return;
                if (npcManager.dialogueNpc.npcId == npc.npcId)
                {
                    //npc.idxOffSetDialogue = (int)OffSet_Dialogue_Ressource.WaitingForTest;
                    npc.SetPLayerPrefs((int)OffSet_Dialogue_Ressource.WaitingForTest);
                    FirstTalk();
                }
                break;

            case (int)OffSet_Dialogue_Ressource.WaitingForTest:
                if(TakeRessource())
                {
                    //npc.idxOffSetDialogue = (int)OffSet_Dialogue_Ressource.GiveRessources;
                    npc.SetPLayerPrefs((int)OffSet_Dialogue_Ressource.GiveRessources);
                }
                break;

            case (int)OffSet_Dialogue_Ressource.GiveRessources:
                if (!npcManager.dialogueNpc)
                    return;
                if (npcManager.dialogueNpc.npcId == npc.npcId)
                {
                    //npc.idxOffSetDialogue = (int)OffSet_Dialogue_Ressource.Destroy;
                    npc.SetPLayerPrefs((int)OffSet_Dialogue_Ressource.Destroy);
                }
                break;

            case (int)OffSet_Dialogue_Ressource.Destroy:
                if (npcManager.dialogueNpc)
                    return;
                gameObject.SetActive(false);
                ValidateQuestRessource();
                break;
            default:
                Debug.LogError("idxDialogue impossible");
                break;
        }
    }
    virtual protected void FirstTalk()
    {
        return;
    }

    virtual protected bool TakeRessource()
    {
        return false;
    }

    virtual protected void ValidateQuestRessource()
    {
        return;
    }

    virtual protected bool ToDestroy()
    {
        return false;
    }
}
