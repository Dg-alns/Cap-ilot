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


    bool onDefense = false;

    void Start()
    {
        timer.SetNSeconds(3);
        SwipeManager = GetComponent<SwipeManager>();
        PlayerAnimator = Player.GetAnimator();
        diabeteAnimator = diabeteBoxe.GetAnimator();

        SwipeManager.canSwipe = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Round.StateOfRound == TEXTofROUND.Garde)
        {

            UpdateBoxeGarde();
        }

        if (Round.StateOfRound == TEXTofROUND.Attaque)
        {
            UpdateBoxeAttaque();
        }
    }

    void UpdateBoxeGarde()
    {
        if (onDefense == false)
        {
            if (timer.ElapseNsecond())
            {
                int nb = SelectAttaqueDiabete();
                diabeteBoxe.LaucheAnimationDetectorAttaque(nb);
                StartCoroutine(StartDefensePlayer(nb));
            }
        }

        //if (SwipeManager.directionOfSwipe != 0)
        //{
        //    StartDefensePlayer();
        //}
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
        int dirEsquive = diabeteBoxe.ChooseActionDiabete();

        if (SwipeManager.directionOfSwipe > 0)
        {
            Player.ChangeActionOfPLayer(ACTION.AttaqueGauche);
        }
        if (SwipeManager.directionOfSwipe < 0)
        {
            Player.ChangeActionOfPLayer(ACTION.AttaqueDroite);
        }


        switch (dirEsquive)
        {
            case 1:
                diabeteAnimator.SetInteger("Esquive", 1);
                break;
            case 2:
                diabeteAnimator.SetInteger("Esquive", 2);
                break;
        }

        DetectionOffContact(SwipeManager.directionOfSwipe, dirEsquive);
        SwipeManager.ChangeStateOfCanSwipe(false);
        StartCoroutine(ResetAttaque());



        Round.NextPhase();


    }

    public IEnumerator StartDefensePlayer(int attaque) // Coreoutine ??
    {
        onDefense = true;

        //SwipeManager.directionOfSwipe != 0

        yield return new WaitForSeconds(2);

        if (SwipeManager.directionOfSwipe != 0)
        {

            if (SwipeManager.directionOfSwipe > 0)
            {
                Player.ChangeActionOfPLayer(ACTION.GardeDroite);
            }
            if (SwipeManager.directionOfSwipe < 0)
            {
                Player.ChangeActionOfPLayer(ACTION.GardeGauche);
            }
        }
        else{
            //lauch Inactivitie
        }


        switch (attaque)
        {
            case 1:
                diabeteAnimator.SetInteger("Attaque", 1);
                break;
            case 2:
                diabeteAnimator.SetInteger("Attaque", 2);
                break;
        }

        DetectionOffContact(SwipeManager.directionOfSwipe, attaque);
        SwipeManager.ChangeStateOfCanSwipe(false);
        StartCoroutine(ResetAttaque());



        Round.NextPhase();


        Debug.Log("ee");
        diabeteAnimator.SetInteger("Attaque", 2);
        Player.ChangeActionOfPLayer(ACTION.GardeGauche);
    }

    int SelectAttaqueDiabete()
    {
        int dirEsquive = diabeteBoxe.ChooseActionDiabete();

        return dirEsquive;
    }

    void DetectionOffContact(int directionOfSwipe, int valuediabète)
    {
        if(directionOfSwipe > 0 && valuediabète == 1)
        {
            Debug.Log("Contact");
            return;
        }

        if(directionOfSwipe < 0 && valuediabète == 2)
        {
            Debug.Log("Contact");
            return;
        }
    }

    IEnumerator ResetAttaque()
    {
        yield return new WaitForSeconds(1.2f);

        if (Round.StateOfRound == TEXTofROUND.Garde)
        {
            diabeteAnimator.SetInteger("Attaque", 0);
        }

        if (Round.StateOfRound == TEXTofROUND.Attaque)
        {
            diabeteAnimator.SetInteger("Esquive", 0);
        }

        Player.ChangeActionOfPLayer(ACTION.Idle);
        SwipeManager.ChangeStateOfCanSwipe(true);
        diabeteBoxe.ResetWarning();
        onDefense = false;
    }
}
