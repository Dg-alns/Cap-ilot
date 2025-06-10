using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Infos_MiniJeux : MonoBehaviour
{
    public Objects ObjReference;

    public TextMeshProUGUI text;
    public Image img;

    public GameObject Back;


    void Start()
    {
        if(ObjReference.sprite != null)
            img.sprite = ObjReference.sprite;

        text.text = ObjReference.infos;
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjReference.CanShowInfo)
        {
            if (ObjReference.gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
                ObjReference.CanShowInfo = false;
            }
        }
    }
}
