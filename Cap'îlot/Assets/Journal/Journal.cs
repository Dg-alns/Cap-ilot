using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI InputField;
    public SerializableDictionary<string, string> journal;
    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            JsonUtility.FromJson<Journal>("save.json");
        }
        catch 
        {
            journal = new SerializableDictionary<string, string>();
        }
    }
    public void Save()
    {
        journal[DateTime.Now.ToString()] = InputField.text;
    }
}
