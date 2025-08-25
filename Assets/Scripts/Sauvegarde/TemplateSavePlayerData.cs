using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TemplateSavePlayerData
{
    public string Name_Part_Body;
    public Color Color_Part_Body;

    public TemplateSavePlayerData(string name, Color Color)
    {
        Name_Part_Body = name;
        Color_Part_Body = Color;
    }

    public void CheckNewPart_Body(string name)
    {
        if(name != Name_Part_Body)
        {
            Name_Part_Body = name;
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
