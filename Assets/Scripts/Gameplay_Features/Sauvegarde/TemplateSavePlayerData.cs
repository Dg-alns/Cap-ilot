using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[System.Serializable]
public class TemplateSavePlayerData
{
    public string Name_Part_Body;
    public Color Color_Part_Body;

    public TemplateSavePlayerData(string sprite, Color Color)
    {
        Name_Part_Body = sprite;
        Color_Part_Body = Color;
    }

    public void CheckNewPart_Body(string sprite)
    {
        if(sprite != Name_Part_Body)
        {
            Name_Part_Body = sprite;
        }
    }

    public void CheckNewColor(Color Color)
    {
        if(Color != Color_Part_Body)
        {
            Color_Part_Body = Color;
        }
    }

    public string GetSpriteName() { return Name_Part_Body; }
    public Color GetColor() { return Color_Part_Body; }
}
