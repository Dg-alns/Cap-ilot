using System;
using System.Xml.Xsl;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Infos_MiniJeux : MonoBehaviour
{
    public GameObject Game;
    public GameObject ObjReference;
    Objects obj;

    public TextMeshProUGUI text;
    public Image img;

    public Image Back;


    void Start()
    {
        obj = ObjReference.GetComponent<Objects>();


        if (obj.sprite != null)
            img.sprite = obj.sprite;

        text.text = obj.infos;


    }

    bool DetectionImg(Image obj)
    {
        Vector3 mouse = Input.mousePosition;

        RectTransform rect = obj.GetComponent<RectTransform>();
        float HalfHeight = (rect.rect.height * obj.transform.localScale.y) / 2f;
        float HalfWidth = (rect.rect.width * obj.transform.localScale.x) / 2f;

        bool InY = obj.transform.position.y - HalfHeight <= mouse.y && obj.transform.position.y + HalfHeight >= mouse.y;
        bool InX = obj.transform.position.x - HalfWidth <= mouse.x && obj.transform.position.x + HalfWidth >= mouse.x;

        return InY && InX;
    }


    void DetectionBack()
    {
        if (DetectionImg(Back))
        {
            gameObject.SetActive(false);
            Game.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf ==false)
            { return; }

        if (Input.GetMouseButtonDown(0)) // a changer
        {
            DetectionBack();
        }
    }
}
