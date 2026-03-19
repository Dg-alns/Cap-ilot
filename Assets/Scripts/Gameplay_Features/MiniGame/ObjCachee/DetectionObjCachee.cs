using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetectionObjCachee : Minigame
{
    public Camera cam;
    public Timer timer;
    public Score score;
    public Sauvegarde_Minigame minigame;

    List<Objects> objects;
    List<TextMeshProUGUI> nameobjs;
    public Infos_MiniJeux infos;

    public GameObject diabete;

    private Tools _tools;

    private void Awake()
    {
        objects = Tools.CreateList<Objects>("ToFind");
        nameobjs = Tools.CreateList<TextMeshProUGUI>("Bot");

        _tools = FindAnyObjectByType<Tools>();

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


                objects[i].gameObject.SetActive(false);
                nameobjs[i].fontStyle = FontStyles.Strikethrough;
                if (minigame.GetCanShowInfo(SceneManager.GetActiveScene().name) == true)
                {
                    timer.stop = true;
                    infos.AssociateInfo(objects[i]);
                    infos.gameObject.SetActive(true); }
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
    public override void PauseMinigame()
    {
        timer.stop = true;
    }

    public override void ResumeMinigame()
    {
        timer.stop = false;
    }

}
