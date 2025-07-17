using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SearchScriptObj : MonoBehaviour
{
    // Arm
    static public List<PersoPlayerData> ArmSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> HandSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> ShoulderSObj = new List<PersoPlayerData>();


    static public List<PersoPlayerData> ChestSObj = new List<PersoPlayerData>();

    // Face
    //static public List<ScriptableObject> EyebrowsSObj = new List<ScriptableObject>();
    static public List<PersoPlayerData> EyesSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> HairSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> MouseSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> NoseSObj = new List<PersoPlayerData>();


    // Leg
    static public List<PersoPlayerData> FootSObj = new List<PersoPlayerData>();
    static public List< PersoPlayerData> ThighSObj = new List<PersoPlayerData>();
    static public List<PersoPlayerData> LegSObj = new List<PersoPlayerData>();

    void Awake()
    {
        LoadCategory("Assets/Art/personalisation/ScriptObj/Arm/Arm", ArmSObj);
        LoadCategory("Assets/Art/personalisation/ScriptObj/Arm/Hand", HandSObj);
        LoadCategory("Assets/Art/personalisation/ScriptObj/Arm/Shoulder", ShoulderSObj);

        LoadCategory("Assets/Art/personalisation/ScriptObj/Chest", ChestSObj);

        //LoadCategory("Assets/Art/personalisation/ScriptObj/Face/EyeBrows", EyesSObj); 
        LoadCategory("Assets/Art/personalisation/ScriptObj/Face/Eyes", EyesSObj); 
        LoadCategory("Assets/Art/personalisation/ScriptObj/Face/Hair", HairSObj);
        LoadCategory("Assets/Art/personalisation/ScriptObj/Face/Mouse", MouseSObj);
        LoadCategory("Assets/Art/personalisation/ScriptObj/Face/Nose", NoseSObj);

        LoadCategory("Assets/Art/personalisation/ScriptObj/Leg/Foot", FootSObj);
        LoadCategory("Assets/Art/personalisation/ScriptObj/Leg/Thigh", ThighSObj);
        LoadCategory("Assets/Art/personalisation/ScriptObj/Leg/Leg", LegSObj);

    }

    void LoadCategory(string Path, List<PersoPlayerData> lst)
    {
       /* lst.Clear();
        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { Path });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            PersoPlayerData asset = AssetDatabase.LoadAssetAtPath<PersoPlayerData>(path);

            if (asset != null)
            {
                lst.Add(asset);
            }
        }*/
    }
}
