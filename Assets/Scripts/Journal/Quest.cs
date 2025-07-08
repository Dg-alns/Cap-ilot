using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class QuestManager
{
    public List<Quest> quests;
    public SerializableDictionary<int, bool> statusDict;
    private Saving SaveData { get; set; }
    public QuestManager() 
    {
        quests = new List<Quest>();
        statusDict = new SerializableDictionary<int, bool>();
        quests.Add(new ExampleQuest());
        //add all quests
        for (int i = 0; i < quests.Count; i++)
        {
            statusDict.Add(i, quests[i].status);
        }
    }
}

public class Quest
{
    public bool status;
    //système de reward à ajouters
    public Quest()
    {
        status = false;
    }
    virtual public bool CheckCondition(Saving Data)
    {
        return false;
    }
}

public class ExampleQuest : Quest
{
    override public bool CheckCondition(Saving Data)
    {
        if (Data.profile.Username != "")
        {
            status = true;
            return true;
        }
        return false;
    }
}
