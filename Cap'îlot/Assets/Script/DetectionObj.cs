using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectionObj : MonoBehaviour
{
    public ObjCachee game;
    public Camera cam;

    public GameObject menu;

    List<Objects> objects;
    List<TextMeshProUGUI> nameobjs;

    private void Start()
    {
        objects = game.objs;
        nameobjs = game.Nameobjs;
    }

    bool Detection(GameObject obj)
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 positionMin = cam.WorldToScreenPoint(obj.GetComponent<Renderer>().bounds.min);
        Vector3 positionMax = cam.WorldToScreenPoint(obj.GetComponent<Renderer>().bounds.max);

        bool InY = positionMin.y <= mouse.y && positionMax.y >= mouse.y;
        bool InX = positionMin.x <= mouse.x && positionMax.x >= mouse.x;

        Debug.Log(positionMin.y + " <= " + mouse.y + " && " + positionMax.y +  " >= " + mouse.y);
        Debug.Log(positionMin.x + " <= " + mouse.x + " && " + positionMax.x +  " >= " + mouse.x);
        return InY && InX;
    }

    void DetectionObject()
    {
        if (objects.Count <= 0)
            return;

        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].gameObject.activeSelf == false)
                continue;

            if (Detection(objects[i].gameObject))
            {
                objects[i].gameObject.SetActive(false);
                
                if (objects[i] == objects[i])
                {
                    nameobjs[i].fontStyle = FontStyles.Strikethrough;
                    return;
                }
                               
            }
        }
    }

    void DetectionMenu()
    {
        if (Detection(menu))
        {
            Debug.Log("GO MENU");

        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // a changer
        {
            DetectionObject();
            //DetectionMenu();
        }
    }
}
