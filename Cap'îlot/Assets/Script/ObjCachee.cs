using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ObjCachee : MonoBehaviour
{
    public List<GameObject> objs;
    public List<TextMeshProUGUI> Nameobjs;
    public List<Infos_MiniJeux> allinfos;

    public Timer timer;
    public GameObject diabetes;

    //GetComponentsInChildren utiliser pour le regroupement de toute les props use find un parent
    void Start()
    {
        Assert.AreEqual(objs.Count, Nameobjs.Count);

        for(int i = 0; i < objs.Count; i++)
        {
            Nameobjs[i].text = objs[i].name;
        }
    }

    void Update()
    {
        timer.UpdateTimer();

        diabetes.GetComponent<Diabète>().GoToPosition();
    }
}
