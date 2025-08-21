using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Personalisation : MonoBehaviour
{
    public PagesManagement pagesManagement;
    public TextMeshProUGUI PageOfAccesoires;
    public Sprite cadena;

    public GameObject PrefabEyes;

    int nbMaxpagesOfCurrentLst;
    int idxcurrentpages;
    int sizeGrid;
    int idxcurrentBody = -1;
    int idxcurrentEyes = -1;

    Dictionary<PartOfBody, int> nbPagesOfAccesoires = new Dictionary<PartOfBody, int>();
    List<Color> listOfAllColors = new List<Color>();

    List<GameObject> listOfGridPersonalisation;
    List<GameObject> GridAllColors;

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
        PageOfAccesoires.text = $"{idxcurrentpages} / {nbMaxpagesOfCurrentLst}";
    }

    List<PersoPlayerData> GetLsitScriptObj(PartOfBody part)
    {
        switch (part)
        {
            case PartOfBody.Hair:
                return SearchScriptObj.HairSObj;

            case PartOfBody.Body:
                return SearchScriptObj.BodySObj;

            case PartOfBody.EyesLeft:
                return SearchScriptObj.EyesLeftObj;

            case PartOfBody.EyesRight:
                return SearchScriptObj.EyesRightObj;

            case PartOfBody.Top:
                return SearchScriptObj.TopSObj;

            case PartOfBody.Bottom:
                return SearchScriptObj.BottomSObj;

            case PartOfBody.Shoes:
                return SearchScriptObj.ShoesSObj;
        }

        return null;
    }

    int OffsetOfidxLst()
    {
        if (idxcurrentpages >= 1 && idxcurrentpages <= nbMaxpagesOfCurrentLst)
            return (idxcurrentpages - 1) * sizeGrid;

        return -1;
    }

    public void UpdateGrillPersonalisation()
    {
        int offset = OffsetOfidxLst();

        if (offset < 0)
            return;

        for(int i = 0; i < listOfGridPersonalisation.Count; i++)
        {
            if(i + offset >= GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage)).Count)
            {
                listOfGridPersonalisation[i].GetComponent<Image>().color = Color.clear;
                continue;
            }

            Sprite sprite = GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].sprite;
            listOfGridPersonalisation[i].GetComponent<Image>().sprite = sprite;

            if (GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].IsDebloquer())
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
        if (offset < 0)
            return;
        if (gameObject.GetComponent<Image>().sprite != null)
        {
            for (int i = 0; i < listOfGridPersonalisation.Count; i++) {
                if (gameObject.name.Contains((i+1).ToString()))
                {
                    if (GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].IsDebloquer())
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
        if(idxcurrentpages < nbMaxpagesOfCurrentLst)
        {
            idxcurrentpages++;
            UpdateGrillPersonalisation();
            UpdateText();
        }
    }

    public void PreviousPageOfCustoPart()
    {
        if (idxcurrentpages > 1)
        {
            idxcurrentpages--;
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
        idxcurrentpages = 1;
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

    public void NextBody()
    {
        List<PersoPlayerData> AllBody =  GetLsitScriptObj(PartOfBody.Body);
        GameObject go = Playerpart.GetPartOfPlayer(PartOfBody.Body.ToString());

        for(int i = 0; i < AllBody.Count;i++)
        {
            if (AllBody[i].sprite == go.GetComponent<SpriteRenderer>().sprite)
                idxcurrentBody = i;
        }

        int nextBody = (idxcurrentBody + 1) < AllBody.Count ? (idxcurrentBody + 1) : 0;

        PersoPlayerData data = AllBody[nextBody];


        if (go.GetComponent<SpriteRenderer>().sprite != data.sprite)
        {
            go.GetComponent<SpriteRenderer>().sprite = data.sprite;
            idxcurrentBody = nextBody;
        }
    }

    public void NextEye()
    {
        List<PersoPlayerData> AllEyesLeft =  GetLsitScriptObj(PartOfBody.EyesLeft);
        GameObject Left = Playerpart.GetPartOfPlayer(PartOfBody.EyesLeft.ToString());

        List<PersoPlayerData> AllEyesRight =  GetLsitScriptObj(PartOfBody.EyesRight);
        GameObject Right = Playerpart.GetPartOfPlayer(PartOfBody.EyesRight.ToString());

        for(int i = 0; i < AllEyesLeft.Count;i++)
        {
            if (AllEyesLeft[i].sprite == Left.GetComponent<SpriteRenderer>().sprite)
                idxcurrentEyes = i;
        }

        int nextBody = (idxcurrentEyes + 1) < AllEyesLeft.Count ? (idxcurrentEyes + 1) : 0;

        PersoPlayerData dataLeft = AllEyesLeft[nextBody];
        PersoPlayerData dataRight = AllEyesLeft[nextBody];


        if (Left.GetComponent<SpriteRenderer>().sprite != dataLeft.sprite)
        {
            Left.GetComponent<SpriteRenderer>().sprite = dataLeft.sprite;
            Right.GetComponent<SpriteRenderer>().sprite = dataRight.sprite;
            idxcurrentEyes = nextBody;
        }
    }
}
