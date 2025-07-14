using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ROUND
{
    Defense,
    Attaque
}

public enum TEXTofROUND
{
    Start,
    Garde,
    Attaque,
    Fin
}

public class Round : MonoBehaviour
{
    //ROUND round = ROUND.Defense;
    Animator animator;

    public TextMeshProUGUI textRound;
    public int NumberOfRound;
    public TEXTofROUND StateOfRound;
    public SwipeManager swipeManager;

    int CurrentRound = 1;
    int nbPhase = 3;
    int CurrentPhase = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Continue", true);
        SwitchTextOfRound();
    }

    void SwitchTextOfRound()
    {
        switch (StateOfRound)
        {
            case TEXTofROUND.Start:
                textRound.text = $"Début du Round {CurrentRound} / {NumberOfRound}";
                break;
            case TEXTofROUND.Garde:
                textRound.text = "En Garde";
                break;
            case TEXTofROUND.Attaque:
                textRound.text = "Attaque";
                break;
            case TEXTofROUND.Fin:
                textRound.text = $"Fin du Round {CurrentRound} / {NumberOfRound}";
                break;
        }
        // Timer nSeconde comparer a l'anim du text ??

        SwitchOfRound();


        ChangeSwipe();
    }

    void SwitchOfRound()
    {
        switch (StateOfRound)
        {
            case TEXTofROUND.Start:
                StateOfRound = TEXTofROUND.Garde;
                //StateOfRound = TEXTofROUND.Attaque;
                break;
            case TEXTofROUND.Garde:
                //StateOfRound = TEXTofROUND.Attaque;
                animator.SetBool("Continue", false);
                break;
            case TEXTofROUND.Attaque:
                //StateOfRound = TEXTofROUND.Fin;
                //CurrentRound++;
                animator.SetBool("Continue", false);
                break;
            case TEXTofROUND.Fin:
                StateOfRound = TEXTofROUND.Start;
                break;
        }
    }

    public void NextPhase()
    {
        if (CurrentRound >= NumberOfRound)
            return;


        if (CurrentPhase <= nbPhase)
        {
            switch (StateOfRound)
            {
                case TEXTofROUND.Garde:
                    CurrentPhase++;
                    if (CurrentPhase >= nbPhase)
                    {
                        Debug.Log(CurrentPhase);
                        CurrentRound++;
                        CurrentPhase = 0;
                        //StateOfRound = ROUND.Attaque;
                    }
                    break;
            
                case TEXTofROUND.Attaque:
                    CurrentPhase++;
                    if (CurrentPhase >= nbPhase)
                    {
                        Debug.Log(CurrentPhase);
                        CurrentRound++;
                        CurrentPhase = 0;
                        //StateOfRound = ROUND.Fin;
                    }
                    break;
            }
        }
    }


    public void ChangeSwipe()
    {
        bool state = false;

        if(StateOfRound == TEXTofROUND.Attaque || StateOfRound == TEXTofROUND.Garde)
            state = true;

        swipeManager.ChangeStateOfCanSwipe(state);
    }
}
