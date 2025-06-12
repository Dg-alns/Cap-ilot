using TMPro;
using UnityEngine;

public class Objects : MonoBehaviour
{
    Sprite sprite;
    public string str;
    public bool CanShowInfo = true;

    public GameObject prefabInfos;

    public TextMeshProUGUI nameobjs;

    Infos_MiniJeux infos;

    public Diabète db;

    public Sprite GetSprite() { return sprite; }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;

        if(prefabInfos == null )
            return;

        infos = prefabInfos.GetComponent<Infos_MiniJeux>();

        infos.ObjReference = gameObject;
    }


    private void OnMouseUp()
    {
        Debug.Log("obj");
        if (gameObject.activeSelf == false)
            return;

        if (db.clikme)
        {
            db.clikme = false;
            return;
        }


        gameObject.SetActive(false);
        nameobjs.fontStyle = FontStyles.Strikethrough;

        infos.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    

}
