using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSpriteManager : MonoBehaviour
{
    public PlayerData playerData;

    public GameObject Corps;
    public GameObject Cheveux;
    public GameObject AccessoirTete;
    public GameObject Haut;
    public GameObject Bas;
    public GameObject Chaussure;

    public GameObject Diabete;

    public TextMeshProUGUI TextQuestTMP; //TODO a modifier pour le mettre dans les scene

    public bool inProfil = false;

    private void Start()
    {
        if (Diabete != null)
        {
            if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
                Diabete.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name == "Personalisation")
        {
            if (HaveDateEnter() == false)
                return;

            Corps.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(PART.Corps);
            Cheveux.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(PART.Cheveux);
            AccessoirTete.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(PART.AccessoirTete);
            Haut.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(PART.Haut);
            Bas.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(PART.Bas);
            Chaussure.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(PART.Chaussure);

            Corps.GetComponent<SpriteRenderer>().color = playerData.GetColor(PART.Corps);
            Cheveux.GetComponent<SpriteRenderer>().color = playerData.GetColor(PART.Cheveux);
            AccessoirTete.GetComponent<SpriteRenderer>().color = playerData.GetColor(PART.AccessoirTete);
            Haut.GetComponent<SpriteRenderer>().color = playerData.GetColor(PART.Haut);
            Bas.GetComponent<SpriteRenderer>().color = playerData.GetColor(PART.Bas);
            Chaussure.GetComponent<SpriteRenderer>().color = playerData.GetColor(PART.Chaussure);
        }
        else if(inProfil)
        {
            Corps.GetComponent<Image>().sprite = playerData.GetSprite(PART.Corps);
            Cheveux.GetComponent<Image>().sprite = playerData.GetSprite(PART.Cheveux);
            AccessoirTete.GetComponent<Image>().sprite = playerData.GetSprite(PART.AccessoirTete);
            Haut.GetComponent<Image>().sprite = playerData.GetSprite(PART.Haut);
            Bas.GetComponent<Image>().sprite = playerData.GetSprite(PART.Bas);
            Chaussure.GetComponent<Image>().sprite = playerData.GetSprite(PART.Chaussure);
        }
        else
        {
            UpdateTextQuest();
        }
    }

    private void Update()
    {

        UpdateTextQuest();

        if (Diabete == null)
            return;

        if (Diabete.activeSelf)
            return;

        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
            Diabete.SetActive(true);

    }

    public void SavePersonalisation()
    {
        playerData.SaveSprite(PART.Corps, Corps.GetComponent<SpriteRenderer>().sprite, Corps.GetComponent<SpriteRenderer>().color);

        playerData.SaveSprite(PART.Cheveux, Cheveux.GetComponent<SpriteRenderer>().sprite, Cheveux.GetComponent<SpriteRenderer>().color);

        playerData.SaveSprite(PART.AccessoirTete, AccessoirTete.GetComponent<SpriteRenderer>().sprite, AccessoirTete.GetComponent<SpriteRenderer>().color);

        playerData.SaveSprite(PART.Haut, Haut.GetComponent<SpriteRenderer>().sprite, Haut.GetComponent<SpriteRenderer>().color);

        playerData.SaveSprite(PART.Bas, Bas.GetComponent<SpriteRenderer>().sprite, Bas.GetComponent<SpriteRenderer>().color);

        playerData.SaveSprite(PART.Chaussure, Chaussure.GetComponent<SpriteRenderer>().sprite, Chaussure.GetComponent<SpriteRenderer>().color);
       
        
        
        //playerData.Cheveux = Cheveux.GetComponent<SpriteRenderer>().sprite;
        //playerData.AccessoirTete = AccessoirTete.GetComponent<SpriteRenderer>().sprite;
        //playerData.Haut = Haut.GetComponent<SpriteRenderer>().sprite;
        //playerData.Bas = Bas.GetComponent<SpriteRenderer>().sprite;
        //playerData.Chaussure = Chaussure.GetComponent<SpriteRenderer>().sprite;

        //playerData.Color_Corps = Corps.GetComponent<SpriteRenderer>().color;
        //playerData.Color_Cheveux = Cheveux.GetComponent<SpriteRenderer>().color;
        //playerData.Color_AccessoirTete = AccessoirTete.GetComponent<SpriteRenderer>().color;
        //playerData.Color_Haut = Haut.GetComponent<SpriteRenderer>().color;
        //playerData.Color_Bas = Bas.GetComponent<SpriteRenderer>().color;
        //playerData.Color_Chaussure = Chaussure.GetComponent<SpriteRenderer>().color;
    }

    bool HaveDateEnter()
    {
        return playerData.GetSprite(PART.Corps) != null ||
                playerData.GetSprite(PART.Cheveux) != null ||
                playerData.GetSprite(PART.AccessoirTete) != null ||
                playerData.GetSprite(PART.Haut) != null ||
                playerData.GetSprite(PART.Bas) != null ||
                playerData.GetSprite(PART.Chaussure) != null;
    }


    void UpdateTextQuest()
    {
        if (TextQuestTMP == null)
            return;

        switch(QuestManager.GetCurrentQuest())
        {
            case (int)QUESTS.Introduction:
                TextQuestTMP.text = "Introduction";
                break;
            case (int)QUESTS.ReparationPhare:
                TextQuestTMP.text = "ReparationPhare";
                break;
            case (int)QUESTS.R_Ressoucre:
                TextQuestTMP.text = "R_Ressoucre";
                break;
            case (int)QUESTS.Relation:
                TextQuestTMP.text = "Relation";
                break;
            case (int)QUESTS.Phare:
                TextQuestTMP.text = "Phare";
                break;
            case (int)QUESTS.Maison:
                TextQuestTMP.text = "Maison";
                break;
            case (int)QUESTS.Hopital:
                TextQuestTMP.text = "Hopital";
                break;
            case (int)QUESTS.Sport:
                TextQuestTMP.text = "Sport";
                break;
            case (int)QUESTS.Ecole:
                TextQuestTMP.text = "Ecole";
                break;
            case (int)QUESTS.Alimentation:
                TextQuestTMP.text = "Alimentation";
                break;
            case (int)QUESTS.S_Ressource:
                TextQuestTMP.text = "S_Ressource";
                break;
            case (int)QUESTS.A_Ressource:
                TextQuestTMP.text = "A_Ressource";
                break;
            case (int)QUESTS.E_Ressource:
                TextQuestTMP.text = "E_Ressource";
                break;
            case (int)QUESTS.DemandeCapitaine:
                TextQuestTMP.text = "DemandeCapitaine";
                break;
        }
    }
}
