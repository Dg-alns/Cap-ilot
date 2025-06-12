using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class DetectionObj : MonoBehaviour
{
    public ObjCachee objCachee;
    public Camera cam;

    public Image menu;
    public Image insuline;

    List<Objects> objects;
    List<TextMeshProUGUI> nameobjs;
    List<Infos_MiniJeux> allinfos;

    private void Start()
    {
        objects = Tools.CreateList<Objects>("ToFind");
        nameobjs = Tools.CreateList<TextMeshProUGUI>("Bot");
        allinfos = Tools.CreateList<Infos_MiniJeux>("AllInfo");


        objCachee = gameObject.GetComponent<ObjCachee>();

        foreach (Infos_MiniJeux infos in allinfos)
        {
            infos.gameObject.SetActive(false);
        }

        Assert.AreEqual(objects.Count, nameobjs.Count);

        for (int i = 0; i < objects.Count; i++)
        {
            nameobjs[i].text = objects[i].name;
            objects[i].GetComponent<Objects>().SetText(nameobjs[i]);
        }


    }

    bool Detection(GameObject obj)
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 positionMin = cam.WorldToScreenPoint(obj.GetComponent<Renderer>().bounds.min);
        Vector3 positionMax = cam.WorldToScreenPoint(obj.GetComponent<Renderer>().bounds.max);

        bool InY = positionMin.y <= mouse.y && positionMax.y >= mouse.y;
        bool InX = positionMin.x <= mouse.x && positionMax.x >= mouse.x;

        return InY && InX;
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

    void DetectionObject()
    {
        if (objects.Count <= 0)
            return;

        for (int i = 0; i<objects.Count; i++)
        {
            if (objects[i].gameObject.activeSelf == false)
                continue;

            if (Detection(objects[i].gameObject))
            {
                if (Detection(objCachee.diabetes))
                    break;


                objects[i].gameObject.SetActive(false);
                nameobjs[i].fontStyle = FontStyles.Strikethrough;

                Infos_MiniJeux infos = FindInfos(objects[i].gameObject);

                infos.gameObject.SetActive(true);
                gameObject.SetActive(false);

                break;


            }
        }
    }

    Infos_MiniJeux FindInfos(GameObject obj)
    {
        foreach (Infos_MiniJeux inf in allinfos)
        {
            if (inf.ObjReference == obj)
            {
                allinfos.Remove(inf);
                return inf;
            }
        }
        return null;
    }

    void DetectionMenu()
    {
        if (DetectionImg(menu))
        {
            Debug.Log("GO MENU");
        }
    }

    void DetectionInsuluine()
    {
        if (DetectionImg(insuline))
        {
            StartCoroutine(objCachee.diabetes.GetComponent<Diabète>().DbWithInsuline());

        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectionObject();
            DetectionMenu();
            DetectionInsuluine();
        }
    }
}
