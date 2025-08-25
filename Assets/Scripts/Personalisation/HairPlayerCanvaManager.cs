using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairPlayerCanvaManager : MonoBehaviour
{
    //public PositionementPartCanvaManager positionementPartCanvaManager

    public Image FrontHair;
    public Image BackHair;
    public Image Body;
    public Image Top;

    public void InitHair(Sprite Front, Sprite Back = null)
    {
        FrontHair.sprite = Front;
        BackHair.color = Color.clear;
        if (Back != null)
        {
            BackHair.sprite = Back;
            BackHair.color = Color.white;
        }
    }

    public void SetColorVisual(Color color)
    {
        FrontHair.color = color;
        BackHair.color = color;
        Body.color = color;
        Top.color = color;
    }

}
