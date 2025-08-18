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
    [SerializeField] bool isDiscover;
    [SerializeField] bool CanGoToIle;

    public Iles ile = Iles.None;

    public void IsDiscover() { isDiscover = true; }
    public void CanGotoIle() { CanGoToIle = true; }

    public bool GetIsDiscover() { return isDiscover; }
    public bool GetCanGotoIle() { return CanGoToIle; }
}
