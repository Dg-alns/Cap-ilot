    using System.Collections.Generic;
    using UnityEngine;

    public class SearchScriptObj : MonoBehaviour
    {
        static public List<PersoPlayerData> TopSObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> BodySObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> EyesLeftObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> EyesRightObj = new List<PersoPlayerData>();
        static public List<HairData> HairFrontSObj = new List<HairData>();
        static public List<PersoPlayerData> HairBackSObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> ShoesSObj = new List<PersoPlayerData>();
        static public List<PersoPlayerData> BottomSObj = new List<PersoPlayerData>();

        void Awake()
        {
            LoadCategory("ScriptObj/Body", BodySObj);
            LoadCategory("ScriptObj/Top", TopSObj);
            LoadCategory("ScriptObj/Eyes/Left", EyesLeftObj); 
            LoadCategory("ScriptObj/Eyes/Right", EyesRightObj);
            LoadHairCategory("ScriptObj/Hair/Hair", HairFrontSObj);
            LoadCategory("ScriptObj/Hair/Back", HairBackSObj);
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

                case PartOfBody.EyesLeft:
                    return EyesLeftObj;

                case PartOfBody.EyesRight:
                    return EyesRightObj;

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
}
