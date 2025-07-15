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
        quests.Add(new ExampleQuest(0));
        //add all quests
        foreach (Quest quest in quests)
        {
            if (!statusDict.ContainsKey(quest.id))
            {
                statusDict[quest.id] = quest.status;
            }
            else
            {
                while (true)
                {
                    quest.id++;
                    if (!statusDict.ContainsKey(quest.id))
                    {
                        statusDict[quest.id] = quest.status;
                        break;
                    }
                }
            }
        }
    }
}

public class Quest
{
    public bool status;
    public int id;
    //système de reward à ajouters
    public Quest(int id)
    {
        status = false;
        this.id = id;
    }
    virtual public bool CheckCondition(Saving Data)
    {
        return false;
    }
}

public class ExampleQuest : Quest
{
    public ExampleQuest(int id) : base(id)
    {
    }
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