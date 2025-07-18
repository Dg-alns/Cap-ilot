using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Personalisation : MonoBehaviour
{
    //public SearchScriptObj search;
    //public PersoPlayerData playerData;

    public PagesManagement pagesManagement;

    public TextMeshProUGUI PageOfAccesoires;

    public Sprite cadena;

    int nbMaxpagesOfCurrentLst;
    int currentpages;

    Dictionary<PartOfBody, int> nbPagesOfAccesoires = new Dictionary<PartOfBody, int>();

    List<GameObject> listOfGridPersonalisation;

    List<Color> listOfAllColors = new List<Color>();

    List<GameObject> GridAllColors;


    // Arm
    List<PersoPlayerData> ScriptsHaut = SearchScriptObj.HautSObj;
    List<PersoPlayerData> ScriptsHand = SearchScriptObj.HandSObj;
    List<PersoPlayerData> ScriptsShoulder = SearchScriptObj.ShoulderSObj;


    List<PersoPlayerData> ScriptsChest = SearchScriptObj.CorpsSObj;

    // Face
    //List<ScriptableObject> ScriptsEyebrows = SearchScriptObj.EyebrowsSObj;
    List<PersoPlayerData> ScriptsAccTete = SearchScriptObj.AccTeteSObj;
    List<PersoPlayerData> ScriptsHair = SearchScriptObj.HairSObj;
    List<PersoPlayerData> ScriptsMouse = SearchScriptObj.MouseSObj;
    List<PersoPlayerData> ScriptsNose = SearchScriptObj.NoseSObj;


    //Leg
    List<PersoPlayerData> ScriptsShoes = SearchScriptObj.ShoesSObj;
    List<PersoPlayerData> ScriptsThigh = SearchScriptObj.ThighSObj;
    List<PersoPlayerData> ScriptsBas = SearchScriptObj.BasSObj;


    int sizeGrid;


    void Awake()
    {
        listOfGridPersonalisation = Tools.CreateGameObjectList<Button>("Selections");
        GridAllColors = Tools.CreateGameObjectList<Button>("Colors");

        sizeGrid = listOfGridPersonalisation.Count;

    }

    public void LoadSecondPArt()
    {
        ChangeOfAnyPart();

        CreateLstAllColor();
        InitColorPosition();
    }

    void UpdateText()
    {
        PageOfAccesoires.text = $"{currentpages} / {nbMaxpagesOfCurrentLst}";
    }


    //List<PersoPlayerData> GetLsitScriptObj(PartOfBody part)
    //{
    //    switch(part)
    //    {
    //        case PartOfBody.Hair:
    //            return ScriptsHair;

    //        case PartOfBody.Chest:
    //            return ScriptsChest;

    //        case PartOfBody.Eyes:
    //            return ScriptsEyes;

    //        case PartOfBody.Mouse:
    //            return ScriptsMouse;

    //        case PartOfBody.Nose:
    //            return ScriptsNose;

    //        case PartOfBody.L_Arm:
    //            return ScriptsArm;

    //        case PartOfBody.R_Arm:
    //            return ScriptsArm;

    //        case PartOfBody.L_Foot:
    //            return ScriptsFoot;

    //        case PartOfBody.R_Foot:
    //            return ScriptsFoot;

    //        case PartOfBody.L_Thigh:
    //            return ScriptsThigh;

    //        case PartOfBody.R_Thigh:
    //            return ScriptsThigh;

    //        case PartOfBody.L_Hand:
    //            return ScriptsHand;

    //        case PartOfBody.R_Hand:
    //            return ScriptsHand;

    //        case PartOfBody.L_Leg:
    //            return ScriptsLeg;

    //        case PartOfBody.R_Leg:
    //            return ScriptsLeg;

    //        case PartOfBody.L_Shoulder:
    //            return ScriptsShoulder;

    //        case PartOfBody.R_Shoulder:
    //            return ScriptsShoulder;
    //    }

    //    return null;
    //}

    List<PersoPlayerData> GetLsitScriptObj(PartOfBody part)
    {
        switch (part)
        {
            case PartOfBody.Hair:
                return ScriptsHair;

            case PartOfBody.Chest:
                return ScriptsChest;

            case PartOfBody.AccTete:
                return ScriptsAccTete;

            case PartOfBody.Haut:
                return ScriptsHaut;

            case PartOfBody.Bas:
                return ScriptsBas;
        }

        return null;
    }

    int OffsetOfidxLst()
    {
        if (currentpages >= 1 && currentpages <= nbMaxpagesOfCurrentLst)
            return (currentpages - 1) * sizeGrid;

        return -1;
    }

    public void UpdateGrillPersonalisation()
    {
        int offset = OffsetOfidxLst();

        for(int i = 0; i < listOfGridPersonalisation.Count; i++)
        {
            if(i + offset >= GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage)).Count)
            {
                listOfGridPersonalisation[i].GetComponent<Image>().color = Color.clear;
                continue;
            }

            Sprite sprite = GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].sprite;
            listOfGridPersonalisation[i].GetComponent<Image>().sprite = sprite;

            if (GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].isDebloquer)
            {
                listOfGridPersonalisation[i].GetComponent<Image>().color = Color.white;
            }
            else
                listOfGridPersonalisation[i].GetComponent<Image>().sprite = cadena;
        }
    }

    public void ChangePerso(GameObject gameObject)
    {
        int offset = OffsetOfidxLst();
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            for (int i = 0; i < listOfGridPersonalisation.Count; i++) {
                if (gameObject.name.Contains((i+1).ToString()))
                {
                    if (GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].isDebloquer)
                    {
                        GameObject go = Playerpart.GetPartOfPlayer(pagesManagement.CurrentPage);

                        if (go.GetComponent<SpriteRenderer>().sprite != gameObject.GetComponent<Image>().sprite)
                        {
                            go.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Image>().sprite;
                            go.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                }
            }
        }
            
    }

    public void Remove()
    {
        GameObject go = Playerpart.GetPartOfPlayer(pagesManagement.CurrentPage);
        go.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void NextPageOfCustoPart()   
    {
        if(currentpages < nbMaxpagesOfCurrentLst)
        {
            currentpages++;
            UpdateGrillPersonalisation();
            UpdateText();
        }
    }

    public void PreviousPageOfCustoPart()
    {
        if (currentpages > 1)
        {
            currentpages--;
            UpdateGrillPersonalisation();
            UpdateText();
        }
    }

    public void SelectColor(GameObject gameObject)
    {
        GameObject go = Playerpart.GetPartOfPlayer(pagesManagement.CurrentPage);

        if (go.GetComponent<SpriteRenderer>().color != gameObject.GetComponent<Image>().color)
            go.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<Image>().color;  
    }

    public void NextColor()
    {
        GridAllColors[0].GetComponent<Image>().color = GridAllColors[1].GetComponent<Image>().color;
        GridAllColors[1].GetComponent<Image>().color = GridAllColors[2].GetComponent<Image>().color;
        GridAllColors[2].GetComponent<Image>().color = GridAllColors[3].GetComponent<Image>().color;
        GridAllColors[3].GetComponent<Image>().color = GridAllColors[4].GetComponent<Image>().color;

        for (int i = 0; i < listOfAllColors.Count; i++)
        {
            if (listOfAllColors[i] == GridAllColors[4].GetComponent<Image>().color)
            {
                if (i + 1 < listOfAllColors.Count)
                    GridAllColors[4].GetComponent<Image>().color = listOfAllColors[i + 1];
                else
                    GridAllColors[4].GetComponent<Image>().color = listOfAllColors[0];

                break;
            }
        }

    }



    int CalculateNbPageForCustoPart()
    {
        int nbAccessoires = GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage)).Count;

        int nbLst = 0;
        while (true)
        {
            if (nbAccessoires <= 0)
                break; 

            else if (nbAccessoires < sizeGrid)
            {
                nbLst++;
                break;
            }


            nbAccessoires -= sizeGrid;
            nbLst++;
        }
        nbPagesOfAccesoires.Add(Playerpart.DetectionOfPart(pagesManagement.CurrentPage), nbLst);

        return nbLst;
    }

    int FindSpeNbPageOfAccessoires()
    {
        foreach (var part in nbPagesOfAccesoires)   
        {
            if(part.Key == Playerpart.DetectionOfPart(pagesManagement.CurrentPage))
                return part.Value;
        }

        return CalculateNbPageForCustoPart();
    }

    public void ChangeOfAnyPart()
    {
        nbMaxpagesOfCurrentLst = FindSpeNbPageOfAccessoires();
        currentpages = 1;
        UpdateText();
    }


    void AddColor(Color color)
    {
        listOfAllColors.Add(color);
    }


    void CreateLstAllColor()
    {
        AddColor(Color.red);
        AddColor(Color.blue);
        AddColor(Color.green);
        AddColor(Color.cyan);
        AddColor(Color.black);
        AddColor(Color.gray);
        AddColor(Color.yellow);
        AddColor(Color.magenta);
    }

    void InitColorPosition()
    {
        for (int i = 0; i < GridAllColors.Count; i++)
        {
            GridAllColors[i].GetComponent<Image>().color = listOfAllColors[i];
        }
    }
}
