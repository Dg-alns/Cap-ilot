using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create Part of Player")]
public class PersoPlayerData : ScriptableObject
{
    public PartOfBody part;
    public Sprite sprite;
    [SerializeField] private bool isDebloquer;

    public void SetIsDebloque(bool value) {  isDebloquer = value; }

    public bool IsDebloquer() {  return isDebloquer; }
}
