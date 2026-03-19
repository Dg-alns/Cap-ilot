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

enum STATEPORT
{
    NotFind,
    CanGoButNotDiscover,
    Discover,
}

[CreateAssetMenu(fileName = "Port", menuName = "ScriptableObjects/Port")]
public class Port : ScriptableObject
{
    public string IleName;
    [SerializeField] bool isDiscover;
    [SerializeField] bool CanGoToIle;

    public Iles ile = Iles.None;

    public void IsDiscover() 
    { 
        PlayerPrefs.SetInt(IleName, (int)STATEPORT.Discover);
    }
    
    public void CanGotoIle()
    {
        PlayerPrefs.SetInt(IleName, (int)STATEPORT.CanGoButNotDiscover);
    }

    public bool GetIsDiscover() 
    {
        if(isDiscover)
            return true;

        isDiscover = DetectionState(STATEPORT.Discover);

        return isDiscover; 
    }

    public bool GetCanGotoIle() 
    {
        if(CanGoToIle)
            return true;

        CanGoToIle = DetectionState(STATEPORT.CanGoButNotDiscover);

        return CanGoToIle; 
    }



    bool DetectionState(STATEPORT state)
    {
        if (PlayerPrefs.HasKey(IleName) == false)
        {
            PlayerPrefs.SetInt(IleName, 0);
        }


        if (PlayerPrefs.GetInt(IleName) >= (int)state)
            return true;

        return false;
    }
}
