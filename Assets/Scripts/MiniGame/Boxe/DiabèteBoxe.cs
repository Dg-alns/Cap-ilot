using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiabèteBoxe : MonoBehaviour
{
    public GameObject WarningLeft;
    public GameObject WarningRight;

    Animator animator;
    void Start()
    {
        WarningLeft.SetActive(false);
        WarningRight.SetActive(false);

        animator = GetComponent<Animator>();
    }

    public Animator GetAnimator() { return animator; }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ChooseEsquive()
    {
        int taux = Random.Range(0, 2);

        //switch(taux)
        //{
        //    case 0:
        //        return 1;
        //        //Animator.SetInteger("Esquive", 1);
        //    case 1:
        //        return 2;
        //        //Animator.SetInteger("Esquive", 2);

        //}
        return taux;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colission");
    }
}
