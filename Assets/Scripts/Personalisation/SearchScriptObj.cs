using System.Collections.Generic;
using UnityEngine;

public class SearchScriptObj : MonoBehaviour
{
    static public List<PersoPlayerData> TopSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> BodySObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> EyesLeftObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> EyesRightObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> HairSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> ShoesSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> BottomSObj = new List<PersoPlayerData>();

    void Awake()
    {
        LoadCategory("ScriptObj/Body", BodySObj);
        LoadCategory("ScriptObj/Top", TopSObj);
        LoadCategory("ScriptObj/Eyes/Left", EyesLeftObj); 
        LoadCategory("ScriptObj/Eyes/Right", EyesRightObj); 
        LoadCategory("ScriptObj/Hair", HairSObj);
        LoadCategory("ScriptObj/Shoes", ShoesSObj);
        LoadCategory("ScriptObj/Bottom", BottomSObj);
    }

    void LoadCategory(string Path, List<PersoPlayerData> lst)
    {
        lst.Clear();
        PersoPlayerData[] alldataPerso = Resources.LoadAll<PersoPlayerData>(Path);

        for (int i = 0; i < alldataPerso.Length; i++) 
        {
            if (alldataPerso[i].IsDebloquer() == false)
                continue;

            lst.Add(alldataPerso[i]);           
        }
    }
}
