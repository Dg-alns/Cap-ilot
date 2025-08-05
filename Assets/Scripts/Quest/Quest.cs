using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class QuestManager
{
    public List<Quest> quests;
    public SerializableDictionary<int, bool> statusDict;
    public QuestManager()
    {
        quests = new List<Quest>();
        statusDict = new SerializableDictionary<int, bool>();
        quests.Add(new ExampleQuest(0, new ExampleReward(null, "test")));
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