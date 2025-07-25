using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    bool open;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowHideUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowHideUI()
    {
        if (open)
        {
            animator.Play("HideUI");
            open = false;
        }
        else {
            animator.Play("ShowUI");
            open = true;
        }

    }
}
