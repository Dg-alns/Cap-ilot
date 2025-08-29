using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQuizBox : DialogueBox
{
    QuizNPC _npc;

    bool _activeQuiz;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init()
    {
        dialogStarted = false;
        _activeQuiz = false;
        dialogueGroup = GetComponent<CanvasGroup>();

        _npc = npcManager.dialogueNpc as QuizNPC;

        PlayerPrefs.DeleteKey(_npc.GetNamePlayerPrefsNPC());

        if (PlayerPrefs.HasKey(_npc.GetNamePlayerPrefsNPC()) == false)
            PlayerPrefs.SetInt(_npc.GetNamePlayerPrefsNPC(), 0);
    }

    private void Update()
    {
        if (_activeQuiz == false)
        {
            ActiveQuiz(); 
        
        }
    }

    private void ActiveQuiz() {
        
        if (_npc.idxQuizSet == _npc.idxOffSetDialogue || PlayerPrefs.GetInt(_npc.GetNamePlayerPrefsNPC()) != 0)
        {
            _activeQuiz = true;
            _npc.FirstQuestion();
            PlayerPrefs.SetInt(_npc.GetNamePlayerPrefsNPC(), 1);
            
            DestroiUI();
        }
    }

    public override void GetDialogueLines()
    {
        lineList = new List<string>();
        if (_npc == null)
        {
            Debug.LogError("ERROR : Dialogue NPC not found");
        }

        if (PlayerPrefs.GetInt(_npc.GetNamePlayerPrefsNPC()) != 0)
            lineList = npcManager.dialogueNpc.dialogueSet[_npc.idxQuizSet].dialogueLines;
        else
            lineList = npcManager.dialogueNpc.GetLstDialogue();
    }

    public override void EndDialogue()
    {
        if (!npcManager.dialogueNpc.NextSet())
        {
            DestroiUI();
        }
        GetDialogueLines();

        _lineIndex = 0;
        _dialogueText.SetText(lineList[_lineIndex]);
        dialogStarted = true;
    }

    public void DestroiUI()
    {
        Destroy(npcManager.dialogueNpc.GetComponent<Trigger>().activeUI);
        npcManager.dialogueNpc.GetComponent<Trigger>().activeUI = null;
        npcManager.dialogueNpc.GetComponent<Trigger>().uiOpen = false;
    }
}
