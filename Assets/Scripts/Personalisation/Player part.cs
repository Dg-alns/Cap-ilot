using System;
using System.Collections.Generic;
using UnityEngine;

// Detection all part of player for change after for personalisation script
public enum PartOfBody
{
    Body,
    Hair,
    EyesLeft,
    EyesRight,
    Top,
    Bottom,
    Shoes,
    NULL
}

public class Playerpart : MonoBehaviour
{
    static public Dictionary<PartOfBody, GameObject> AllParOfPlayer = new Dictionary<PartOfBody, GameObject>();

    static public PartOfBody DetectionOfPart(string name)
    {
        foreach (PartOfBody part in Enum.GetValues(typeof(PartOfBody)))
        {
            if (part.ToString().Contains(name))
                return part;

        }
        return PartOfBody.NULL;
    }

    static public  GameObject GetPartOfPlayer(string part)
    {
        foreach(var prt in AllParOfPlayer)
        {
            if(prt.Key.ToString() == part)
                return prt.Value;
        }
        return null;
    }


    void Awake()
    {
        AllParOfPlayer.Clear();
        SpriteRenderer[] e = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < e.Length; i++)
        {
            PartOfBody part = DetectionOfPart(e[i].gameObject.name);

            if(part != PartOfBody.NULL)
            {
                AllParOfPlayer.Add(part, e[i].gameObject);
            }
        }
    }
}
