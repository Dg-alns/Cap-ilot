using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class CapitainPort : MonoBehaviour
{
    public SpawnManagement spawnManagement;
    public PortIlePrincipale portIlePrincipale;

    public DialogueNPC dialogue;
    public SpriteRenderer spriteRenderer;


    bool canTP = false;

    Vector3 destination;

    private void Start()
    {

        if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.ReparationPhare))
        {
            gameObject.SetActive(false);
        }


        //    if (PlayerPrefs.HasKey(dialogue.GetNamePlayerPrefsNPC()))
        //{
        //    if (PlayerPrefs.GetInt(dialogue.GetNamePlayerPrefsNPC()) > 1)
        //    {

        //        portIlePrincipale.ActivePhare();
        //        gameObject.SetActive(false);
        //    }
        //}

        canTP = false;


        if (destination != spawnManagement.GetPosSpeCapitain("TutoPancarte"))
            destination = spawnManagement.GetPosSpeCapitain("TutoPancarte");

        if (PlayerPrefs.HasKey(dialogue.GetNamePlayerPrefsNPC()))
        {
            if (PlayerPrefs.GetInt(dialogue.GetNamePlayerPrefsNPC()) == 1)
            {
                portIlePrincipale.ActivePancartePhare();
            }
        }
    }

    void Update()
    {
        if (canTP)
            TPtoDestination();
    }


    void TPtoDestination()
    {
        transform.position = destination;
    }

    public DialogueNPC GetDialogue() { return dialogue; }

    public void SetCanTP(bool value) {  canTP = value; }
}
