using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ObjCachee : MonoBehaviour
{
    List<Objects> objs;
    List<TextMeshProUGUI> Nameobjs;
    List<Infos_MiniJeux> allinfos;

    public Timer timer;
    public GameObject diabetes;

    public List<Objects> GetAllObjToFind() {  return objs; }
    public List<TextMeshProUGUI> GetAllText() {  return Nameobjs; }
    public List<Infos_MiniJeux> GetAllInfos() {  return allinfos; }

    //GetComponentsInChildren utiliser pour le regroupement de toute les props use find un parent
    private void Awake()
    {
        objs = Tools.CreateList<Objects>("ToFind");
        Nameobjs = Tools.CreateList<TextMeshProUGUI>("Bot");
        allinfos = Tools.CreateList<Infos_MiniJeux>("Canvas (1)");

        foreach(Infos_MiniJeux infos in allinfos)
        {
            infos.gameObject.SetActive(false);
        }

        Assert.AreEqual(objs.Count, Nameobjs.Count);

        for (int i = 0; i < objs.Count; i++)
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
