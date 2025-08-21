using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create Hair of Player")]
public class HairPlayerData : ScriptableObject
{
    public PartOfBody part;
    public Sprite sprite;
    [SerializeField] private bool isDebloquer;

    public void SetIsDebloque(bool value) {  isDebloquer = value; }

    public bool IsDebloquer() {  return isDebloquer; }
}
