using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEngine.Rendering.DebugUI;
using TMPro;
using UnityEngine.UI;

public class Sauvegarde : MonoBehaviour
{
    private Journal journal;
    [SerializeField] private TextMeshProUGUI Input;
    [SerializeField] private TMP_Dropdown Output;
    [SerializeField] private TextMeshProUGUI OutputText;
    [SerializeField] private Wheel EmotionWheel;
    [SerializeField] UnityEngine.UI.Button Hospital;
    [SerializeField] UnityEngine.UI.Button Food;
    [SerializeField] UnityEngine.UI.Button Sport;
    [SerializeField] UnityEngine.UI.Button School;
    [SerializeField] UnityEngine.UI.Button Relations;
    [SerializeField] UnityEngine.UI.Button Temptations;
    // Start is called before the first frame update

    private void Awake()
    {
        journal = new Journal();
        journal.Output = Output;
        journal.EmotionWheel = EmotionWheel;
        journal.OutputText = OutputText;
        try
        {
            string jsonstring = File.ReadAllText("save.json");
            journal = JsonUtility.FromJson<Journal>(jsonstring);
            journal.Output = Output;
            journal.EmotionWheel = EmotionWheel;
            journal.OutputText = OutputText;
            journal.UpdateJournal();
        }
        catch
        {

        }
        journal.InputField = Input;
        Output.onValueChanged.AddListener(delegate {
            journal.DropdownValueChanged(Output);
        });
    }
    public void Save()
    {
        journal.Save();
        string jsonString = JsonUtility.ToJson(journal);
        string fileName = "save.json";
        File.WriteAllText(fileName, jsonString);
        journal.UpdateJournal();
    }
}
