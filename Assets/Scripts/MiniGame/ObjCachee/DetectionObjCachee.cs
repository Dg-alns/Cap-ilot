using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class DetectionObjCachee : DetectionUI
{
    public Camera cam;
    public Timer timer;
    public Score score;

    List<Objects> objects;
    List<TextMeshProUGUI> nameobjs;
    public Infos_MiniJeux infos;

    private void Awake()
    {
        objects = Tools.CreateList<Objects>("ToFind");
        nameobjs = Tools.CreateList<TextMeshProUGUI>("Bot");

        _tools = FindAnyObjectByType<Tools>();

        Assert.AreEqual(objects.Count, nameobjs.Count);

        for (int i = 0; i < objects.Count; i++)
        {
            nameobjs[i].text = objects[i].name;
        }


        timer.stop = true;

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

    bool FindActiveGameObject()
    {
        for(int i = 0; i < objects.Count;i++)
        {
            if (objects[i].gameObject.activeSelf)
                return true;
        }

        return false;
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
                if (Detection(diabete))
                    break;

                timer.stop = true;

                objects[i].gameObject.SetActive(false);
                nameobjs[i].fontStyle = FontStyles.Strikethrough;

                infos.AssociateInfo(objects[i]);
                infos.gameObject.SetActive(true);
                break;
            }
        }
    }

    void Update()
    {
        if (FindActiveGameObject() == false)
        {
            if(infos.gameObject.activeSelf == false)
                score.LauchScore();

            return;
        }

        if (Input.GetMouseButtonDown(0) && !_tools.IsPointerOverUIElement())
        {
            DetectionObject();
        }
        
    }
}
