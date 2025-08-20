using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitainPhare : MonoBehaviour
{
    public NextSceneDestination sceneDestination;

    public GameObject dialogueCapitain;
    public GameObject MiniGameCapitain;

    public NPCManager npcManager;
    void Awake()
    {
        if (PlayerPrefs.HasKey("ReparationPhare") == false)
        {
            PlayerPrefs.SetInt("ReparationPhare", 0);
        }

        if (PlayerPrefs.GetInt("ReparationPhare") == 1)
            QuestManager.ValidateQuest(QUESTS.ReparationPhare);


        ActiveCapitain();
        ActiveSetOffDialogue();
    }

    void ActiveCapitain()
    {
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Maison) || PlayerPrefs.GetInt("ReparationPhare") == 1)
        {
            dialogueCapitain.SetActive(true);
            return;
        }

        if (PlayerPrefs.GetInt("ReparationPhare") == 0)
        {
            MiniGameCapitain.SetActive(true);
        }
    }

    void ActiveSetOffDialogue()
    {
        if (dialogueCapitain == null)
            return;

        if(QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 1;
        }
        if(QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Phare))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 2;

        }
        if(QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(QUESTS.Phare))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 3;
        }
    }

    private void Update()
    {
        if (npcManager.dialogueNpc == null)
            return;

        if (dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue == 1 && QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
            QuestManager.ValidateQuest(QUESTS.DemandeCapitaine);

        if (dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue == 2 && QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Phare))
            QuestManager.ValidateQuest(QUESTS.Phare);


        ActiveSetOffDialogue();
    }
}
