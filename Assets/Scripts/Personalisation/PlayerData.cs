using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create PersoData")]
public class PlayerData : ScriptableObject
{
    public Sprite Corps;
    public Sprite Cheveux;
    public Sprite AccessoirTete;
    public Sprite Haut;
    public Sprite Bas;
    public Sprite Chaussure;

    public Color Color_Corps;
    public Color Color_Cheveux;
    public Color Color_AccessoirTete;
    public Color Color_Haut;
    public Color Color_Bas;
    public Color Color_Chaussure;
}
