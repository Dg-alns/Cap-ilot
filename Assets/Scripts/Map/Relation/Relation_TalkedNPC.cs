using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relation_TalkedNPC : MonoBehaviour
{
    public bool alreadyTalked = false;
    NPCManager npcManager;
    string keyPlayerPrefs;

    // Start is called before the first frame update
    void Start()
    {
        keyPlayerPrefs = gameObject.name + "Talked";
        PlayerPrefs.DeleteKey(keyPlayerPrefs);
        if (PlayerPrefs.HasKey(keyPlayerPrefs))
        {
            alreadyTalked = PlayerPrefs.GetInt(keyPlayerPrefs) == 0 ? false : true;
            return;
        }

        npcManager = FindAnyObjectByType<NPCManager>();
        PlayerPrefs.SetInt(keyPlayerPrefs, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!alreadyTalked)
        {
            if (!npcManager.dialogueNpc)
                return;

            if(npcManager.dialogueNpc.name == gameObject.name)
            {
                PlayerPrefs.SetInt(keyPlayerPrefs, 1);
                alreadyTalked = true;
                FindAnyObjectByType<Relation_RessourcePNJ>().ConditionToGiveRessource();
            }
        }
    }
}
