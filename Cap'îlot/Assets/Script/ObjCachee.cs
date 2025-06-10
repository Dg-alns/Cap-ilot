using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ObjCachee : MonoBehaviour
{
    public List<Objects> objs;
    public List<TextMeshProUGUI> Nameobjs;


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
        
    }
}
