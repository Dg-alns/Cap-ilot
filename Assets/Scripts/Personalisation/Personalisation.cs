using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Personalisation : MonoBehaviour
{
    public PagesManagement pagesManagement;
    public PositionementPartManager positionementPartManager;
    public TextMeshProUGUI PageOfAccesoires;
    public Sprite cadena;

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

        if (pagesManagement.CurrentPage.Equals(PartOfBody.Hair.ToString()))
        {
            UpdateHair(offset);
            return;
        }

        for(int i = 0; i < listOfGridPersonalisation.Count; i++)
        {
            listOfGridPersonalisation[i].GetComponentInChildren<HairPlayerCanvaManager>().SetColorVisual(new Color(1, 1, 1, 0));

            if (i + offset >= SearchScriptObj.GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage)).Count)
            {
                listOfGridPersonalisation[i].GetComponent<Image>().color = Color.clear;
                continue;
            }

            Sprite sprite = SearchScriptObj.GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].sprite;
            listOfGridPersonalisation[i].GetComponent<Image>().sprite = sprite;

            if (SearchScriptObj.GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].IsDebloquer())
            {
                listOfGridPersonalisation[i].GetComponent<Image>().color = Color.white;
            }
            else
                listOfGridPersonalisation[i].GetComponent<Image>().sprite = cadena;
        }
    }

    void UpdateHair(int offset)
    {
        for (int i = 0; i < listOfGridPersonalisation.Count; i++)
        {
            listOfGridPersonalisation[i].GetComponent<Image>().color = Color.clear;

            if (i + offset >= SearchScriptObj.GetLstHairObj().Count)
            {
                listOfGridPersonalisation[i].GetComponentInChildren<HairPlayerCanvaManager>().SetColorVisual(new Color(1, 1, 1, 0));
                continue;
            }

            listOfGridPersonalisation[i].GetComponentInChildren<HairPlayerCanvaManager>().SetColorVisual(new Color(1, 1, 1, 1));
            HairData hair = SearchScriptObj.GetLstHairObj()[i + offset];

            HairPlayerCanvaManager Head = listOfGridPersonalisation[i].GetComponentInChildren<HairPlayerCanvaManager>();


            if (hair.Back == null)
                ShowFrontHair(Head, hair, i);
            else
                ShowFrontBackHair(Head, hair, i);
        }
    }

    void ShowFrontHair(HairPlayerCanvaManager Head, HairData hair, int i)
    {
        if (hair.IsDebloquer())
        {
            Head.InitHair(hair.sprite);
        }
        else
        {
            listOfGridPersonalisation[i].GetComponent<Image>().sprite = cadena;
            listOfGridPersonalisation[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }
    void ShowFrontBackHair(HairPlayerCanvaManager Head, HairData hair, int i)
    {
        if (hair.IsDebloquer() && hair.Back.IsDebloquer())
        {
            Head.InitHair(hair.sprite, hair.Back.sprite);
        }
        else
        {
            listOfGridPersonalisation[i].GetComponent<Image>().sprite = cadena;
            listOfGridPersonalisation[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void ChangePerso(GameObject gameObject) // TODO Diego Gere les null en back
    {
        int offset = OffsetOfidxLst();

        if (offset < 0)
            return;

        if (pagesManagement.CurrentPage.Equals(PartOfBody.Hair.ToString()))
        {
            ChangeHair(offset, gameObject);
            return;
        }

        if (gameObject.GetComponent<Image>().sprite != null)
        {
            for (int i = 0; i < listOfGridPersonalisation.Count; i++) {
                if (gameObject.name.Contains((i+1).ToString()))
                {
                    if (listOfGridPersonalisation[i].GetComponent<Image>().color == Color.clear)
                        return;


                    if (SearchScriptObj.GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset].IsDebloquer())
                    {
                        GameObject go = Playerpart.GetPartOfPlayer(pagesManagement.CurrentPage);

                        if (go.GetComponent<SpriteRenderer>().sprite != gameObject.GetComponent<Image>().sprite)
                        {
                            go.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Image>().sprite;
                            go.GetComponent<SpriteRenderer>().color = Color.white;
                            positionementPartManager.SetPostion(go, SearchScriptObj.GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage))[i + offset]);
                        }
                    }
                }
            }
        }
            
    }

    public void ChangeHair(int offset, GameObject gameObject)
    {
        HairPlayerCanvaManager ee = gameObject.GetComponentInChildren<HairPlayerCanvaManager>();

        for (int i = 0; i < listOfGridPersonalisation.Count; i++) {
            if (gameObject.name.Contains((i+1).ToString()))
            {
                HairData hair = SearchScriptObj.GetLstHairObj()[i + offset];

                if (hair.Back == null)
                    ChangeFrontHair(ee, offset, i);
                else
                    ChangeFrontBackHair(ee, offset, i);
            }
        }   
    }


    void ChangeFrontBackHair(HairPlayerCanvaManager ee, int offset, int i)
    {
        if (SearchScriptObj.GetLstHairObj()[i + offset].IsDebloquer() && SearchScriptObj.GetLstHairObj()[i + offset].Back.IsDebloquer())
        {
            Dictionary<PartOfBody, GameObject> HairPlayer = Playerpart.GetHairs();

            if (HairPlayer[PartOfBody.Hair].GetComponent<SpriteRenderer>().sprite != ee.FrontHair.sprite || HairPlayer[PartOfBody.HairBack].GetComponent<SpriteRenderer>().sprite != ee.BackHair.sprite)
            {
                HairPlayer[PartOfBody.Hair].GetComponent<SpriteRenderer>().sprite = ee.FrontHair.sprite;
                HairPlayer[PartOfBody.HairBack].GetComponent<SpriteRenderer>().sprite = ee.BackHair.sprite;

                HairPlayer[PartOfBody.Hair].GetComponent<SpriteRenderer>().color = Color.white;
                HairPlayer[PartOfBody.HairBack].GetComponent<SpriteRenderer>().color = Color.white;

                positionementPartManager.SetPostionHair(HairPlayer[PartOfBody.Hair], SearchScriptObj.GetLstHairObj()[i + offset]);
            }
        }
    }

    void ChangeFrontHair(HairPlayerCanvaManager ee, int offset, int i)
    {
        if (SearchScriptObj.GetLstHairObj()[i + offset].IsDebloquer())
        {
            Dictionary<PartOfBody, GameObject> HairPlayer = Playerpart.GetHairs();

            if (HairPlayer[PartOfBody.Hair].GetComponent<SpriteRenderer>().sprite != ee.FrontHair.sprite)
            {
                HairPlayer[PartOfBody.Hair].GetComponent<SpriteRenderer>().sprite = ee.FrontHair.sprite;
                HairPlayer[PartOfBody.Hair].GetComponent<SpriteRenderer>().color = Color.white;

                HairPlayer[PartOfBody.HairBack].GetComponent<SpriteRenderer>().color = Color.clear;

                positionementPartManager.SetPostionHair(HairPlayer[PartOfBody.Hair], SearchScriptObj.GetLstHairObj()[i + offset]);
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
        int nbAccessoires = 0;

        if (pagesManagement.CurrentPage.Equals(PartOfBody.Hair.ToString())) 
            nbAccessoires = SearchScriptObj.GetLstHairObj().Count;
        else
            nbAccessoires = SearchScriptObj.GetLsitScriptObj(Playerpart.DetectionOfPart(pagesManagement.CurrentPage)).Count;

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
        AddColor(Color.white);
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
        List<PersoPlayerData> AllBody =  SearchScriptObj.GetLsitScriptObj(PartOfBody.Body);
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
        List<PersoPlayerData> AllEyesLeft =  SearchScriptObj.GetLsitScriptObj(PartOfBody.EyesLeft);
        GameObject Left = Playerpart.GetPartOfPlayer(PartOfBody.EyesLeft.ToString());

        List<PersoPlayerData> AllEyesRight =  SearchScriptObj.GetLsitScriptObj(PartOfBody.EyesRight);
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
