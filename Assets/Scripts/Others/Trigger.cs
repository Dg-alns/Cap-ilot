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
    DIALOG
}

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private bool uiOpen;
    public string SceneName;
    public TriggerType Type;


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
                    ui.GetComponentInChildren<TextMeshProUGUI>().text = "Souhaitez vous vous rendre vers : " + SceneName;
                    Button no = ui.GetComponentsInChildren<Button>()[0];
                    no.onClick.AddListener(() => Destroy(ui));
                    no.onClick.AddListener(() => uiOpen = false);
                    Button yes = ui.GetComponentsInChildren<Button>()[1];
                    yes.onClick.AddListener(() => SceneManager.LoadScene(SceneName));
                    uiOpen = true;
                }
                break;
            case TriggerType.DIALOG:
                if (!uiOpen)
                {
                    GameObject ui = Instantiate(UI);

                    ui.GetComponentInChildren<DialogueBox>().dialogStarted = false;
                    ui.GetComponentInChildren<DialogueBox>().lineList.Clear();
                    ui.GetComponentInChildren<DialogueBox>().FindNPCManagerInActiveScene();
                    ui.GetComponentInChildren<DialogueBox>().AssignTextAreas();
                    ui.GetComponentInChildren<DialogueBox>().GetDialogueLines();
                    ui.GetComponentInChildren<DialogueBox>().StartDialogue();

                    uiOpen = true;
                }     
                break;
        }
    }

}
