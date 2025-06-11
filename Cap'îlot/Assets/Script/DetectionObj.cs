using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetectionObj : MonoBehaviour
{
    ObjCachee objCachee;
    public Camera cam;

    public Image menu;

    List<GameObject> objects;
    List<TextMeshProUGUI> nameobjs;
    List<Infos_MiniJeux> infos;

    private void Start()
    {
        objCachee = gameObject.GetComponent<ObjCachee>();

        objects = objCachee.objs;
        nameobjs = objCachee.Nameobjs;
        infos = objCachee.allinfos;
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

        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].activeSelf == false)
                continue;

            if (Detection(objects[i]))
            {
                if(Detection(objCachee.diabetes))
                    break;


                objects[i].SetActive(false);
                nameobjs[i].fontStyle = FontStyles.Strikethrough;

                Infos_MiniJeux infos = FindInfos(objects[i]);

                infos.gameObject.SetActive(true);
                gameObject.SetActive(false);

                break;
                
                               
            }
        }
    }

    Infos_MiniJeux FindInfos(GameObject obj)
    {
        foreach (Infos_MiniJeux inf in infos)
        {
            if (inf.ObjReference == obj)
            {
                infos.Remove(inf);
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

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // a changer
        {
            DetectionObject();
            DetectionMenu();
        }
    }
}
