using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemplateSavePlayerData
{
    public Sprite Part_Body;
    public Color Color_Part_Body;

    public TemplateSavePlayerData(Sprite Part, Color Color)
    {
        Part_Body = Part;
        Color_Part_Body = Color;
    }

    public void CheckNewPart_Body(Sprite Part)
    {
        if(Part != Part_Body)
        {
            Part_Body = Part;
        }
    }

    public void CheckNewColor(Color Color)
    {
        if(Color != Color_Part_Body)
        {
            Color_Part_Body = Color;
        }
    }

    public Sprite GetSprite() { return Part_Body; }
    public Color GetColor() { return Color_Part_Body; }
}
