using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionementPartManager : MonoBehaviour
{
    [SerializeField] private List<Vector2> AllPostionTop;

    [SerializeField] private List<Vector2> AllPostionBottom;

    [SerializeField] private List<Vector2> AllPostionShoes;

    public void SetPostion(GameObject go, PersoPlayerData persoPlayerData)
    {
        char spePArt = DetectonNumberSpeOffPart(persoPlayerData.name);

        List<Vector2> lst = DetectionList(persoPlayerData.part);

        for (int i = 0; i < lst.Count; i++)
        {
            if (spePArt.Equals(char.Parse((i+1).ToString())))
            {
                go.transform.localPosition = lst[i];
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
        }

        return null;
    }
}
