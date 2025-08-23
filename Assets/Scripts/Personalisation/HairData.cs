using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create Hair of Player")]
public class HairData : ScriptableObject
{
    public PartOfBody part;
    public Sprite sprite;
    public PersoPlayerData Back;
    [SerializeField] private bool isDebloquer;

    public void SetIsDebloque(bool value) {  isDebloquer = value; }

    public bool IsDebloquer() {  return isDebloquer; }
}
