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
    public CanvasGroup dialogueGroup;
    public bool dialogStarted;

    private TMP_Text[] _textAreas;
    private int _lineIndex;
    private TMP_Text _dialogueText;
    private TMP_Text _dialogueNpcName;
    private List<string> _lineList;

    private void Awake()
    {
        FindNPCManagerInActiveScene();
        AssignTextAreas();
        dialogueGroup = GetComponent<CanvasGroup>();
    }

    public void FindNPCManagerInActiveScene()
    {
        npcManager = Object.FindFirstObjectByType<NPCManager>(FindObjectsInactive.Include);
        if (npcManager == null)
        {
            Debug.Log("ERROR : NPC Manager not found");
        }
        else
        {
            Debug.Log("SUCCESS : NPC Manager found");
        }
    }

    public void AssignTextAreas()
    {
        _textAreas = GetComponentsInChildren<TMP_Text>();
        //Debug.Log(_textAreas);
        //Debug.Log(_textAreas.Length);

        _dialogueText = _textAreas[0];
        _dialogueNpcName = _textAreas[1];
    }

    public void GetDialogueLines()
    {
        _lineList = new List<string>();
        if (npcManager.dialogueNpc == null)
        {
            //Debug.Log("NPC Manager = " + npcManager);
            Debug.LogError("ERROR : Dialogue NPC not found");
        }
        _lineList = npcManager.dialogueNpc.dialogueLines;
        //Debug.Log(npcManager);
    }

    public void DisplayNpcName()
    {
        //Debug.Log(npcManager);
        //Debug.Log(npcManager.dialogueNpc);
        //Debug.Log(npcManager.dialogueNpc.npcName);
        //Debug.Log(_dialogueNpcName);
        _dialogueNpcName.SetText(npcManager.dialogueNpc.npcName);
    }

    public void StartDialogue()
    {
        if (!dialogStarted)
        {
            _lineIndex = 0;
            DisplayNpcName();
            _dialogueText.SetText(_lineList[_lineIndex]);
            dialogStarted = true;
        }
    }

    public void GoToNextDialogueLine()
    {
        //if (_lineList[_lineIndex + 1] != null)
        //{
        //    _dialogueText.SetText(_lineList[_lineIndex++]);
        //}
    }

    public bool IsDialogueFinished()
    {
        if (dialogStarted)
        {
            if (_lineList.Count == _lineIndex)
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
}
