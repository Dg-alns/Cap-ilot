using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class SearchScriptObj : MonoBehaviour
{
    // Arm
    static public List<PersoPlayerData> HautSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> HandSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> ShoulderSObj = new List<PersoPlayerData>();


    static public List<PersoPlayerData> CorpsSObj = new List<PersoPlayerData>();

    // Face
    //static public List<ScriptableObject> EyebrowsSObj = new List<ScriptableObject>();
    static public List<PersoPlayerData> AccTeteSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> HairSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> MouseSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> NoseSObj = new List<PersoPlayerData>();


    // Leg
    static public List<PersoPlayerData> ShoesSObj = new List<PersoPlayerData>();
    static public List< PersoPlayerData> ThighSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> BasSObj = new List<PersoPlayerData>();

    void Awake() // Todo Personalisation Changer Les scripObj par 'corps, cheveux, accessoires tete, vetement haut, vetement bas, chaussures'
    {
        LoadCategory("ScriptObj/Arm/Arm", HautSObj);
        //LoadCategory("ScriptObj/Arm/Hand", HandSObj);
        //LoadCategory("ScriptObj/Arm/Shoulder", ShoulderSObj);

        LoadCategory("ScriptObj/Chest", CorpsSObj);

        ////LoadCategory("ScriptObj/Face/EyeBrows", EyesSObj); 
        LoadCategory("ScriptObj/Face/Eyes", AccTeteSObj); 
        LoadCategory("ScriptObj/Face/Hair", HairSObj);
        //LoadCategory("ScriptObj/Face/Mouse", MouseSObj);
        //LoadCategory("ScriptObj/Face/Nose", NoseSObj);

        LoadCategory("ScriptObj/Leg/Foot", ShoesSObj);
        //LoadCategory("ScriptObj/Leg/Thigh", ThighSObj);
        LoadCategory("ScriptObj/Leg/Leg", BasSObj);

    }

    void LoadCategory(string Path, List<PersoPlayerData> lst)
    {
        lst.Clear();
        PersoPlayerData[] alldataPerso = Resources.LoadAll<PersoPlayerData>(Path);

        for (int i = 0; i < alldataPerso.Length; i++) 
        {
            if (alldataPerso[i].isDebloquer == false)
                Debug.Log(i);

            lst.Add(alldataPerso[i]);           
        }
    }
}
