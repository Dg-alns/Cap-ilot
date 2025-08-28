using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
//using static QuestManager;
using UnityEngine.UI;
//using static UnityEngine.InputManagerEntry;
//using UnityEditor.UIElements;

public class Sauvegarde : MonoBehaviour
{
    public Journal journal;
    public Profile profile;
    public QuestManager questManager;
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
    public SerializableDictionary<string, TemplateSaveMinigame> StatMinigame;
    public SerializableDictionary<string, TemplateSavePlayerData> StatPlayer;
    // Start is called before the first frame update

    private void Awake()
    {
        journal = new Journal();
        profile = new Profile();
        questManager = new QuestManager();
        StatMinigame = new SerializableDictionary<string, TemplateSaveMinigame>();
        StatPlayer = new SerializableDictionary<string, TemplateSavePlayerData>();
        try
        {
            Debug.Log("Catch1");
            Saving save = JSON_Manager.LoadData<Saving>("Save");
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Profile"))
            {
                SceneManager.LoadScene("Journal");
            }
            journal = save.journal;
            profile = save.profile;
            questManager = save.questManager;
            StatMinigame = save.statMinigame;
            StatPlayer = save.statPlayer;

            foreach (Quest quest in save.questManager.GetQuests())
            {
                if (save.questManager.statusDict[quest.id] == false)
                {
                    if (QuestManager.GetCurrentQuest() != quest.id)
                    {
                        QuestManager.SetQuest(quest.id);
                        Debug.Log( "CurrentQuest : " + QuestManager.GetCurrentQuest());
                    }
                    return;
                }
            }

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Journal"))
            {
                journal.Output = Output;
                journal.EmotionWheel = EmotionWheel;
                journal.OutputText = OutputText;
                journal.UpdateJournal();
                profile.Output = OutputName;
                profile.OutputDate = OutputDate;
                profile.UpdateProfile();
                journal.InputField = Input;
                Output.onValueChanged.AddListener(delegate
                {
                    journal.DropdownValueChanged(Output);
                });
                Hospital.onClick.AddListener(() => { OnClick(Hospital); });
                Food.onClick.AddListener(() => { OnClick(Food); });
                Sport.onClick.AddListener(() => { OnClick(Sport); });
                School.onClick.AddListener(() => { OnClick(School); });
                Relations.onClick.AddListener(() => { OnClick(Relations); });
                Temptations.onClick.AddListener(() => { OnClick(Temptations); });
            }
        }
        catch
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Journal"))
            {
                SceneManager.LoadScene("Profile");
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Profile"))
            {
                profile.Input = InputName;
                profile.Jour = Jour;
                profile.Mois = Mois;
                profile.Annee = Annee;
            }
        }
    }
    public void Update()
    {
        Saving save = new Saving(journal, profile, questManager, StatMinigame, StatPlayer);

        foreach (Quest quest in save.questManager.GetQuests())
        {
            if (save.questManager.statusDict[quest.id] == false)
            {
                Debug.Log("FirstQuest Fasle : " + quest.id);
                if (quest.CheckCondition(save))
                {
                    Debug.Log("ValidateQuest : " + quest.id);
                    questManager.statusDict[quest.id] = true;
                    QuestManager.NextQuest(quest.id);
                    Save(save);
                    Destroy(quest.reward.GO, 3);
                }
                return; // Stop au premier false
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
        Saving save = new Saving(journal, profile, questManager, StatMinigame, StatPlayer);
        //JSON_Manager.SaveData<Saving>(Application.dataPath + "/Json/Save.json", save);
        JSON_Manager.SaveData("Save", save);
        journal.UpdateJournal();
        profile.UpdateProfile();
        Themes.Clear();
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Profile"))
        {
            SceneManager.LoadScene("Journal");
        }
    }

    public void Save (Saving save)
    {
        journal = save.journal;
        profile = save.profile;
        questManager = save.questManager;
        StatMinigame = save.statMinigame;
        StatPlayer = save.statPlayer;
        JSON_Manager.SaveData("Save", save);
        //JSON_Manager.SaveData<Saving>(Application.dataPath + "/Json/Save.json", save);
    }
}

[System.Serializable]
public class Saving
{
    public Journal journal;
    public Profile profile;
    public QuestManager questManager;
    public SerializableDictionary<string, TemplateSaveMinigame> statMinigame;
    public SerializableDictionary<string, TemplateSavePlayerData> statPlayer;
    public Saving(Journal journal, Profile profile, QuestManager questManager, SerializableDictionary<string, TemplateSaveMinigame> statMinigame, SerializableDictionary<string, TemplateSavePlayerData> statPlayer)
    {
        this.journal = journal;
        this.profile = profile;
        this.questManager = questManager;
        this.statMinigame = statMinigame;
        this.statPlayer = statPlayer;
    }
    public int GetCompletedJournal()
    {
        return journal.journal.Count;
    }
}
