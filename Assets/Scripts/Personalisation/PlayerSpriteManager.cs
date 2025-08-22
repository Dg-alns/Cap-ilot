using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerSpriteManager : MonoBehaviour
{
    public PlayerData playerData;
    public TemplateSavePlayerData TemplateSavePlayer;
    public PositionementPartManager positionementPartManager;
    [SerializeField] Sauvegarde _Sauvegarde;

    private SerializableDictionary<string, TemplateSavePlayerData> _statePlayer;

    public GameObject Corps;
    public GameObject Cheveux;
    public GameObject EyeLeft;
    public GameObject EyeRight;
    public GameObject Haut;
    public GameObject Bas;
    public GameObject Chaussure;

    public GameObject Diabete;

    public TextMeshProUGUI TextQuestTMP; //TODO a modifier pour le mettre dans les scene

    public bool inProfil = false;

    private void Start()
    {
        _statePlayer = _Sauvegarde.StatPlayer;

        if (Diabete != null)
        {
            if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
                Diabete.SetActive(true);
        }



        if (SceneManager.GetActiveScene().name == "Personalisation")
        {
            //if (HaveDateEnter() == false)
            //    return;

            DetectionPlayerDataSprite(PartOfBody.Body, Corps);
            DetectionPlayerDataSprite(PartOfBody.Hair, Cheveux);
            DetectionPlayerDataSprite(PartOfBody.EyesLeft, EyeLeft);
            DetectionPlayerDataSprite(PartOfBody.EyesRight, EyeRight);
            DetectionPlayerDataSprite(PartOfBody.Top, Haut);
            DetectionPlayerDataSprite(PartOfBody.Bottom, Bas);
            DetectionPlayerDataSprite(PartOfBody.Shoes, Chaussure);

            DetectionPlayerDataColor(PartOfBody.Body, Corps);
            DetectionPlayerDataColor(PartOfBody.Hair, Cheveux);
            DetectionPlayerDataColor(PartOfBody.EyesLeft, EyeLeft);
            DetectionPlayerDataColor(PartOfBody.EyesRight, EyeRight);
            DetectionPlayerDataColor(PartOfBody.Top, Haut);
            DetectionPlayerDataColor(PartOfBody.Bottom, Bas);
            DetectionPlayerDataColor(PartOfBody.Shoes, Chaussure);
        }
        else if(inProfil)
        {
            DetectionPlayerDataSprite(PartOfBody.Body, Corps);
            DetectionPlayerDataSprite(PartOfBody.Hair, Cheveux);
            DetectionPlayerDataSprite(PartOfBody.EyesLeft, EyeLeft);
            DetectionPlayerDataSprite(PartOfBody.EyesRight, EyeRight);
            DetectionPlayerDataSprite(PartOfBody.Top, Haut);
            DetectionPlayerDataSprite(PartOfBody.Bottom, Bas);
            DetectionPlayerDataSprite(PartOfBody.Shoes, Chaussure);
            
            DetectionPlayerDataColor(PartOfBody.Body, Corps);
            DetectionPlayerDataColor(PartOfBody.Hair, Cheveux);
            DetectionPlayerDataColor(PartOfBody.EyesLeft, EyeLeft);
            DetectionPlayerDataColor(PartOfBody.EyesRight, EyeRight);
            DetectionPlayerDataColor(PartOfBody.Top, Haut);
            DetectionPlayerDataColor(PartOfBody.Body, Bas);
            DetectionPlayerDataColor(PartOfBody.Shoes, Chaussure);
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
        SavePart(PartOfBody.Body, Corps);
        SavePart(PartOfBody.Hair, Cheveux);
        SavePart(PartOfBody.EyesLeft, EyeLeft);
        SavePart(PartOfBody.EyesRight, EyeRight);
        SavePart(PartOfBody.Top, Haut);
        SavePart(PartOfBody.Bottom, Bas);
        SavePart(PartOfBody.Shoes, Chaussure);

        Saving save = new Saving(_Sauvegarde.journal, _Sauvegarde.profile, _Sauvegarde.questManager, _Sauvegarde.StatMinigame, _statePlayer);
        _Sauvegarde.Save(save);
        

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


    void SavePart(PartOfBody part, GameObject Part)
    {
        playerData.SetPart_Body(part, Part.GetComponent<SpriteRenderer>().sprite, Part.GetComponent<SpriteRenderer>().color);

        string partName = part.ToString();

        if (_statePlayer.ContainsKey(partName))
        {
            _statePlayer[partName].CheckNewPart_Body(Part.GetComponent<SpriteRenderer>().sprite);
            _statePlayer[partName].CheckNewColor(Part.GetComponent<SpriteRenderer>().color);
        }
        else
        {
            _statePlayer[partName] = new TemplateSavePlayerData(Part.GetComponent<SpriteRenderer>().sprite, Part.GetComponent<SpriteRenderer>().color);
        }
    }

    public void DetectionPlayerDataSprite(PartOfBody partOfBody, GameObject go)
    {
        TemplateSavePlayerData Part_Player = _statePlayer[partOfBody.ToString()];

        List<PersoPlayerData> ee = SearchScriptObj.GetLsitScriptObj(partOfBody);

        if (Part_Player == null) {
            go.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(partOfBody);
            return;
        }


        playerData.SetPart_Body(partOfBody, Part_Player.GetSprite(), Part_Player.GetColor());

        go.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(partOfBody);    

        for(int i  = 0; i < ee.Count; i++)
        {
            if (ee[i].sprite == Part_Player.Part_Body)
                positionementPartManager.SetPostion(go, ee[i]);
        }
    }

    public void DetectionPlayerDataColor(PartOfBody partOfBody, GameObject go)
    {
        TemplateSavePlayerData Part_Player = _statePlayer[partOfBody.ToString()];

        if (Part_Player == null)
            go.GetComponent<Image>().color = playerData.GetColor(partOfBody);

        playerData.SetPart_Body(partOfBody, Part_Player.GetSprite(), Part_Player.GetColor());

        go.GetComponent<Image>().color = playerData.GetColor(partOfBody);
    }

    bool HaveDateEnter()
    {
        return playerData.GetSprite(PartOfBody.Body) != null ||
                playerData.GetSprite(PartOfBody.Hair) != null ||
                playerData.GetSprite(PartOfBody.EyesLeft) != null ||
                playerData.GetSprite(PartOfBody.EyesRight) != null ||
                playerData.GetSprite(PartOfBody.Top) != null ||
                playerData.GetSprite(PartOfBody.Bottom) != null ||
                playerData.GetSprite(PartOfBody.Shoes) != null;
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
