using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum Type
{
    PORT,
    DIALOG
}

public class Trigger : MonoBehaviour
{
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
}
