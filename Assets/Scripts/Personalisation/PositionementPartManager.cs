using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionementPartManager : MonoBehaviour
{
    [SerializeField] private List<Vector2> AllPostionTop;

    [SerializeField] private List<Vector2> AllPostionBottom;

    [SerializeField] private List<Vector2> AllPostionShoes;

    [SerializeField] private List<Vector2> AllPostionHairFront;
    [SerializeField] private List<Vector2> AllPostionHairBack;

    public void SetPostion(GameObject go, PersoPlayerData persoPlayerData)
    {
        char spePArt = DetectonNumberSpeOffPart(persoPlayerData.name);

        List<Vector2> lst = DetectionList(persoPlayerData.part);

        if (lst == null)
            return;

        for (int i = 0; i < lst.Count; i++)
        {
            if (spePArt.Equals(char.Parse((i+1).ToString())))
            {
                go.transform.localPosition = lst[i];
                return;
            }
        }
    }
    public void SetPostionHair(GameObject go, HairData hairData)
    {
        char spePArtFront = DetectonNumberSpeOffPart(hairData.name);

        List<Vector2> lstFront = DetectionList(hairData.part);

        if (lstFront == null)
            return;

        for (int i = 0; i < lstFront.Count; i++)
        {
            if (spePArtFront.Equals(char.Parse((i+1).ToString()))) // AVoir
            {
                go.transform.localPosition = lstFront[i];
                return;
            }
        }

        if (hairData.Back == null)
            return;

        char spePArtBack = DetectonNumberSpeOffPart(hairData.Back.name);

        List<Vector2> lstBack = DetectionList(hairData.Back.part);

        if (lstBack == null)
            return;
        for (int i = 0; i < lstBack.Count; i++)
        {
            if (spePArtBack.Equals(char.Parse((i+1).ToString())))
            {
                go.transform.localPosition = lstBack[i];
                return;
            }
        }
    }

    char DetectonNumberSpeOffPart(string name)
    {
        return name[name.Length - 1];
    }


    List<Vector2> DetectionList(PartOfBody partOfBody)
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
}
