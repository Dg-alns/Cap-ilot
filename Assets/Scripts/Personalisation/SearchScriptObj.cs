    using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

    public class SearchScriptObj : MonoBehaviour
    {
        static public List<PersoPlayerData> TopSObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> BodySObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> EyesObj = new List<PersoPlayerData>();
        static public List<HairData> HairFrontSObj = new List<HairData>();
        static public List<PersoPlayerData> HairBackSObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> ShoesSObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> BottomSObj = new List<PersoPlayerData>();

        void Awake()
        {
            LoadCategory("ScriptObj/Body", BodySObj);
            LoadCategory("ScriptObj/Top", TopSObj);
            LoadCategory("ScriptObj/Eyes", EyesObj); 
            LoadHairCategory("ScriptObj/Hair/Hair", HairFrontSObj);
            LoadCategory("ScriptObj/Hair/Back", HairBackSObj);
            LoadCategory("ScriptObj/Shoes", ShoesSObj);
            LoadCategory("ScriptObj/Bottom", BottomSObj);
        }

        public static void LoadCategory<T>(string Path, List<T> lst) where T : ScriptableObject
        {
            lst.Clear();
            T[] alldataPerso = Resources.LoadAll<T>(Path);

            for (int i = 0; i < alldataPerso.Length; i++) 
            {
                if (alldataPerso[i] is PersoPlayerData perso && perso.IsDebloquer() == false)
                    continue;

                lst.Add(alldataPerso[i]);           
            }
        }

        void LoadHairCategory(string Path, List<HairData> lst)
        {
            lst.Clear();
            HairData[] alldataPerso = Resources.LoadAll<HairData>(Path);

            for (int i = 0; i < alldataPerso.Length; i++) 
            {
                if (alldataPerso[i].IsDebloquer() == false)
                    continue;

                lst.Add(alldataPerso[i]);           
            }
        }

        public static List<PersoPlayerData> GetLsitScriptObj(PartOfBody part)
        {
            switch (part)
            {
                case PartOfBody.Body:
                    return BodySObj;

                case PartOfBody.Eyes:
                    return EyesObj;

                case PartOfBody.Top:
                    return TopSObj;

                case PartOfBody.Bottom:
                    return BottomSObj;

                case PartOfBody.Shoes:
                    return ShoesSObj;
            }

            return null;
        }

        public static List<HairData> GetLstHairObj()
        {
            return HairFrontSObj;

        }

    public static Sprite GetSprite(List<PersoPlayerData> lst, string name)
    {
        for(int i = 0; i < lst.Count; i++)
        {
            if (lst[i].sprite.name == name)
                return lst[i].sprite;
        }

        return null;
    }

    public static Sprite GetSpriteHair(List<HairData> lst, string name) 
    {
        
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].sprite.name == name)
                return lst[i].sprite;
        }
        
        return null;
    }
}
