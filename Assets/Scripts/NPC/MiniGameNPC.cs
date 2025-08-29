using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameNPC : NPC 
{
    public int idxMiniGameSet;
    public string MiniGameName;
    public LoadNexScene loadNexScene;


    public override bool NextSet()
    {
        if (idxOffSetDialogue + 1 <= dialogueSet.Count - 1)
        {
            idxOffSetDialogue++;
            return true;
        }
        else
            idxOffSetDialogue = idxMiniGameSet;


        PlayerPrefs.SetInt(NPCPLayerPrefsName, idxOffSetDialogue);
        return false;
    }
}
