using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpriteManager : MonoBehaviour
{
    public PlayerData playerData;
    public PositionementPartManager positionementPartManager;
    [SerializeField] Sauvegarde _Sauvegarde;

    private SerializableDictionary<string, TemplateSavePlayerData> _statePlayer;

    public GameObject Corps;
    public GameObject CheveuxFront;
    public GameObject CheveuxBack;
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

        DetectionPlayerDataSprite(PartOfBody.Body, Corps);
        DetectionPlayerDataHairSprite(PartOfBody.Hair, CheveuxFront);
        DetectionPlayerDataHairSprite(PartOfBody.HairBack, CheveuxBack);
        DetectionPlayerDataEyes(PartOfBody.EyesLeft, EyeLeft);
        DetectionPlayerDataSprite(PartOfBody.Top, Haut);
        DetectionPlayerDataSprite(PartOfBody.Bottom, Bas);
        DetectionPlayerDataSprite(PartOfBody.Shoes, Chaussure);

        DetectionPlayerDataColor(PartOfBody.Body, Corps);
        DetectionPlayerDataColor(PartOfBody.Hair, CheveuxFront);
        DetectionPlayerDataColor(PartOfBody.HairBack, CheveuxBack);
        DetectionPlayerDataColor(PartOfBody.Top, Haut);
        DetectionPlayerDataColor(PartOfBody.Bottom, Bas);
        DetectionPlayerDataColor(PartOfBody.Shoes, Chaussure);

        if (inProfil == false) {
            SpriteRenderer Body = Corps.GetComponent<SpriteRenderer>();
            SpriteRenderer Top = Haut.GetComponent<SpriteRenderer>();
            SpriteRenderer Bottom = Bas.GetComponent<SpriteRenderer>();
            SpriteRenderer Shoes = Chaussure.GetComponent<SpriteRenderer>();

            GetComponent<AnimationAvatarManager>().Init(Body, Bottom, Top, Shoes);
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
        SavePart(PartOfBody.Hair, CheveuxFront);
        SavePart(PartOfBody.HairBack, CheveuxBack);
        SavePart(PartOfBody.EyesLeft, EyeLeft);
        SavePart(PartOfBody.Top, Haut);
        SavePart(PartOfBody.Bottom, Bas);
        SavePart(PartOfBody.Shoes, Chaussure);

        Saving save = new Saving(_Sauvegarde.journal, _Sauvegarde.profile, _Sauvegarde.questManager, _Sauvegarde.StatMinigame, _statePlayer);
        _Sauvegarde.Save(save);
    }


    void SavePart(PartOfBody part, GameObject Part)
    {
        string nameSprite;
        Color color;

        if (part == PartOfBody.HairBack)
        {
            if (Part.GetComponent<SpriteRenderer>().sprite != null)
            {
                nameSprite = Part.GetComponent<SpriteRenderer>().sprite.name;
                color = Part.GetComponent<SpriteRenderer>().color;
            }

            else
            {
                nameSprite = null;
                color = Color.clear;
            }

        }
        else
        {
            nameSprite = Part.GetComponent<SpriteRenderer>().sprite.name;
            color = Part.GetComponent<SpriteRenderer>().color;
        }

        string partName = part.ToString();

        if (_statePlayer.ContainsKey(partName))
        {
            _statePlayer[partName].CheckNewPart_Body(nameSprite);
            _statePlayer[partName].CheckNewColor(Part.GetComponent<SpriteRenderer>().color);
        }
        else
        {
            _statePlayer[partName] = new TemplateSavePlayerData(nameSprite, color);
        }
    }

    public void DetectionPlayerDataSprite(PartOfBody partOfBody, GameObject go) 
    {
        if (_statePlayer.ContainsKey(partOfBody.ToString()) == false)
            return;

        TemplateSavePlayerData Part_Player = _statePlayer[partOfBody.ToString()];
        List<PersoPlayerData> AllPlayerPart = SearchScriptObj.GetLsitScriptObj(partOfBody);

        playerData.SetPart_Body(partOfBody, Part_Player.GetSpriteName(), Part_Player.GetColor());


        for (int i = 0; i < AllPlayerPart.Count; i++)
        {
            if (AllPlayerPart[i].sprite.name == Part_Player.Name_Part_Body)
                positionementPartManager.SetPostion(go, AllPlayerPart[i]);
        }

        if (go.GetComponent<SpriteRenderer>())
            go.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(partOfBody);
        
        else if (go.GetComponent<Image>())
            go.GetComponent<Image>().sprite = playerData.GetSprite(partOfBody);
        


    }

    public void DetectionPlayerDataEyes(PartOfBody partOfBody, GameObject go) 
    {
        if (_statePlayer.ContainsKey(partOfBody.ToString()) == false)
            return;

        TemplateSavePlayerData Part_Player = _statePlayer[partOfBody.ToString()];

        List<PersoPlayerData> AllPlayerPart = SearchScriptObj.GetLsitScriptObj(partOfBody);
        List<PersoPlayerData> EyeRightLst = SearchScriptObj.GetLsitScriptObj(PartOfBody.EyesRight);

        playerData.SetPart_Body(partOfBody, Part_Player.GetSpriteName(), Part_Player.GetColor());

        for(int i = 0; i < AllPlayerPart.Count; i++)
        {
            if (AllPlayerPart[i].sprite.name == Part_Player.Name_Part_Body)
                playerData.SetPart_Body(PartOfBody.EyesRight, EyeRightLst[i].sprite.name, Part_Player.GetColor());
        }

        if (go.GetComponent<SpriteRenderer>())
        {
            go.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(partOfBody);
            EyeRight.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(PartOfBody.EyesRight);
        }
        else
        {
            go.GetComponent<Image>().sprite = playerData.GetSprite(partOfBody);
            EyeRight.GetComponent<Image>().sprite = playerData.GetSprite(PartOfBody.EyesRight);
        }
    }

    public void DetectionPlayerDataHairSprite(PartOfBody partOfBody, GameObject go) 
    {
        if (_statePlayer.ContainsKey(partOfBody.ToString()) == false)
            return;


        TemplateSavePlayerData Part_Player = _statePlayer[partOfBody.ToString()];

        List<HairData> AllHairData = SearchScriptObj.GetLstHairObj();

        playerData.SetPart_Body(partOfBody, Part_Player.GetSpriteName(), Part_Player.GetColor());

        for (int i = 0; i < AllHairData.Count; i++)
        {
            if (partOfBody == PartOfBody.Hair)
            {
                if (AllHairData[i].sprite.name == Part_Player.Name_Part_Body)
                    positionementPartManager.SetPostionHair(partOfBody, go, AllHairData[i]);
            }
            else if (partOfBody == PartOfBody.HairBack)
            {
                if (AllHairData[i].Back == null)
                    continue;

                if (AllHairData[i].Back.sprite.name == Part_Player.Name_Part_Body)
                    positionementPartManager.SetPostionHair(partOfBody, go, AllHairData[i]);
            }
        }

        if (go.GetComponent<SpriteRenderer>())
            go.GetComponent<SpriteRenderer>().sprite = playerData.GetSprite(partOfBody);
        else if (go.GetComponent<Image>())
            go.GetComponent<Image>().sprite = playerData.GetSprite(partOfBody);    


    }

    public void DetectionPlayerDataColor(PartOfBody partOfBody, GameObject go)
    {
        if (_statePlayer.ContainsKey(partOfBody.ToString()) == false)
            return;

        TemplateSavePlayerData Part_Player = _statePlayer[partOfBody.ToString()];
        playerData.SetPart_Body(partOfBody, Part_Player.GetSpriteName(), Part_Player.GetColor());


        if (go.GetComponent<SpriteRenderer>())
        {
            if (playerData.GetSprite(partOfBody) != null)
                go.GetComponent<SpriteRenderer>().color = playerData.GetColor(partOfBody);
            else
                go.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        else if (go.GetComponent<Image>())
        {
            if (playerData.GetSprite(partOfBody) != null)
                go.GetComponent<Image>().color = playerData.GetColor(partOfBody);
            else
                go.GetComponent<Image>().color = Color.clear;
        }
    }

    bool HaveDateEnter()
    {
        return playerData.GetSprite(PartOfBody.Body) != null ||
                playerData.GetSprite(PartOfBody.HairBack) != null ||
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
