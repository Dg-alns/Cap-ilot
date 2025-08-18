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

    protected TMP_Text[] _textAreas;
    protected int _lineIndex;
    protected TMP_Text _dialogueText;
    protected TMP_Text _dialogueNpcName;
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

    public virtual void GetDialogueLines()
    {
        lineList = new List<string>();
        if (npcManager.dialogueNpc == null)
        {
            Debug.LogError("ERROR : Dialogue NPC not found");
        }
        lineList = npcManager.dialogueNpc.GetLstDialogue();
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
            if (_lineIndex + 1 > lineList.Count)
            {
                Debug.Log("dialogue end");
                EndDialogue();
            }
            else
            {

                _dialogueText.SetText(lineList[_lineIndex++]);
                touchManager.DialogueLineSkiped = true;
            }
        }
        else if (IsDialogueFinished())
        {
            Debug.Log("dialogue end");
            EndDialogue();
        }
    }

    public bool IsDialogueFinished()
    {
        //Debug.Log("dialogStarted : " + dialogStarted);
        if (dialogStarted)
        {
            if (lineList.Count < _lineIndex)
            {
                Debug.Log("dialogFinish : " + lineList.Count + " " +_lineIndex);
                return true;
            }
            else
            {
                //Debug.Log("dialogFinish : " + lineList.Count + " " + _lineIndex);
                return false;
            }
        }
        return false;
    }

    public virtual void EndDialogue()
    {
        Destroy(npcManager.dialogueNpc.GetComponent<Trigger>().activeUI);
        npcManager.dialogueNpc.GetComponent<Trigger>().activeUI = null;
        npcManager.dialogueNpc.GetComponent<Trigger>().uiOpen = false;

        if (npcManager.dialogueNpc.npcName.Equals("Diabete"))
        {
            //QuestManager.NextQuest();
            QuestManager.ValidateQuest(QUESTS.Maison);
            npcManager.dialogueNpc.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        npcManager.ResetDialogueNPC();
    }
}
