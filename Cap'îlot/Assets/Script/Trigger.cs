using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum Type
{
    PORT,
    DIALOG
}

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private bool uiOpen;
    public string SceneName;
    public Type Type;
    public int clickedNpcId;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<Movement>().WTF();

        if (collision.gameObject.GetComponent<Trigger>().Type == Type.DIALOG)
        {
            clickedNpcId = collision.gameObject.GetComponent<NPC>().npcId;
        }
    }

    public void IsTrigger()
    {
        switch (Type) { 
        
        case Type.PORT:
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
        }
    }
}
