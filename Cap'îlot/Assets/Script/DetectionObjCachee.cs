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
    ObjCachee objCachee;
    public Camera cam;

    List<Objects> objects;
    List<TextMeshProUGUI> nameobjs;
    public Infos_MiniJeux infos;

    private void Awake()
    {
        objects = Tools.CreateList<Objects>("ToFind");
        nameobjs = Tools.CreateList<TextMeshProUGUI>("Bot");


        objCachee = gameObject.GetComponent<ObjCachee>();

        Assert.AreEqual(objects.Count, nameobjs.Count);

        for (int i = 0; i < objects.Count; i++)
        {
            nameobjs[i].text = objects[i].name;
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

                infos.AssociateInfo(objects[i]);
                infos.gameObject.SetActive(true);

                objCachee.AddScore();
                break;
            }
        }
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
