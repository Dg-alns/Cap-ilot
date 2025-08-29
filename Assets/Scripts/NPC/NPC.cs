using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class AllDialogueSet
{
    [TextArea]
    public List<string> dialogueLines;
}

public class NPC : MonoBehaviour
{
    public int npcId;
    public string npcName;
    public int idxOffSetDialogue;

    public List<AllDialogueSet> dialogueSet = new List<AllDialogueSet>();

    protected string NPCPLayerPrefsName;

    private void Awake()
    {
        NPCPLayerPrefsName = npcId.ToString() + npcName;

        if (PlayerPrefs.HasKey(NPCPLayerPrefsName))
        {
            idxOffSetDialogue = PlayerPrefs.GetInt(NPCPLayerPrefsName);
        }
        else
        {
            PlayerPrefs.SetInt(NPCPLayerPrefsName, 0);
        }
    }

    public List<string> GetLstDialogue() { return dialogueSet[idxOffSetDialogue].dialogueLines; }
   
    public virtual bool NextSet() 
    {
        if (idxOffSetDialogue + 1 <= dialogueSet.Count -1)
        {
            idxOffSetDialogue++;
            return true;
        }
        else
            idxOffSetDialogue += 0;


        PlayerPrefs.SetInt(NPCPLayerPrefsName, idxOffSetDialogue);
        return false;
    }
    public void NextSetForCapitain() 
    {
        idxOffSetDialogue++;
        PlayerPrefs.SetInt(NPCPLayerPrefsName, idxOffSetDialogue);
            
    }

    public void UpInfoPLayerPrefs()
    {
        PlayerPrefs.SetInt(NPCPLayerPrefsName, (PlayerPrefs.GetInt(NPCPLayerPrefsName) + 1));
        idxOffSetDialogue += 1;
    }

    public void SetPLayerPrefs(int value)
    {
        PlayerPrefs.SetInt(NPCPLayerPrefsName, value);
        idxOffSetDialogue = value;
    }

    public int GetPlayerPrefs()
    {
        return PlayerPrefs.GetInt(NPCPLayerPrefsName);
    }

    public string GetNamePlayerPrefsNPC() { return NPCPLayerPrefsName; }
    public int GePlayerPrefsNPC()
    {
        if (PlayerPrefs.HasKey(NPCPLayerPrefsName) == false) 
            PlayerPrefs.SetInt(NPCPLayerPrefsName, 0); 

        return PlayerPrefs.GetInt(NPCPLayerPrefsName);  
    }
}
