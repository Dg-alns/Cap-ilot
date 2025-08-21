using UnityEngine;


[CreateAssetMenu(fileName = "PersonalisationData", menuName = "PersonalisationData/Create Eyes of Player")]
public class EyesPlayerData : ScriptableObject
{
    public PartOfBody part;
    public Sprite EyeLeft;
    public Sprite EyeRight;
    [SerializeField] private bool isDebloquer;

    public void SetIsDebloque(bool value) {  isDebloquer = value; }

    public bool IsDebloquer() {  return isDebloquer; }
}
