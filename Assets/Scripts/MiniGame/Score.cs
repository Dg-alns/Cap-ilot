using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Score : MonoBehaviour
{
    public Timer timer;
    public GameObject win;

    public float MinBronzeInSecondes;
    public float MinArgentInSecondes;
    public float MinOrInSecondes;

    float MiniGameSecondes;

    bool animLoad = false;

    Animator animator;

    public void LauchScore()
    {
        if(animLoad)
            return;

        gameObject.SetActive(true);
        MiniGameSecondes = timer.GetTimeInSecondes();

        Debug.Log(MiniGameSecondes);

        animator = GetComponent<Animator>();

        GetMedailles(animator);
        animLoad = true;
    }

    private void Start()
    {
        gameObject.SetActive(false);
        win.SetActive(false);
    }

    public void LauchWin()
    {
        win.SetActive(true);
        win.GetComponent<Animator>().SetBool("TEST", true);
    }


    void GetMedailles(Animator animator)
    {
        if(MiniGameSecondes > MinBronzeInSecondes)
            animator.SetBool("0", true);

        else if(MiniGameSecondes <= MinBronzeInSecondes && MiniGameSecondes > MinArgentInSecondes)
            animator.SetBool("1", true);

        else if(MiniGameSecondes <= MinArgentInSecondes && MiniGameSecondes > MinOrInSecondes)
            animator.SetBool("2", true);

        else if(MiniGameSecondes <= MinOrInSecondes)
            animator.SetBool("3", true);

    }

}
