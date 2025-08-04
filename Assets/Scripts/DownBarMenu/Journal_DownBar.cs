
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Journal_DownBar : MonoBehaviour
{
    private List<string> listTheme = new List<string>() {"- Thèmes","Hopital","Sport","Ecole","Alimentation","Relation","Tentation"};

    [Header("Theme")]
    [SerializeField] private TMP_Dropdown dropdown_Theme = null;
    [SerializeField] private TMP_InputField inputField_Theme    = null;

    [Header("Journal de bord")]
    [SerializeField] private TMP_InputField inputField_Journal  = null;
    // Start is called before the first frame update
    void Start()
    {
        dropdown_Theme.AddOptions(listTheme);

        // If someone write something, we have to reload it the same day
        inputField_Journal.text = "If someone write something, we have to reload it the same day";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTheme()
    {
        string newTheme = inputField_Theme.text;

        if (string.IsNullOrEmpty(newTheme)) return;
        if (listTheme.Contains(newTheme)) return;

        listTheme.Add(newTheme);
        dropdown_Theme.ClearOptions();
        dropdown_Theme.AddOptions(listTheme);
        inputField_Theme.text = "";
    }

}
