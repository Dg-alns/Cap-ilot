using System.Collections;
using UnityEngine;

public class BoxeGame : MonoBehaviour
{
    SwipeManager SwipeManager;
    Timer timer;

    public Round Round;
    public ActionOfPLayer Player;
    public DiabèteBoxe diabeteBoxe;
    public Score score;

    Animator diabeteAnimator;
    Animator PlayerAnimator;


    bool StartGame = false;
    bool onDefense = false;

    void Start()
    {
        timer = GetComponent<Timer>();
        timer.SetNSeconds(3);
        SwipeManager = GetComponent<SwipeManager>();
        PlayerAnimator = Player.GetAnimator();
        diabeteAnimator = diabeteBoxe.GetAnimator();

        SwipeManager.canSwipe = false;
    }

    public void StartBoxe()
    {
        StartGame = true;
        SwipeManager.canSwipe = true;
        Round.StartRound();
    }

    void Update()
    {
        if (Round.HaveFinishAllRound())
        {
            score.LauchScore();
            Player.gameObject.SetActive(false);
            StartGame = false;
        }

        if (StartGame == false)
            return;

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
                int nb = diabeteBoxe.ChooseActionDiabete();
                Debug.Log(nb);
                diabeteBoxe.LaucheAnimationDetectorAttaque(nb);
                StartCoroutine(StartDefensePlayer(nb));
            }
        }
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
        SwipeManager.StopCanSwipe();
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
                diabeteAnimator.SetInteger("Esquive", dirEsquive);
                break;
            case 2:
                diabeteAnimator.SetInteger("Esquive", dirEsquive);
                break;
        }

        DetectionOffContact(SwipeManager.directionOfSwipe, dirEsquive);
        SwipeManager.ChangeStateOfCanSwipe(false);
        StartCoroutine(ResetAttaque());
    }

    public IEnumerator StartDefensePlayer(int attaque)
    {
        onDefense = true;

        yield return new WaitForSeconds(2);
        SwipeManager.StopCanSwipe();

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
        else
        {
            Player.ActiveIdle();
        }


        switch (attaque)
        {
            case 1:
                diabeteAnimator.SetInteger("Attaque", 2);
                break;
            case 2:
                diabeteAnimator.SetInteger("Attaque", 1);
                break;
        }

        DetectionOffContact(SwipeManager.directionOfSwipe, attaque);
        StartCoroutine(ResetAttaque());
    }

    void DetectionOffContact(int directionOfSwipe, int valuediabète)
    {

        if (Round.StateOfRound == TEXTofROUND.Attaque)
        {
            if (directionOfSwipe > 0 && valuediabète == 1)
            {
                score.AddScore();
                return;
            }

            if (directionOfSwipe < 0 && valuediabète == 2)
            {
                score.AddScore();
                return;
            }
        }

        if (Round.StateOfRound == TEXTofROUND.Garde)
        {
            if (directionOfSwipe < 0 && valuediabète == 1)
            {
                score.AddScore();
                return;
            }

            if (directionOfSwipe > 0 && valuediabète == 2)
            {
                score.AddScore();
                return;
            }
        }


    }

    IEnumerator ResetAttaque()
    {

        SwipeManager.ChangeStateOfCanSwipe(false);
        yield return new WaitForSeconds(1.2f);

        diabeteAnimator.SetInteger("Attaque", 0);
        diabeteAnimator.SetInteger("Esquive", 0);  

        diabeteBoxe.ResetWarning();

        Player.ActiveIdle();

        yield return new WaitForSeconds(1.2f);



        yield return new WaitForSeconds(0.8f);
        Round.NextPhase(timer);
        SwipeManager.ChangeStateOfCanSwipe(true);
        onDefense = false;
    }
}
