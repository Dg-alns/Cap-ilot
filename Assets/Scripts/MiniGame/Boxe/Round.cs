using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TEXTofROUND
{
    Start,
    Garde,
    Attaque,
    Fin
}

public class Round : MonoBehaviour
{
    Animator animator;

    public TextMeshProUGUI textRound;
    public int NumberOfRound;
    public TEXTofROUND StateOfRound;
    public SwipeManager swipeManager;

    int CurrentRound = 1;
    int nbPhase = 3;
    int CurrentPhase = 0;

    public void StartRound()
    {
        gameObject.SetActive(true);
        animator = GetComponent<Animator>();
        UpdateTextOfRound();
        animator.SetBool("Continue", true);
    }

    void UpdateTextOfRound()
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
    }

    void SwitchOfRound()
    {
        switch (StateOfRound)
        {
            case TEXTofROUND.Start:
                StateOfRound = TEXTofROUND.Garde;
                animator.SetBool("Continue", true);
                break;
            case TEXTofROUND.Garde:
                StateOfRound = TEXTofROUND.Attaque;
                break;
            case TEXTofROUND.Attaque:
                StateOfRound = TEXTofROUND.Fin;
                break;
            case TEXTofROUND.Fin:
                CurrentRound++;

                if(HaveFinishAllRound())
                    gameObject.SetActive(false);

                StateOfRound = TEXTofROUND.Start;
                animator.SetBool("Continue", true);
                break;
        }
    }

    public void NextPhase(Timer timer)
    {
        if (CurrentRound > NumberOfRound)
            return;


        if (CurrentPhase <= nbPhase)
        {
            switch (StateOfRound)
            {
                case TEXTofROUND.Garde:
                    CurrentPhase++;
                    if (CurrentPhase >= nbPhase)
                    {
                        timer.ResetNSecconds();
                        UpdateNextPhase();
                    }
                    break;
            
                case TEXTofROUND.Attaque:
                    CurrentPhase++;
                    if (CurrentPhase >= nbPhase)
                    {
                        UpdateNextPhase();
                    }
                    break;
            }
        }
    }

    void UpdateNextPhase()
    {
        CurrentPhase = 0;
        SwitchOfRound();
        UpdateTextOfRound();
        ChangeSwipe();
        animator.SetBool("Continue", true);
    }

    public void ChangeSwipe()
    {
        bool state = false;

        if(StateOfRound == TEXTofROUND.Attaque || StateOfRound == TEXTofROUND.Garde)
            state = true;

        swipeManager.ChangeStateOfCanSwipe(state);
    }


    public void ChangeState()
    {
        animator.SetBool("Continue", false);

        if (StateOfRound != TEXTofROUND.Attaque && StateOfRound != TEXTofROUND.Garde)
        {
            SwitchOfRound();

            UpdateTextOfRound();
        }

    }

    public bool HaveFinishAllRound()
    {
        return CurrentRound > NumberOfRound;
    }
}
