using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Services.Friends.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D;

public enum QUESTS
{
    Introduction,
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
    R_Ressoucre,
    Tentation,

    Count
}

[System.Serializable]
public class QuestManager
{
    public static List<Quest> quests;
    public SerializableDictionary<int, bool> statusDict;

    static string namePlayerPrefQuest = "PlayerPrefsQuest";
    static string TextPlayerPrefQuest = "PlayerPrefsQuestText";

    public QuestManager()
    {
        quests = new List<Quest>();
        statusDict = new SerializableDictionary<int, bool>();

        quests.Add(new GlobalQuest((int)QUESTS.Introduction, new ExampleReward(null, QUESTS.Introduction.ToString())));

        quests.Add(new GlobalQuest((int)QUESTS.ReparationPhare, new ExampleReward(null, QUESTS.ReparationPhare.ToString())));

        quests.Add(new GlobalQuest((int)QUESTS.Maison, new ExampleReward(null, QUESTS.Maison.ToString())));

        quests.Add(new GlobalQuest((int)QUESTS.DemandeCapitaine, new ExampleReward(null, QUESTS.DemandeCapitaine.ToString())));

        quests.Add(new GlobalQuest((int)QUESTS.Hopital, new ExampleReward(null, QUESTS.Hopital.ToString())));

        quests.Add(new GlobalQuest((int)QUESTS.Phare, new ExampleReward(null, QUESTS.Phare.ToString())));

        quests.Add(new GlobalQuest((int)QUESTS.Alimentation, new ExampleReward(null, QUESTS.Alimentation.ToString())));

        quests.Add(new /*RessourceQuest*/GlobalQuest((int)QUESTS.A_Ressource, new ExampleReward(null, QUESTS.A_Ressource.ToString())));  // Replace Null

        quests.Add(new GlobalQuest((int)QUESTS.Ecole, new ExampleReward(null, QUESTS.Ecole.ToString())));

        quests.Add(new GlobalQuest/*RessourceQuest*/((int)QUESTS.E_Ressource, new ExampleReward(null, QUESTS.E_Ressource.ToString())));  // Replace Null

        quests.Add(new GlobalQuest((int)QUESTS.Sport, new ExampleReward(null, QUESTS.Sport.ToString())));

        quests.Add(new /*RessourceQuest*/GlobalQuest((int)QUESTS.S_Ressource, new ExampleReward(null, QUESTS.S_Ressource.ToString())));  // Replace Null

        quests.Add(new GlobalQuest((int)QUESTS.Relation, new ExampleReward(null, QUESTS.Relation.ToString())));

        quests.Add(new /*RessourceQuest*/GlobalQuest((int)QUESTS.R_Ressoucre, new ExampleReward(null, QUESTS.R_Ressoucre.ToString())));  // Replace Null

        quests.Add(new GlobalQuest((int)QUESTS.Tentation, new ExampleReward(null, QUESTS.Tentation.ToString())));


        //add all quests
        foreach (Quest quest in quests)
        {
            if (!statusDict.ContainsKey(quest.id))
            {
                statusDict[quest.id] = quest.status;
            }
            
        }

    }    

    public static void NextQuest(int CurrentQuest)
    {
        PlayerPrefs.SetInt(namePlayerPrefQuest, CurrentQuest + 1);
    }

    public static int GetCurrentQuest() 
    {
        if (PlayerPrefs.HasKey(namePlayerPrefQuest) == false)
        {
            PlayerPrefs.SetInt(namePlayerPrefQuest, 0);
        }

        return PlayerPrefs.GetInt(namePlayerPrefQuest); 
    }

    public static int GetQUESTS(QUESTS quest) { return (int)quest; }

    public static void SetQuest(int quest)
    {

        PlayerPrefs.SetInt(namePlayerPrefQuest, quest);
    }

    public List<Quest> GetQuests() { return quests; }



    public static void ValidateQuest(QUESTS quest)
    {
        Debug.Log(quest.ToString() + " = true");
        quests[(int)quest].status = true;
    }

    public static void SetTextOffCurrentQuest(string text)
    {
        if (text.Equals(PlayerPrefs.GetString(TextPlayerPrefQuest)))
            return;


        PlayerPrefs.SetString(TextPlayerPrefQuest, text);
    }

    public static string GetTextOffCurrentQuest() => PlayerPrefs.GetString(TextPlayerPrefQuest);
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

public class GlobalQuest : Quest
{
    public GlobalQuest(int id, Reward reward) : base(id, reward)
    {
    }
    override public bool CheckCondition(Saving Data)
    {
        if (QuestManager.GetCurrentQuest() == id && status)
            return true;


        return false;
    }
}

public class RessourceQuest : Quest
{
    public RessourceQuest(int id, Reward reward) : base(id, reward)
    {
    }
    override public bool CheckCondition(Saving Data)
    {

        if (QuestManager.GetCurrentQuest() == id && status)
            return true;


        return false;

        //if (Data.profile.Username != "")
        //{
        //    status = true;
        //    reward.Obtain(Data);
        //    return true;
        //}
        //return false;
    }
}