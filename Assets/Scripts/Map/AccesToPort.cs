using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccesToPort : MonoBehaviour
{
    public Port port;
    public LoadNexScene loadNexScene;
   
    public void LoadScene()
    {
        if (port.isDiscover)
            loadNexScene.LoadNextScene(port.IleName);
        else
            loadNexScene.LoadNewIle(port.IleName);
    }
}
