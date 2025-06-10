using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEngine.Rendering.DebugUI;
using TMPro;

public class Sauvegarde : MonoBehaviour
{
    private Journal journal;
    [SerializeField] private TextMeshProUGUI Input;
    // Start is called before the first frame update

    private void Awake()
    {
        journal = new Journal();
        try
        {
            string jsonstring = File.ReadAllText("C:/Users/ederveaux/Documents/GitHub/Cap-ilot/Cap'îlot/save.json");
            journal = JsonUtility.FromJson<Journal>(jsonstring);
        }
        catch
        {

        }
        journal.InputField = Input;
    }
    public void Save()
    {
        journal.Save();
        string jsonString = JsonUtility.ToJson(journal);
        string fileName = "save.json";
        File.WriteAllText(fileName, jsonString);
    }
}
