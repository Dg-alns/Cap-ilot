using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class DetectionObjCachee : DetectionUI
{
    public ObjCachee objCachee;
    public Camera cam;

    List<Objects> objects;
    List<TextMeshProUGUI> nameobjs;
    List<Infos_MiniJeux> allinfos;

    private void Awake()
    {
        objects = Tools.CreateList<Objects>("ToFind");
        nameobjs = Tools.CreateList<TextMeshProUGUI>("Bot");
        allinfos = Tools.CreateList<Infos_MiniJeux>("AllInfo");


        objCachee = gameObject.GetComponent<ObjCachee>();

        Assert.AreEqual(objects.Count, nameobjs.Count);
        Assert.AreEqual(objects.Count, allinfos.Count);

        for (int i = 0; i < objects.Count; i++)
        {
            nameobjs[i].text = objects[i].name;
            objects[i].GetComponent<Objects>().SetText(nameobjs[i]);
            allinfos[i].ObjReference = objects[i].gameObject;
            allinfos[i].gameObject.SetActive(false);
        }

        objCachee.SetScore(objects.Count * objCachee.point);
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

                objCachee.timer.stop = true;

                objects[i].gameObject.SetActive(false);
                nameobjs[i].fontStyle = FontStyles.Strikethrough;

                Infos_MiniJeux infos = FindInfos(objects[i].gameObject);

                infos.gameObject.SetActive(true);
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

    public override void DetectionInsuluine()
    {
        if (DetectionImg(insuline))
        {
            StartCoroutine(diabete.GetComponent<Diabète>().DbWithInsuline());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectionMenu();
            DetectionInsuluine();
            DetectionObject();
        }
    }
}
