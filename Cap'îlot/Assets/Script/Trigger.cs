using System.Collections;
using System.Collections.Generic;
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
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<Movement>().WTF();

    }
}
