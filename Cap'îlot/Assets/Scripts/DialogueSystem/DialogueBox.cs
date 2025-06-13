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
        npcManager = FindNPCManagerInActiveScene();
        AssignTextAreas();
        dialogueGroup = GetComponent<CanvasGroup>();
    }

    NPCManager FindNPCManagerInActiveScene()
    {
        List<NPCManager> found = new List<NPCManager>();
        SceneManager.GetActiveScene().GetRootGameObjects()
            .ToList()
            .ForEach(obj => obj.GetComponentsInChildren(true, found)); // true = inclut désactivés

        return found.FirstOrDefault();
    }

    public void AssignTextAreas()
    {
        _textAreas = GetComponentsInChildren<TMP_Text>();

        _dialogueText = _textAreas[0];
        _dialogueNpcName = _textAreas[1];
    }

    public void GetDialogueLines()
    {
        _lineList = new List<string>();
        _lineList = npcManager.dialogueNpc.dialogueLines;
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
            _dialogueText.SetText(_lineList[_lineIndex]);
            dialogStarted = true;
        }
    }

    public void GoToNextDialogueLine()
    {
        _dialogueText.SetText(_lineList[_lineIndex++]);
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
