using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// Detection all part of player for change after for personalisation script
public enum PartOfBody
{
    Body,
    Hair,
    HairBack,
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

    static public GameObject GetPartOfPlayer(string part)
    {
        foreach(var prt in AllParOfPlayer)
        {
            if(prt.Key.ToString() == part)
                return prt.Value;
        }
        return null;
    }

    static public Dictionary<PartOfBody, GameObject> GetHairs()
    {
        Dictionary<PartOfBody, GameObject> lst = new Dictionary<PartOfBody, GameObject>();

        foreach (var prt in AllParOfPlayer)
        {
            if (prt.Key.ToString() == PartOfBody.Hair.ToString() || prt.Key.ToString() == PartOfBody.HairBack.ToString())
                lst[prt.Key] = prt.Value;
        }

        return lst;
    }

    static public void Init(GameObject gameObject)
    {
        AllParOfPlayer.Clear();
        SpriteRenderer[] e = gameObject.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < e.Length; i++)
        {
            PartOfBody part = DetectionOfPart(e[i].gameObject.name);

            if (part != PartOfBody.NULL)
            {
                AllParOfPlayer.Add(part, e[i].gameObject);
            }
        }
    }


    //void Awake()
    //{
    //    AllParOfPlayer.Clear();
    //    SpriteRenderer[] e = GetComponentsInChildren<SpriteRenderer>();

    //    for (int i = 0; i < e.Length; i++)
    //    {
    //        PartOfBody part = DetectionOfPart(e[i].gameObject.name);

    //        if (part != PartOfBody.NULL)
    //        {
    //            AllParOfPlayer.Add(part, e[i].gameObject);
    //        }
    //    }
    //}
}
