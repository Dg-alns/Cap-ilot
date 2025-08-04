using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int idxOffSetDialogue = 0;

    public List<AllDialogueSet> dialogueSet = new List<AllDialogueSet>();

    string NPCPLayerPrefsName;

    private void Start()
    {
        NPCPLayerPrefsName = npcId.ToString() + npcName;

        PlayerPrefs.SetInt(NPCPLayerPrefsName, 0);

        if (PlayerPrefs.HasKey(NPCPLayerPrefsName))
        {
            idxOffSetDialogue = PlayerPrefs.GetInt(NPCPLayerPrefsName);

            Debug.Log(PlayerPrefs.GetInt(NPCPLayerPrefsName));
        }
    }

    public List<string> GetLstDialogue() { return dialogueSet[idxOffSetDialogue].dialogueLines; }
   
    public void NextSet() 
    {
        if (idxOffSetDialogue + 1 <= dialogueSet.Count -1)
        {
            idxOffSetDialogue++;
        }
        else
            idxOffSetDialogue += 0;
    }
    public void NextSetForCapitain() 
    {
        idxOffSetDialogue++;
        PlayerPrefs.SetInt(NPCPLayerPrefsName, idxOffSetDialogue);
            
    }

    public void UpInfoPLayerPrefs()
    {
        PlayerPrefs.SetInt(NPCPLayerPrefsName, (PlayerPrefs.GetInt(NPCPLayerPrefsName) + 1));
    }

    public void SetPLayerPrefs(int value)
    {
        PlayerPrefs.SetInt(NPCPLayerPrefsName, value);
    }

    public string GetNamePlayerPrefsNPC() { return NPCPLayerPrefsName; }
}
