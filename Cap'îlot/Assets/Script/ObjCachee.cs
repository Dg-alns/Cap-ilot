using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class ObjCachee : MonoBehaviour
{
    public Timer timer;
    public GameObject diabetes;

    void Update()
    {
        timer.UpdateTimer();

        diabetes.GetComponent<Diabète>().GoToPosition();
    }
}
