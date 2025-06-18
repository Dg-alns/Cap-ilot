using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create Part of Player")]
public class PersoPlayerData : ScriptableObject
{
    public PartOfBody part;
    public Sprite sprite;
    public bool isDebloquer = true;
}
