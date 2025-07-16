using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.CameraUI;

[System.Serializable]
public class Journal
{
    public TextMeshProUGUI InputField {  get; set; }
    public TMP_Dropdown Output {  get; set; }
    public TextMeshProUGUI OutputText { get; set; }
    private List<string> OutputList;
    public SerializableDictionary<string, string> journal;
    public Wheel EmotionWheel { get; set; }
    public List<string> ThemeList { get; set; }
    // Start is called before the first frame update
    public Journal()
    {
        journal = new SerializableDictionary<string, string>();
    }
    public void UpdateJournal()
    {
        if (Output != null)
        {
            if (journal.Keys.Count > 0)
            {
                Output.ClearOptions();
                List<string> keyList = new List<string>();
                foreach (string key in journal.Keys)
                {
                    keyList.Add(key);
                }
                Output.AddOptions(keyList);
                OutputList = new List<string>();
                foreach (TMP_Dropdown.OptionData option in Output.options)
                {
                    OutputList.Add(journal[option.text]);
                }
                OutputText.text = OutputList[0];
                Output.RefreshShownValue();
            }
        }
    }
    public void Save()
    {
        if (InputField != null)
        {
            if (InputField.text != null)
            {
                string themeSTR = "Thèmes : ";
                foreach (string theme in ThemeList)
                {
                    themeSTR += theme + " ";
                }
                journal[DateTime.Today.ToString("d")] = themeSTR + "\n" + EmotionWheel.ActualEmoji + "\n" + InputField.text;
            }
        }
    }
    public void DropdownValueChanged(TMP_Dropdown change)
    {
        OutputText.text = OutputList[Output.value];
    }
}