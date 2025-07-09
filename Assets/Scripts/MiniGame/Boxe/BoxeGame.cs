using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class BoxeGame : MonoBehaviour
{
    public Round Round;
    SwipeManager SwipeManager;


    public ActionOfPLayer Player;
    public DiabèteBoxe diabeteBoxe;
    public Timer timer;

    Animator diabeteAnimator;
    Animator PlayerAnimator;


    void Start()
    {
        //timer.SetNSeconds(3);
        SwipeManager = GetComponent<SwipeManager>();
        PlayerAnimator = Player.GetAnimator();
        diabeteAnimator = diabeteBoxe.GetAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Round.StateOfRound == TEXTofROUND.Garde)
        //{
        //}

        if (Round.StateOfRound == TEXTofROUND.Attaque)
        {
            UpdateBoxeAttaque();
        }
    }

    void UpdateBoxeGarde()
    {

    }

    void UpdateBoxeAttaque()
    {
        if(SwipeManager.directionOfSwipe != 0)
        {
            StartAttaquePlayer();
        }
    }

    public void StartAttaquePlayer()
    {
        int dirEsquive = diabeteBoxe.ChooseEsquive();

        //while (true)
        //{
        //    if (timer.ElapseNsecond())
        //    {
        //        break;
        //    }
        //}
        
        switch(dirEsquive) // TODO Diego A revoir en priorité
        {
            case 1:
                if (SwipeManager.directionOfSwipe > 0)
                {
                    Debug.Log("E droite");
                    Player.ChangeActionOfPLayer(ACTION.AttaqueDroite);
                    diabeteAnimator.SetInteger("Esquive", 1);
                }
                break;
            case 2:
                if (SwipeManager.directionOfSwipe < 0)
                {
                    Debug.Log("E Gauche");
                    Player.ChangeActionOfPLayer(ACTION.AttaqueGauche);
                    diabeteAnimator.SetInteger("Esquive", 2);
                }
                break;
        }

    }

    public void StartDefensePlayer()
    {

    }
}
