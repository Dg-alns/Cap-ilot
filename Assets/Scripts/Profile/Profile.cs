using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[System.Serializable]
public class Profile
{
    public string Username;
    public int Age;
    public int DayBirth;
    public int MonthBirth;
    public int YearBirth;
    public string EmailDoctor;
    public TextMeshProUGUI Input {  get; set; }
    public TextMeshProUGUI Output { get; set; }
    public TMP_Dropdown Jour { get; set; }
    public TMP_Dropdown Mois { get; set; }
    public TMP_Dropdown Annee { get; set; }
    public TextMeshProUGUI OutputDate { get; set; }
    public Profile()
    {
        Username = string.Empty;
        Age = 0;
    }
    public void UpdateProfile()
    {
        if (Output != null)
        {
            Output.text = Username;
            OutputDate.text = Age + " ans";
        }
    }

    public void AssignAge(bool isDropdownValue = true)
    {
        DateTime dateTime = DateTime.Today;
        DateTime birth;
        if (isDropdownValue)
            birth = DateTime.Parse(DayBirth + "/" + MonthBirth + "/" + (YearBirth+1949));
        else
            birth = DateTime.Parse(DayBirth + "/" + MonthBirth + "/" + YearBirth);
        TimeSpan AgeT = dateTime - birth;
        Age = AgeT.Days / 365;
    }

    public void Save()
    {
        if (Input != null)
        {
            if (Input.text.Length > 1)
            {
                Username = Input.text;
            }
            DateTime dateTime = DateTime.Today;
            DateTime birth = new DateTime(dateTime.Year - (Annee.value - 1), Mois.value, Jour.value);
            TimeSpan AgeT = dateTime - birth;
            Age = AgeT.Days / 365;
        }
    }
}
