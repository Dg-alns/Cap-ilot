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
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.ReparationPhare))
        {
            gameObject.SetActive(false);
        }

        canTP = false;

        destination = spawnManagement.GetPosSpeCapitain("TutoPancarte");

        if (PlayerPrefs.HasKey(dialogue.GetNamePlayerPrefsNPC()))
        {
            if (PlayerPrefs.GetInt(dialogue.GetNamePlayerPrefsNPC()) == 1)
            {
                TPtoDestination();
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
