using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class DialogueBox : MonoBehaviour
{
    public NPCManager npcManager;
    public TouchManager touchManager;
    public CanvasGroup dialogueGroup;
    public bool dialogStarted = false;

    private TMP_Text[] _textAreas;
    private int _lineIndex;
    private TMP_Text _dialogueText;
    private TMP_Text _dialogueNpcName;
    public List<string> lineList;

    private void Awake()
    {
        dialogStarted = false;
        /*FindNPCManagerInActiveScene();
        AssignTextAreas();*/
        dialogueGroup = GetComponent<CanvasGroup>();
    }

    public void FindNPCManagerInActiveScene()
    {
        npcManager = Object.FindFirstObjectByType<NPCManager>(FindObjectsInactive.Include);
        if (npcManager == null)
        {
            Debug.Log("ERROR : NPC Manager not found");
        }
    }
    public void FindTouchManagerInActiveScene()
    {
        touchManager = Object.FindFirstObjectByType<TouchManager>(FindObjectsInactive.Include);
        if (touchManager == null)
        {
            Debug.Log("ERROR : Touch Manager not found");
        }
    }

    public void AssignTextAreas()
    {
        _textAreas = GetComponentsInChildren<TMP_Text>();

        _dialogueText = _textAreas[0];
        _dialogueNpcName = _textAreas[1];
    }

    public void GetDialogueLines()
    {
        lineList = new List<string>();
        if (npcManager.dialogueNpc == null)
        {
            Debug.LogError("ERROR : Dialogue NPC not found");
        }
        lineList = npcManager.dialogueNpc.dialogueLines;
    }

    public void DisplayNpcName()
    {
        _dialogueNpcName.SetText(npcManager.dialogueNpc.npcName);
    }

    public void StartDialogue()
    {
        if (!dialogStarted)
        {
            _lineIndex = 0;
            DisplayNpcName();
            _dialogueText.SetText(lineList[_lineIndex]);
            dialogStarted = true;
        }
    }

    public void GoToNextDialogueLine()
    {
        if (!IsDialogueFinished() && !touchManager.DialogueLineSkiped)
        {
            _dialogueText.SetText(lineList[_lineIndex++]);
            touchManager.DialogueLineSkiped = true;
        }
        else if (IsDialogueFinished())
        {
            Debug.Log("dialogue end");
            EndDialogue();
        }
    }

    public bool IsDialogueFinished()
    {
        if (dialogStarted)
        {
            if (lineList.Count == _lineIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void EndDialogue()
    {
        Destroy(npcManager.dialogueNpc.GetComponent<Trigger>().activeUI);
    }
}
