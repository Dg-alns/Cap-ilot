using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create PersoData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] Sprite Corps;
    [SerializeField] Sprite CheveuxFront;
    [SerializeField] Sprite CheveuxBack;
    [SerializeField] Sprite Eyes;
    [SerializeField] Sprite EyeRight;
    [SerializeField] Sprite Haut;
    [SerializeField] Sprite Bas;
    [SerializeField] Sprite Chaussure;

    [SerializeField] Color Color_Corps;
    [SerializeField] Color Color_CheveuxFront;
    [SerializeField] Color Color_CheveuxBack;
    [SerializeField] Color Color_Eyes;
    [SerializeField] Color Color_EyeRight;
    [SerializeField] Color Color_Haut;
    [SerializeField] Color Color_Bas;
    [SerializeField] Color Color_Chaussure;


    public void SetPart_Body(PartOfBody part, string sprite, Color color)
    {
        switch(part)
        {
            case PartOfBody.Body:
                Corps = SearchScriptObj.GetSprite(SearchScriptObj.BodySObj, sprite);
                Color_Corps = color;
                break;
            case PartOfBody.HairBack:
                if(sprite == null)
                    CheveuxBack = null;
                else
                    CheveuxBack = SearchScriptObj.GetSprite(SearchScriptObj.HairBackSObj, sprite);
                Color_CheveuxBack = color;
                break;
            case PartOfBody.Hair:
                CheveuxFront = SearchScriptObj.GetSpriteHair(SearchScriptObj.HairFrontSObj, sprite);
                Color_CheveuxFront = color;
                break;
            case PartOfBody.Eyes:
                Eyes = SearchScriptObj.GetSprite(SearchScriptObj.EyesObj, sprite);
                Color_Eyes = color;
                break;
            case PartOfBody.Top:
                Haut = SearchScriptObj.GetSprite(SearchScriptObj.TopSObj, sprite);
                Color_Haut = color;
                break;
            case PartOfBody.Bottom:
                Bas = SearchScriptObj.GetSprite(SearchScriptObj.BottomSObj, sprite);
                Color_Bas = color;
                break;
            case PartOfBody.Shoes:
                Chaussure = SearchScriptObj.GetSprite(SearchScriptObj.ShoesSObj, sprite);
                Color_Chaussure = color;
                break;
        }
    }

    public Sprite GetSprite(PartOfBody part)
    {
        switch(part)
        {
            case PartOfBody.Body:
                return Corps;
            case PartOfBody.Hair:
                return CheveuxFront;
            case PartOfBody.HairBack:
                return CheveuxBack;
            case PartOfBody.Eyes:
                return Eyes;
            case PartOfBody.Top:
                return Haut;
            case PartOfBody.Bottom:
                return Bas;     
            case PartOfBody.Shoes:
                return Chaussure;
        }

        return null;
    }

    public Color GetColor(PartOfBody part)
    {
        switch(part)
        {
            case PartOfBody.Body:
                return Color_Corps;
            case PartOfBody.Hair:
                return Color_CheveuxFront;
            case PartOfBody.HairBack:
                return Color_CheveuxBack;
            case PartOfBody.Eyes:
                return Color_Eyes;
            case PartOfBody.Top:
                return Color_Haut;
            case PartOfBody.Bottom:
                return Color_Bas;
            case PartOfBody.Shoes:
                return Color_Chaussure;
        }

        return Color.white;
    }
}
