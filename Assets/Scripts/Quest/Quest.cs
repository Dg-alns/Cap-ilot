using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D;

public enum QUESTS
{
    None,
    ReparationPhare,
    Maison,
    DemandeCapitaine,
    Hopital,
    Phare,
    Alimentation,
    A_Ressource,
    Ecole,
    E_Ressource,
    Sport,
    S_Ressource,
    Relation,
    R_Ressoucre
}

[System.Serializable]
public class QuestManager
{
    public List<Quest> quests;
    public SerializableDictionary<int, bool> statusDict;
    private Saving SaveData { get; set; }

    static string namePlayerPrefQuest = "PlayerPrefsQuest";

    public QuestManager()
    {
        quests = new List<Quest>();
        statusDict = new SerializableDictionary<int, bool>();

        InitQuest(QUESTS.None);
        InitQuest(QUESTS.ReparationPhare);

        //quests.Add(new PhareQuest((int)QUESTS.Maison, new ExampleReward(null, QUESTS.Maison.ToString())));
        InitQuest(QUESTS.Maison);
        InitQuest(QUESTS.DemandeCapitaine);
        InitQuest(QUESTS.Hopital);
        InitQuest(QUESTS.Phare);
        InitQuest(QUESTS.Alimentation);
        InitQuest(QUESTS.A_Ressource);
        InitQuest(QUESTS.Ecole);
        InitQuest(QUESTS.E_Ressource);
        InitQuest(QUESTS.Sport);
        InitQuest(QUESTS.S_Ressource);
        InitQuest(QUESTS.Relation);
        InitQuest(QUESTS.R_Ressoucre);


        //add all quests
        foreach (Quest quest in quests)
        {
            if (!statusDict.ContainsKey(quest.id))
            {
                statusDict[quest.id] = quest.status;
            }
            else
            {
                int tmpId = quest.id;
                while (true)
                {

                    if (!statusDict.ContainsKey(tmpId))
                    {
                        statusDict[tmpId] = quest.status;
                        break;
                    }
                    tmpId++;

                    if (tmpId > statusDict.Count)
                        break;
                }
            }
        }
    }

    

    public static void NextQuest()
    {
        PlayerPrefs.SetInt(namePlayerPrefQuest, PlayerPrefs.GetInt(namePlayerPrefQuest) + 1);
        Debug.Log("Nm Quest  ==  " + PlayerPrefs.GetInt(namePlayerPrefQuest));
    }

    void InitQuest(QUESTS quest, Sprite sprite = null)
    {
        quests.Add(new ExampleQuest((int)quest, new ExampleReward(sprite, quest.ToString())));
    }

    public static int GetPlayerPref() 
    {
        if (PlayerPrefs.HasKey(namePlayerPrefQuest) == false)
        {
            Debug.Log("re");
            PlayerPrefs.SetInt(namePlayerPrefQuest, 0);
        }

        return PlayerPrefs.GetInt(namePlayerPrefQuest); 
    }

    public static int GetQUESTS(QUESTS quest) { return (int)quest; }
    public static void SetQuest(QUESTS quest) //TODO for debug game
    {

        PlayerPrefs.SetInt(namePlayerPrefQuest, (int)quest);
    }
}

public class Quest
{
    public bool status;
    public int id;
    public Reward reward;
    public Quest(int id, Reward reward)
    {
        status = false;
        this.id = id;
        this.reward = reward;
    }
    virtual public bool CheckCondition(Saving Data)
    {
        return false;
    }
}

public class ExampleQuest : Quest
{
    public ExampleQuest(int id, Reward reward) : base(id, reward)
    {
    }
    override public bool CheckCondition(Saving Data)
    {
        if (Data.profile.Username != "")
        {
            status = true;
            reward.Obtain(Data);
            return true;
        }
        return false;
    }
}

//public class PhareQuest : Quest
//{
//    public PhareQuest(int id, Reward reward) : base(id, reward)
//    {
//    }
//    override public bool CheckCondition(Saving Data)
//    {

//        if (PlayerPrefs.GetInt("ReparationPhare") == 1)
//        {
//            Debug.Log("Condition True Next Quest");
//            return true;
//        }
//        //    if (Data.profile.Username != "")
//        //{
//        //    status = true;
//        //    reward.Obtain(Data);
//        //    return true;
//        //}
//        return false;
//    }
//}