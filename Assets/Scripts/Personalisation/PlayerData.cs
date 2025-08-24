using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create PersoData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] Sprite Corps;
    [SerializeField] Sprite CheveuxFront;
    [SerializeField] Sprite CheveuxBack;
    [SerializeField] Sprite EyeLeft;
    [SerializeField] Sprite EyeRight;
    [SerializeField] Sprite Haut;
    [SerializeField] Sprite Bas;
    [SerializeField] Sprite Chaussure;

    [SerializeField] Color Color_Corps;
    [SerializeField] Color Color_CheveuxFront;
    [SerializeField] Color Color_CheveuxBack;
    [SerializeField] Color Color_EyeLeft;
    [SerializeField] Color Color_EyeRight;
    [SerializeField] Color Color_Haut;
    [SerializeField] Color Color_Bas;
    [SerializeField] Color Color_Chaussure;


    public void SetPart_Body(PartOfBody part, Sprite sprite, Color color)
    {
        switch(part)
        {
            case PartOfBody.Body:
                Corps = sprite;
                Color_Corps = color;
                break;
            case PartOfBody.HairBack:
                CheveuxBack = sprite;
                Color_CheveuxBack = color;
                break;
            case PartOfBody.Hair:
                CheveuxFront = sprite;
                Color_CheveuxFront = color;
                break;
            case PartOfBody.EyesLeft:
                EyeLeft = sprite;
                Color_EyeLeft = color;
                break;
            case PartOfBody.EyesRight:
                EyeRight = sprite;
                Color_EyeRight = color;
                break;
            case PartOfBody.Top:
                Haut = sprite;
                Color_Haut = color;
                break;
            case PartOfBody.Bottom:
                Bas = sprite;
                Color_Bas = color;
                break;
            case PartOfBody.Shoes:
                Chaussure = sprite;
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
            case PartOfBody.EyesLeft:
                return EyeLeft;
            case PartOfBody.EyesRight:
                return EyeRight;
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
            case PartOfBody.EyesLeft:
                return Color_EyeLeft;
            case PartOfBody.EyesRight:
                return Color_EyeRight;
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
