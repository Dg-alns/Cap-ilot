using UnityEngine;

public class Objects : MonoBehaviour
{
    Sprite sprite;
    public string infos;
    public bool CanShowInfo = true;

    public Sprite GetSprite() { return sprite; }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
