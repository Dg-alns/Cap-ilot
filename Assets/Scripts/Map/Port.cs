using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Iles
{
    None,
    Principal,
    Hopital,
    Sport,
    Alimentation,
    Ecole,
    Relation,
    Tentation
}

[CreateAssetMenu(fileName = "Port", menuName = "ScriptableObjects/Port")]
public class Port : ScriptableObject
{
    public string IleName;
    public bool isDiscover = false;
    public bool CanGoToIle = false;

    public Iles ile = Iles.None;
}
