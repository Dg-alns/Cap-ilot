using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairPlayerCanvaManager : MonoBehaviour
{
    public PositionementCanvaPartManager positionementCanvaPartManager;

    public GameObject FrontHair;
    public GameObject BackHair;
    public GameObject Body;
    public GameObject Top;

    public void InitHair(HairData hairData)
    {
        FrontHair.GetComponent<Image>().sprite = hairData.sprite;
        BackHair.GetComponent<Image>().color = Color.clear;
        positionementCanvaPartManager.SetPostionHair(PartOfBody.Hair, FrontHair, hairData);

        if (hairData.Back != null)
        {
            BackHair.GetComponent<Image>().sprite = hairData.Back.sprite;
            BackHair.GetComponent<Image>().color = Color.white;
            positionementCanvaPartManager.SetPostionHair(PartOfBody.HairBack, BackHair, hairData);
        }
    }

    public void SetColorVisual(Color color)
    {
        FrontHair.GetComponent<Image>().color = color;
        BackHair.GetComponent<Image>().color = color;
        Body.GetComponent<Image>().color = color;
        Top.GetComponent<Image>().color = color;
    }

    public Sprite GetFrontHair() { return FrontHair.GetComponent<Image>().sprite; }
    public Sprite GetBackHair() { return BackHair.GetComponent<Image>().sprite; }
    public Sprite GetTop() { return Top.GetComponent<Image>().sprite; }
    public Sprite GetBody() { return Body.GetComponent<Image>().sprite; }

}
