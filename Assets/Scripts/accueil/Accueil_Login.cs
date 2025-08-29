using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;

enum Debug_Login
{
    USERNAME_ERROR = 0,
    BIRTHDATE_ERROR,
    BIRTHDATE_INEXISTANT,
    EMAIL_ERROR,

    SAUVEGARDE,

    COUNT

}

public class Accueil_Login : MonoBehaviour
{
    [Header("Login Window")]
    [SerializeField] TMP_InputField _username;
    [SerializeField] GameObject _birthDropDownsParent;
    [SerializeField] TMP_InputField _doctorMail;
     
    [SerializeField] TextMeshProUGUI _DebugText;


    private List<string> _DebugList = new List<string>() { 
        "Le nom du joueur incorrect",
        "La date de naissance est incorrect !",
        "La date de naissance entré ne peut exister !",
        "L'adresse email entré n'est pas valide !",
        "Vos données ont été sauvegardé !"
    };

    // BirthDate
    private List<TMP_Dropdown> _dropDowns;
    private string birthInput;

    // Email
    string MatchEmailPattern;

    private Saving save; 
    private bool _dataLoaded;

    private void Start()
    {
        Init();

        gameObject.SetActive(false);
        _DebugText.gameObject.SetActive(false);
    }

    private void Init()
    {
        string jsonstring = File.ReadAllText(Application.dataPath + "/JSON/Save.json");
        save = JsonUtility.FromJson<Saving>(jsonstring);

        _dropDowns = Tools.CreateList<TMP_Dropdown>(_birthDropDownsParent);

        FillDate();

        MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        
        LoadData();
        _dataLoaded = true;
    }

    // Fill days, months and years 
    private void FillDate()
    {
        string firstOption = "-----";

        // Create Days Dropdown
        List<string> options = new List<string>() { firstOption };
        for (int i = 1; i <= 31; i++)
        {
            options.Add(i.ToString());
        }
        _dropDowns[0].AddOptions(options);

        // Create Month Dropdown
        options.Clear();
        options.Add(firstOption);
        options.AddRange(new string[] { "Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" });
        _dropDowns[1].AddOptions(options);

        // Create Years Dropdown
        options.Clear();
        options.Add(firstOption);
        int Years = DateTime.Now.Year;
        for (int i = Years ; i > 1900; i--)
        {
            options.Add(i.ToString());
        }
        _dropDowns[2].AddOptions(options);
    }

    public void OpenWindow()
    { 
        gameObject.SetActive(true); 
        if(!_dataLoaded)
            Init();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        _DebugText.gameObject.SetActive(false);
    }

    public void GoToGame()
    {
        if (CanGoInGame())
        {
            LoadNexScene loadNexScene = FindAnyObjectByType<LoadNexScene>();
            loadNexScene.StartGame();
        }
    }

    public bool CanGoInGame()
    {
        if(string.IsNullOrEmpty(save.profile.Username))
        {
            OpenWindow();
            return false;
        }
        return true;
    }

    public void SaveData()
    {
        if (!CanSaveData())
            return;

        save.profile.Username = _username.text;

        save.profile.DayBirth = _dropDowns[0].value;
        save.profile.MonthBirth = _dropDowns[1].value;
        save.profile.YearBirth = _dropDowns[2].value;

        save.profile.AssignAge(true);

        save.profile.EmailDoctor = _doctorMail.text ;


        JSON_Manager.SaveData("Save", save);
    }

    private bool CanSaveData()
    {
        string mail = _doctorMail.text;

        // Check Username
        if(string.IsNullOrEmpty(_username.text.Replace(" ","")))
        {
            ActiveDebugText(Debug_Login.USERNAME_ERROR);
            return false;
        }

        // Check Birthday
        foreach(TMP_Dropdown dropdown in _dropDowns)
        {
            if(dropdown.value == 0)
            {
                ActiveDebugText(Debug_Login.BIRTHDATE_ERROR);
                return false;
            }
        }

        birthInput = _dropDowns[0].value + "/" + _dropDowns[1].value + "/" + _dropDowns[2].value;
        if (!DateTime.TryParse(birthInput, out DateTime date))
        {
            ActiveDebugText(Debug_Login.BIRTHDATE_INEXISTANT);
            return false;
        }
        
        // Check Email
        if (!string.IsNullOrEmpty(mail))
            if (!Regex.IsMatch(mail, MatchEmailPattern))
            {
                ActiveDebugText(Debug_Login.EMAIL_ERROR);
                return false;
            }

        ActiveDebugText(Debug_Login.SAUVEGARDE);
        return true;
    }

    private void LoadData()
    {
        _username.text      = save.profile.Username;

        _dropDowns[0].value = save.profile.DayBirth;
        _dropDowns[1].value = save.profile.MonthBirth;
        _dropDowns[2].value = save.profile.YearBirth;

        _doctorMail.text    = save.profile.EmailDoctor;
    }

    private void ActiveDebugText(Debug_Login debug_Login)
    {
        _DebugText.text = _DebugList[(int)debug_Login];
        _DebugText.gameObject.SetActive(true);
    }
}
