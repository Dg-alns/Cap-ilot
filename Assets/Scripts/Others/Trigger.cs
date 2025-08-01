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
    DIALOG
}

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private bool uiOpen;
    public string SceneName;
    public LoadNexScene nexScene;
    public TriggerType Type;
    public GameObject activeUI;

    public bool InPort = true;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<Movement>().WTF();


    }

    public void IsTrigger()
    {
        switch (Type)
        {
            case TriggerType.PORT:
                if (!uiOpen)
                {
                    GameObject ui = Instantiate(UI);
                    string spePort = InPort ? "embarquez" : "d�barquez";
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
                break;
        }
    }
}
