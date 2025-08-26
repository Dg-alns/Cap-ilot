using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    private static readonly HashSet<char> nb = new HashSet<char>
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

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
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < name.Length; i++)
        {
            if (nb.Contains(name[i]))
                result.Append(name[i]);
            else
                break;
        }

        return result.ToString();
    }

    protected string DetectonNumberSpeOffPartRevert(string name)
    {
        StringBuilder result = new StringBuilder();

        for (int i = name.Length - 1; i >= 0; i--)
        {
            if (nb.Contains(name[i]))
                result.Insert(0, name[i]);
            else
                break;
        }

        return result.ToString();
    }

    protected bool DeteIdxChiffre(char value)
    {
        for (int i = 0; i < 10 ;i++)
        {
            if(value.Equals(char.Parse(i.ToString())))
                return true;
        }
        return false;
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
