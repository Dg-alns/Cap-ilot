using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class DialogueMiniGameBox : DialogueBox
{
    public GameObject buttonStart;
    public GameObject buttonReturn;

    MiniGameNPC npc;

    bool activeButton;

    bool _isPostMinigameDialogue = false;

    public void Init(bool isPostMinigameDialogue = false)
    {
        dialogStarted = false;
        /*FindNPCManagerInActiveScene();
        AssignTextAreas();*/
        dialogueGroup = GetComponent<CanvasGroup>();

        buttonStart.SetActive(false);
        buttonReturn.SetActive(false);

        npc = npcManager.dialogueNpc as MiniGameNPC;

        activeButton = false;

        _isPostMinigameDialogue = isPostMinigameDialogue;

        PlayerPrefs.DeleteKey(npc.GetNamePlayerPrefsNPC());

        if(PlayerPrefs.HasKey(npc.GetNamePlayerPrefsNPC()) == false)
            PlayerPrefs.SetInt(npc.GetNamePlayerPrefsNPC(), 0);
    }

    private void Update()
    {
        if (activeButton == false)
            ActiveButton();
    }

    void ActiveButton()
    {
        if(npc.idxMiniGameSet == npc.idxOffSetDialogue)
        {
            activeButton = true;
            buttonStart.SetActive(true);
            buttonReturn.SetActive(true);

            Debug.Log("Active Button : " + PlayerPrefs.GetInt(npc.GetNamePlayerPrefsNPC()));

            PlayerPrefs.SetInt(npc.GetNamePlayerPrefsNPC(), 1);
        }
    }



    public override void GetDialogueLines()
    {
        lineList = new List<string>();
        if (npc == null)
        {
            Debug.LogError("ERROR : Dialogue NPC not found");
        }

        if (PlayerPrefs.GetInt(npc.GetNamePlayerPrefsNPC()) == npc.idxMiniGameSet)
            lineList = npcManager.dialogueNpc.dialogueSet[npc.idxMiniGameSet].dialogueLines;
        else
            lineList = npcManager.dialogueNpc.GetLstDialogue();
    }

    public override void EndDialogue()
    {
        if (buttonStart.activeSelf == false)
        {
            //DestroiUI();
            npcManager.dialogueNpc.NextSet();
            GetDialogueLines();

            _lineIndex = 0;
            _dialogueText.SetText(lineList[_lineIndex]);
            dialogStarted = true;
        }
        if (_isPostMinigameDialogue)
        {
            DestroiUI();
            npc.SetPLayerPrefs(1);
            npc.idxOffSetDialogue = 1;
            _isPostMinigameDialogue = false;
        }
    }

    public void DestroiUI()
    {
        Destroy(npcManager.dialogueNpc.GetComponent<Trigger>().activeUI);
        npcManager.dialogueNpc.GetComponent<Trigger>().activeUI = null;
        npcManager.dialogueNpc.GetComponent<Trigger>().uiOpen = false;
    }

    public void LoadMiniGame()
    {
        npc.loadNexScene.LoadMiniGame(npc.MiniGameName);
    }

    public MiniGameNPC GetNPC() { return npc; }
}
