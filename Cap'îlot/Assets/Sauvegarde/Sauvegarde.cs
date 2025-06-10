using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Sauvegarde : MonoBehaviour
{
    public Journal journal;
    // Start is called before the first frame update
    public void Save()
    {
        journal.Save();
        string jsonString = JsonUtility.ToJson(journal);
        string fileName = "save.json";
        File.WriteAllText(fileName, jsonString);
    }
}
