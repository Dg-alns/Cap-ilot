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
    private List<string> OutputList;
    // Start is called before the first frame update

    private void Awake()
    {
        journal = new Journal();
        try
        {
            string jsonstring = File.ReadAllText("save.json");
            journal = JsonUtility.FromJson<Journal>(jsonstring);
            Output.ClearOptions();
            List<string> keyList = new List<string>();
            foreach (string key in journal.journal.Keys)
            {
                keyList.Add(key);
            }
            Output.AddOptions(keyList);
            OutputList = new List<string>();
            foreach (TMP_Dropdown.OptionData option in Output.options)
            {
                OutputList.Add(journal.journal[option.text]);
            }
            OutputText.text = OutputList[Output.value];
            Output.RefreshShownValue();
        }
        catch
        {

        }
        journal.InputField = Input;
        Output.onValueChanged.AddListener(delegate {
            DropdownValueChanged(Output);
        });
    }
    public void Save()
    {
        Output.ClearOptions();
        journal.Save();
        string jsonString = JsonUtility.ToJson(journal);
        string fileName = "save.json";
        File.WriteAllText(fileName, jsonString);
        List<string> keyList = new List<string>();
        foreach (string key in journal.journal.Keys)
        {
            keyList.Add(key);
        }
        Output.AddOptions(keyList);
        OutputList = new List<string>();
        foreach (TMP_Dropdown.OptionData option in Output.options)
        {
            OutputList.Add(journal.journal[option.text]);
        }
        OutputText.text = OutputList[Output.value];
        Output.RefreshShownValue();
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        OutputText.text = OutputList[Output.value];
    }
}
