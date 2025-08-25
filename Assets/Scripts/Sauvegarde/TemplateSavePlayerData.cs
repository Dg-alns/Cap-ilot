using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[System.Serializable]
public class TemplateSavePlayerData
{
    public Sprite Part_Body;
    public Color Color_Part_Body;

    public TemplateSavePlayerData(Sprite sprite, Color Color)
    {
        Part_Body = sprite;
        Color_Part_Body = Color;
    }

    public void CheckNewPart_Body(Sprite sprite)
    {
        if(sprite != Part_Body)
        {
            Part_Body = sprite;
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
