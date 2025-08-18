using UnityEngine;

public enum PART
{
    Corps,
    Cheveux,
    AccessoirTete,
    Haut,
    Bas,
    Chaussure,
}


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create PersoData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] Sprite Corps;
    [SerializeField] Sprite Cheveux;
    [SerializeField] Sprite AccessoirTete;
    [SerializeField] Sprite Haut;
    [SerializeField] Sprite Bas;
    [SerializeField] Sprite Chaussure;

    [SerializeField] Color Color_Corps;
    [SerializeField] Color Color_Cheveux;
    [SerializeField] Color Color_AccessoirTete;
    [SerializeField] Color Color_Haut;
    [SerializeField] Color Color_Bas;
    [SerializeField] Color Color_Chaussure;


    public void SaveSprite(PART part, Sprite sprite, Color color)
    {
        switch(part)
        {
            case PART.Corps:
                Corps = sprite;
                Color_Corps = color;
                break;
            case PART.Cheveux:
                Cheveux = sprite;
                Color_Cheveux = color;
                break;
            case PART.AccessoirTete:
                AccessoirTete = sprite;
                Color_AccessoirTete = color;
                break;
            case PART.Haut:
                Haut = sprite;
                Color_Haut = color;
                break;
            case PART.Bas:
                Bas = sprite;
                Color_Bas = color;
                break;
            case PART.Chaussure:
                Chaussure = sprite;
                Color_Chaussure = color;
                break;
        }
    }

    public Sprite GetSprite(PART part)
    {
        switch(part)
        {
            case PART.Corps:
                return Corps;
            case PART.Cheveux:
                return Cheveux;
            case PART.AccessoirTete:
                return AccessoirTete;
            case PART.Haut:
                return Haut;
            case PART.Bas:
                return Bas;     
            case PART.Chaussure:
                return Chaussure;
        }

        return null;
    }

    public Color GetColor(PART part)
    {
        switch(part)
        {
            case PART.Corps:
                return Color_Corps;
            case PART.Cheveux:
                return Color_Cheveux;
            case PART.AccessoirTete:
                return Color_AccessoirTete;
            case PART.Haut:
                return Color_Haut;
            case PART.Bas:
                return Color_Bas;
            case PART.Chaussure:
                return Color_Chaussure;
        }

        return Color.white;
    }
}
