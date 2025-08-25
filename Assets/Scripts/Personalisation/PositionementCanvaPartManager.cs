using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PositionementCanvaPartManager : PositionementPartManager
{
    public override void SetPostion(GameObject go, PersoPlayerData persoPlayerData)
    {
        string spePart = DetectonNumberSpeOffPartRevert(persoPlayerData.name);

        List<Vector2> lst = DetectionPositionList(persoPlayerData.part);
        List<Vector2> lstSize = DetectionSizeList(persoPlayerData.part);

        if (lst == null)
            return;

        for (int i = 0; i < lst.Count; i++)
        {
            if (spePart.Equals((i + 1).ToString()))
            {
                go.GetComponent<RectTransform>().localPosition = lst[i];
                go.GetComponent<RectTransform>().localScale = lstSize[i];
                return;
            }
        }
    }
    public override void SetPostionHair(PartOfBody partOfBody, GameObject go, HairData hairData)
    {
        Debug.Log("Position Size " + partOfBody.ToString());
        string spePartFront = "";
        List<Vector2> lstHair = new List<Vector2>();
        List<Vector2> lstSize = new List<Vector2>();

        if (partOfBody == PartOfBody.Hair)
        {
            spePartFront = DetectonNumberSpeOffPart(hairData.sprite.name);
            lstHair = DetectionPositionList(partOfBody);
            lstSize = DetectionSizeList(partOfBody);
        }
        else if (partOfBody == PartOfBody.HairBack)
        {
            spePartFront = DetectonNumberSpeOffPart(hairData.Back.sprite.name);
            lstHair = DetectionPositionList(partOfBody);
            lstSize = DetectionSizeList(partOfBody);
        }

        if (lstHair == null)
            return;

        for (int i = 0; i < lstHair.Count; i++)
        {
            if (spePartFront.Equals((i + 1).ToString()))
            {
                go.GetComponent<RectTransform>().localPosition = lstHair[i];
                go.GetComponent<RectTransform>().localScale = lstSize[i];
                break;
            }
        }
    }

}
