using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitainPhare : MonoBehaviour
{
    public NextSceneDestination sceneDestination;

    public GameObject dialogueCapitain;
    public GameObject MiniGameCapitain;
    void Awake()
    {
        if (PlayerPrefs.HasKey("ReparationPhare") == false)
        {
            PlayerPrefs.SetInt("ReparationPhare", 0);
        }

        //ActiveCapitain();
        ActiveSetOffDialogue();
    }

    void ActiveCapitain()
    {
        if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.Maison))
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

        if(QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 1;
        }
        if(QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.Phare))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 2;
        }
        if(QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.Phare))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 3;
        }
    }
}
