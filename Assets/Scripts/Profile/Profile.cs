using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Profile
{
    public string Username;
    public TextMeshProUGUI Input {  get; set; }
    public TextMeshProUGUI Output { get; set; }
    public Profile()
    {
        Username = string.Empty;
    }
    public void UpdateName()
    {
        Output.text = Username;
    }
    public void Save()
    {
        if (Input.text.Length > 1)
        {
            Username = Input.text;
        }
    }
}
