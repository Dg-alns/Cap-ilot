using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Journal_DownBar : MonoBehaviour
{
    private List<string> listTheme;

    [SerializeField] private Calendrier_DownBar calendrier;
    [SerializeField] private GameObject MarkPage_journal;

    [Header("Emotion")]
    [SerializeField] private EmotionWheel emotionWheel;

    [Header("Theme")]
    [SerializeField] private TMP_Dropdown dropdown_Theme;
    [SerializeField] private TMP_InputField inputField_Theme;

    [Header("Journal de bord")]
    [SerializeField] private TMP_InputField inputField_Journal;

    // Create an Warning in the console
    private Saving save;

    // Start is called before the first frame update
    void Start()
    {
        MarkPage_journal.SetActive(false);
        listTheme = new List<string>() { "- Thèmes", "Hopital", "Sport", "Ecole", "Alimentation", "Relation", "Tentation" };
        
        string jsonstring = File.ReadAllText(Application.dataPath + "/JSON/Save.json");
        save = JsonUtility.FromJson<Saving>(jsonstring);

        // If the player write something one day, we have to reload it the same day
        if (save.journal.journal.ContainsKey(DateTime.Today.ToString("d")))
        {
            // Get Saved journal data
            string[] data = save.journal.journal[DateTime.Today.ToString("d")].Split("\n");

            string theme = data[0];
            string emotion = data[1];
            string content = "";

            for (int i = 2; i < data.Length; i++) {
                content += data[i] + "\n";
            }

            // Try find theme and if not, add it in the list
            int index = listTheme.FindIndex(x => x.Equals(theme));
            int value = index;
            if(index < 0){
                listTheme.Add(theme); 
                value = listTheme.Count-1;
            }

            // Associate Themes to the dropdown
            dropdown_Theme.AddOptions(listTheme);
            dropdown_Theme.value = value;

            // Simulate the emotion click by the button num
            emotionWheel.OnClick(emotionWheel.GetNumeroEmotion(emotion));

            // Associate the content
            inputField_Journal.text = content;
            return;
        }

        // Initiate listTheme in the dropdown
        dropdown_Theme.AddOptions(listTheme);
    }

    // Update is called once per frame
    void Update()
    {
        if (MarkPage_journal.activeSelf)
            return;

        if(QuestManager.GetCurrentQuest() > (int)QUESTS.ReparationPhare)
            MarkPage_journal.SetActive(true);
    }

    public void AddTheme()
    {
        string newTheme = inputField_Theme.text;

        // Check if there a theme and a theme different to the other
        if (string.IsNullOrEmpty(newTheme)) return;
        if (listTheme.Contains(newTheme)) return;

        newTheme = newTheme.Replace("\n", " ");

        // Add the theme in the dropdown
        listTheme.Add(newTheme);
        dropdown_Theme.ClearOptions();
        dropdown_Theme.AddOptions(listTheme);
        inputField_Theme.text = "";
        dropdown_Theme.value = listTheme.Count - 1;
    }

    public void SaveJournal()
    {
        // Get value
        int valueDropdown = dropdown_Theme.value;
        string content = inputField_Journal.text;
        string emotion = emotionWheel.ActualEmoji;

        // Check if there is empty one
        if (!CanSaveJournal(valueDropdown, emotion, content)) return;

        string theme = dropdown_Theme.options[valueDropdown].text;

        Debug.Log("Thème : " + theme + "\n"
            + "Emotion : " + emotion + "\n"
            + "Content : " + content);

        // Combine data
        string totalContent = theme + "\n" + emotion + "\n" + content;

        // Add save in local Calender data
        calendrier.IsAddingJournalContent(totalContent);

        // Save DATA in JSON
        save.journal.journal[DateTime.Today.ToString("d")] = totalContent;
        JSON_Manager.SaveData<Saving>(Application.dataPath + "/JSON/Save.json",save);

        Debug.Log("Sauvegarde effectuer !");
    }
    bool CanSaveJournal(int valueTheme, string emotion, string content)
    {
        // Check if player select a theme
        if (valueTheme == 0)
        {
            dropdown_Theme.GetComponent<Image>().color = Color.red;
            return false;
        }
        dropdown_Theme.GetComponent<Image>().color = Color.white;

        // Check if player select an emotion
        if (string.IsNullOrEmpty(emotion))
        {
            emotionWheel.GetComponent<Image>().color = Color.red;
            return false;
        }
        emotionWheel.GetComponent<Image>().color = Color.white;

        // Check if player enter content in the journal
        if (string.IsNullOrEmpty(content))
        {
            inputField_Journal.GetComponent<Image>().color = Color.red;
            return false;
        }
        inputField_Journal.GetComponent<Image>().color = Color.white;

        return true;
    }
}
