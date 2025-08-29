using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum TriggerType
{
    PORT,
    PANCARTE,
    DIALOG,
    MINIGAME,
    QUIZ
}

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject UI;
    public bool uiOpen;
    public string SceneName;
    public LoadNexScene nexScene;
    public TriggerType Type;
    public GameObject activeUI;

    public bool InPort = true;

    [Header ("Use for Capitain in port")]
    public CapitainPort capitainPort;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


    }

    public void IsTrigger()
    {
        switch (Type)
        {
            case TriggerType.PORT:
                if (!uiOpen)
                {
                    GameObject ui = Instantiate(UI);
                    string spePort = InPort ? "embarquez" : "débarquez";
                    string scene = InPort ? "Archipel" : GetComponent<AccesToPort>().port.IleName;

                    ui.GetComponentInChildren<TextMeshProUGUI>().text = $"Souhaitez vous {spePort} ?";
                    Button no = ui.GetComponentsInChildren<Button>()[0];
                    no.onClick.AddListener(() => Destroy(ui));
                    no.onClick.AddListener(() => uiOpen = false);
                    Button yes = ui.GetComponentsInChildren<Button>()[1];
                    yes.onClick.AddListener(() => nexScene.LoadBoat(GetComponent<AccesToPort>()));
                    uiOpen = true;
                }
                break;
            case TriggerType.PANCARTE:
                if (!uiOpen)
                {
                    GameObject ui = Instantiate(UI);
                    ui.GetComponentInChildren<TextMeshProUGUI>().text = "Souhaitez vous vous rendre vers : " + SceneName;
                    Button no = ui.GetComponentsInChildren<Button>()[0];
                    no.onClick.AddListener(() => Destroy(ui));
                    no.onClick.AddListener(() => uiOpen = false);
                    Button yes = ui.GetComponentsInChildren<Button>()[1];
                    yes.onClick.AddListener(() => nexScene.LoadNextScene(SceneName));      
                    uiOpen = true;
                }
                break;
            case TriggerType.DIALOG:
                if (gameObject.name.Contains("Capitain"))
                {
                    if (!uiOpen)
                    {
                        activeUI = Instantiate(UI);

                        activeUI.GetComponentInChildren<DialogueCapitainBox>().dialogStarted = false;
                        activeUI.GetComponentInChildren<DialogueCapitainBox>().lineList.Clear();
                        activeUI.GetComponentInChildren<DialogueCapitainBox>().InitCapitain(capitainPort);
                        activeUI.GetComponentInChildren<DialogueCapitainBox>().FindNPCManagerInActiveScene();
                        activeUI.GetComponentInChildren<DialogueCapitainBox>().FindTouchManagerInActiveScene();
                        activeUI.GetComponentInChildren<DialogueCapitainBox>().AssignTextAreas();
                        activeUI.GetComponentInChildren<DialogueCapitainBox>().GetDialogueLines();
                        activeUI.GetComponentInChildren<DialogueCapitainBox>().StartDialogue();

                        uiOpen = true;
                    }
                }
                else
                {
                    if (!uiOpen)
                    {
                        activeUI = Instantiate(UI);

                        activeUI.GetComponentInChildren<DialogueBox>().dialogStarted = false;
                        activeUI.GetComponentInChildren<DialogueBox>().lineList.Clear();
                        activeUI.GetComponentInChildren<DialogueBox>().FindNPCManagerInActiveScene();
                        activeUI.GetComponentInChildren<DialogueBox>().FindTouchManagerInActiveScene();
                        activeUI.GetComponentInChildren<DialogueBox>().AssignTextAreas();
                        activeUI.GetComponentInChildren<DialogueBox>().GetDialogueLines();
                        activeUI.GetComponentInChildren<DialogueBox>().StartDialogue();

                        uiOpen = true;
                    }
                }
                break;
            case TriggerType.MINIGAME:
                if (!uiOpen)
                {
                    activeUI = Instantiate(UI);

                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().dialogStarted = false;
                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().lineList.Clear();
                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().FindNPCManagerInActiveScene();
                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().Init();
                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().FindTouchManagerInActiveScene();
                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().AssignTextAreas();
                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().GetDialogueLines();
                    activeUI.GetComponentInChildren<DialogueMiniGameBox>().StartDialogue();

                    uiOpen = true;
                }     
                break;
            case TriggerType.QUIZ:
                if (!uiOpen)
                {
                    activeUI = Instantiate(UI);
                    activeUI.GetComponentInChildren<DialogueQuizBox>().dialogStarted = false;
                    activeUI.GetComponentInChildren<DialogueQuizBox>().lineList.Clear();
                    activeUI.GetComponentInChildren<DialogueQuizBox>().FindNPCManagerInActiveScene();
                    activeUI.GetComponentInChildren<DialogueQuizBox>().Init();
                    activeUI.GetComponentInChildren<DialogueQuizBox>().FindTouchManagerInActiveScene();
                    activeUI.GetComponentInChildren<DialogueQuizBox>().AssignTextAreas();
                    activeUI.GetComponentInChildren<DialogueQuizBox>().GetDialogueLines();
                    activeUI.GetComponentInChildren<DialogueQuizBox>().StartDialogue();

                    uiOpen = true;
                }
                break;
        }
    }
}
