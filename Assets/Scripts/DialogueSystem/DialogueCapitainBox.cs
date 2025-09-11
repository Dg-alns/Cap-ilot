using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using static UnityEngine.Rendering.DebugUI;

public class DialogueCapitainBox : DialogueBox
{

    public CapitainPort capitain;

    public override void EndDialogue()
    {
        Destroy(npcManager.dialogueNpc.GetComponent<Trigger>().activeUI);
        npcManager.dialogueNpc.GetComponent<Trigger>().activeUI = null;
        npcManager.dialogueNpc.GetComponent<Trigger>().uiOpen = false;

        npcManager.dialogueNpc.UpInfoPLayerPrefs();


        if (npcManager.dialogueNpc.idxOffSetDialogue > npcManager.dialogueNpc.dialogueSet.Count - 1)
        {
            npcManager.dialogueNpc.UpInfoPLayerPrefs();
            capitain.portIlePrincipale.ActivePhare();

            capitain.gameObject.SetActive(false);
            QuestManager.ValidateQuest(QUESTS.Introduction);
        }

        capitain.portIlePrincipale.ActivePancartePhare();
        capitain.SetCanTP(true);


    }

    public void InitCapitain(CapitainPort capitainPort)
    {
        capitain = capitainPort;
    }

    public CapitainPort GetCapitain() { return capitain; }
}
