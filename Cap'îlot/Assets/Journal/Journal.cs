using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Journal
{
    public TextMeshProUGUI InputField { get; set; }
    public SerializableDictionary<string, string> journal;
    // Start is called before the first frame update
    public Journal()
    {
        journal = new SerializableDictionary<string, string>();
    }
    public void Save()
    {
        journal[DateTime.Now.ToString()] = InputField.text;
    }
}
