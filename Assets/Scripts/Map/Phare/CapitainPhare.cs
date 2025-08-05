using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitainPhare : MonoBehaviour
{
    public NextSceneDestination sceneDestination;

    public GameObject dialogueCapitain;
    public GameObject MiniGameCapitain;

    int FinishPhare = 0;

    public Port Hopital;
    public Port Alimentation;
    void Awake()
    {
        if (PlayerPrefs.HasKey("ReparationPhare") == false)
        {
            PlayerPrefs.SetInt("ReparationPhare", 0);
            FinishPhare = PlayerPrefs.GetInt("ReparationPhare");
        }
        else
            FinishPhare = PlayerPrefs.GetInt("ReparationPhare");
        
        

        if (sceneDestination.GetPreviousScene().Contains("MiniGame_LightHouse"))
        {
            PlayerPrefs.SetInt("ReparationPhare", 1);
            FinishPhare = PlayerPrefs.GetInt("ReparationPhare");
        }

        ActiveCapitain();
        ActiveSetOffDialogue();
    }

    void ActiveCapitain()
    {
        if(PlayerPrefs.GetInt("ReparationPhare") == 0)
        {
            MiniGameCapitain.SetActive(true);
        }
        else if(PlayerPrefs.GetInt("ReparationPhare") == 1)
        {
            if (QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.ReparationPhare))
            {
                QuestManager.NextQuest();
            }

            dialogueCapitain.SetActive(true);
        }
    }

    void ActiveSetOffDialogue()
    {
        if (dialogueCapitain == null)
            return;

        if(QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 1;
            Hopital.CanGoToIle = true;
        }
        if(QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.Phare))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 2;
            Alimentation.CanGoToIle = true;
        }
        if(QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.Phare))
        {
            dialogueCapitain.GetComponent<NPC>().idxOffSetDialogue = 3;
        }
    }
}
