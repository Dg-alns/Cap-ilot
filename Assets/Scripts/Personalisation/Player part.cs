using System;
using System.Collections.Generic;
using UnityEngine;

// Detection all part of player for change after for personalisation script
public enum PartOfBody
{
    Chest,
    Hair,
    Eyes,
    Mouse,
    Nose,
    L_Shoulder,
    R_Shoulder,
    L_Arm,
    R_Arm,
    L_Hand,
    R_Hand,
    L_Thigh,
    R_Thigh,
    L_Leg,
    R_Leg,
    L_Foot,
    R_Foot,
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
