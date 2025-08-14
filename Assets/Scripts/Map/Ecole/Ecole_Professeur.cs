using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum OffSet_Dialogue_Professeur
{
    QuizSuccess = 2,
    QuizDone = 3,
} 

public class Ecole_Professeur : MonoBehaviour
{
    private MiniGameNPC _professeurNPC;
    public Movement player;

    // Start is called before the first frame update
    private void Start()
    {
        _professeurNPC = GetComponent<MiniGameNPC>();

        string previousScene = _professeurNPC.loadNexScene.GetPreviousSceneName();

        //if (!string.Equals(previousScene, "Quiz"))
        //    return;

        //if (QuestManager.GetPlayerPref() != QuestManager.GetQUESTS(QUESTS.Ecole))
        //    return;

        player.dialogueNpc = gameObject;

        NPCManager npcManager = Object.FindFirstObjectByType<NPCManager>(FindObjectsInactive.Include);
        npcManager.GetComponent<NPCManager>().FindNpcById(_professeurNPC.npcId);

        _professeurNPC.SetPLayerPrefs((int)OffSet_Dialogue_Professeur.QuizDone);
        _professeurNPC.idxOffSetDialogue = _professeurNPC.GetPlayerPrefs();

        ForceDialogueProf();

        QuestManager.NextQuest();
    }
   
    void ForceDialogueProf()
    {
        Trigger trigger = _professeurNPC.GetComponent<Trigger>();
        GameObject activeUI = Instantiate(trigger.UI);
        DialogueMiniGameBox dialogueMiniGameBox = activeUI.GetComponentInChildren<DialogueMiniGameBox>();

        trigger.activeUI = activeUI;

        player.activeDialogueUI = activeUI;


        dialogueMiniGameBox.dialogStarted = false;
        dialogueMiniGameBox.lineList.Clear();
        dialogueMiniGameBox.FindNPCManagerInActiveScene();
        dialogueMiniGameBox.Init(true);
        dialogueMiniGameBox.FindTouchManagerInActiveScene();
        dialogueMiniGameBox.AssignTextAreas();
        dialogueMiniGameBox.GetDialogueLines();
        dialogueMiniGameBox.StartDialogue();

        trigger.uiOpen = true;
    }
}
