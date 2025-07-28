using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
//using static QuestManager;
using UnityEngine.UI;
//using static UnityEngine.InputManagerEntry;
//using UnityEditor.UIElements;

public class Sauvegarde : MonoBehaviour
{
    private Journal journal;
    private Profile profile;
    private QuestManager questManager;
    [SerializeField] TextMeshProUGUI Input;
    [SerializeField] TMP_Dropdown Output;
    [SerializeField] TextMeshProUGUI OutputText;
    [SerializeField] Wheel EmotionWheel;
    [SerializeField] UnityEngine.UI.Button Hospital;
    [SerializeField] UnityEngine.UI.Button Food;
    [SerializeField] UnityEngine.UI.Button Sport;
    [SerializeField] UnityEngine.UI.Button School;
    [SerializeField] UnityEngine.UI.Button Relations;
    [SerializeField] UnityEngine.UI.Button Temptations;
    private List<string> Themes = new List<string>();
    [SerializeField] TextMeshProUGUI InputName;
    [SerializeField] TextMeshProUGUI OutputName;
    [SerializeField] TMP_Dropdown Jour;
    [SerializeField] TMP_Dropdown Mois;
    [SerializeField] TMP_Dropdown Annee;
    [SerializeField] TextMeshProUGUI OutputDate;
    // Start is called before the first frame update

    private void Awake()
    {
        journal = new Journal();
        profile = new Profile();
        questManager = new QuestManager();
        try
        {
            string jsonstring = File.ReadAllText("save.json");
            Saving save = new Saving(journal, profile, questManager);
            save = JsonUtility.FromJson<Saving>(jsonstring);
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Journal"))
            {
                SceneManager.LoadScene("Journal");
            }
            journal = save.journal;
            profile = save.profile;
            journal.Output = Output;
            journal.EmotionWheel = EmotionWheel;
            journal.OutputText = OutputText;
            journal.UpdateJournal();
            profile.Output = OutputName;
            profile.OutputDate = OutputDate;
            profile.UpdateProfile();
            journal.InputField = Input;
            Output.onValueChanged.AddListener(delegate {
                journal.DropdownValueChanged(Output);
            });
            Hospital.onClick.AddListener(() => { OnClick(Hospital); });
            Food.onClick.AddListener(() => { OnClick(Food); });
            Sport.onClick.AddListener(() => { OnClick(Sport); });
            School.onClick.AddListener(() => { OnClick(School); });
            Relations.onClick.AddListener(() => { OnClick(Relations); });
            Temptations.onClick.AddListener(() => { OnClick(Temptations); });
        }
        catch
        {
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Profile"))
            {
                SceneManager.LoadScene("Profile");
            }
            profile.Input = InputName;
            profile.Jour = Jour;
            profile.Mois = Mois;
            profile.Annee = Annee;
        }
    }

    public void Update()
    {
        Saving save = new Saving(journal, profile, questManager);
        foreach (Quest quest in questManager.quests)
        {
            if (quest.CheckCondition(save))
            {
                questManager.statusDict[quest.id] = true;
            }
        }
    }

    public void OnClick(UnityEngine.UI.Button pressed)
    {
        string theme = pressed.GetComponentInChildren<TextMeshProUGUI>().text;
        if (Themes.Contains(theme))
        {
            Themes.Remove(theme);
        }
        else
        {
            Themes.Add(theme);
        }
    }
    public void Save()
    {
        profile.Save();
        journal.ThemeList = Themes;
        journal.Save();
        Saving save = new Saving(journal, profile, questManager);
        string jsonString = JsonUtility.ToJson(save);
        string fileName = "save.json";
        File.WriteAllText(fileName, jsonString);
        journal.UpdateJournal();
        profile.UpdateProfile();
        Themes.Clear();
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Journal"))
        {
            SceneManager.LoadScene("Journal");
        }
    }
}

[System.Serializable]
public class Saving
{
    public Journal journal;
    public Profile profile;
    public QuestManager questManager;
    public Saving(Journal journal, Profile profile, QuestManager questManager) 
    { 
        this.journal = journal;
        this.profile = profile;
        this.questManager = questManager;
    }

}
