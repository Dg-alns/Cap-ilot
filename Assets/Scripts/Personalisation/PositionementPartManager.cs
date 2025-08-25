using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PositionementPartManager : MonoBehaviour
{
    [SerializeField] private List<Vector2> AllPostionTop;
    [SerializeField] private List<Vector2> AllSizeTop;

    [SerializeField] private List<Vector2> AllPostionBottom;
    [SerializeField] private List<Vector2> AllSizeBottom;

    [SerializeField] private List<Vector2> AllPostionShoes;
    [SerializeField] private List<Vector2> AllSizeShoes;

    [SerializeField] private List<Vector2> AllPostionHairFront;
    [SerializeField] private List<Vector2> AllSizeHairFront;

    [SerializeField] private List<Vector2> AllPostionHairBack;
    [SerializeField] private List<Vector2> AllSizeHairBack;

    public virtual void SetPostion(GameObject go, PersoPlayerData persoPlayerData)
    {
        string spePart = DetectonNumberSpeOffPartRevert(persoPlayerData.name);

        List<Vector2> lst = DetectionPositionList(persoPlayerData.part);

        if (lst == null)
            return;

        for (int i = 0; i < lst.Count; i++)
        {
            if (spePart.Equals((i + 1).ToString()))
            {
                go.transform.localPosition = lst[i];
                return;
            }
        }
    }
    public virtual void SetPostionHair(PartOfBody partOfBody, GameObject go, HairData hairData)
    {
        string spePartFront = "";
        List<Vector2> lstFront = new List<Vector2>();

        if (partOfBody == PartOfBody.Hair)
        {
            spePartFront = DetectonNumberSpeOffPart(hairData.sprite.name);
            lstFront = DetectionPositionList(partOfBody);
        }
        else if (partOfBody == PartOfBody.HairBack)
        {
            spePartFront = DetectonNumberSpeOffPart(hairData.Back.sprite.name);
            lstFront = DetectionPositionList(partOfBody);
        }

        if (lstFront == null)
            return;

        for (int i = 0; i < lstFront.Count; i++)
        {
            if (spePartFront.Equals((i + 1).ToString()))
            {
                go.transform.localPosition = lstFront[i];
                break;
            }
        }
    }

    protected string DetectonNumberSpeOffPart(string name)
    {
        string result = "";

        for (int i = 0; i < name.Length; i++)
        {
            if (DeteIdxChiffre(name[i]) >= 0)
                result += name[i];
            else
                break;
        }

        return result;
    }

    protected string DetectonNumberSpeOffPartRevert(string name)
    {
        string result = "";

        for (int i = name.Length - 1; i >= 0; i--)
        {
            if (DeteIdxChiffre(name[i]) >= 0)
                result = name[i] + result;
            else
                break;
        }

        return result;
    }

    protected int DeteIdxChiffre(char value)
    {
        List<int> list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0;i < list.Count;i++)
        {
            if(value.Equals(char.Parse(i.ToString())))
                return i;
        }
        return -1;
    }


    protected List<Vector2> DetectionPositionList(PartOfBody partOfBody)
    {
        switch(partOfBody)
        {
            case PartOfBody.Top:
                return AllPostionTop;
            case PartOfBody.Bottom:
                return AllPostionBottom;
            case PartOfBody.Shoes:
                return AllPostionShoes;
            case PartOfBody.HairBack:
                return AllPostionHairBack;
            case PartOfBody.Hair:
                return AllPostionHairFront;
        }

        return null;
    }


    protected List<Vector2> DetectionSizeList(PartOfBody partOfBody)
    {
        switch(partOfBody)
        {
            case PartOfBody.Top:
                return AllSizeTop;
            case PartOfBody.Bottom:
                return AllSizeBottom;
            case PartOfBody.Shoes:
                return AllSizeShoes;
            case PartOfBody.HairBack:
                return AllSizeHairBack;
            case PartOfBody.Hair:
                return AllSizeHairFront;
        }

        return null;
    }
}
