using TMPro;
using UnityEngine;

public class Objects : MonoBehaviour
{
    Sprite sprite;
    public string str;
    public bool CanShowInfo = true;

    public GameObject prefabInfos;

    TextMeshProUGUI nameobjs;

    public Sprite GetSprite() { return sprite; }

    public void SetText(TextMeshProUGUI text) { nameobjs =  text; }
    

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
