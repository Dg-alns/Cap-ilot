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
    //Garde,
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

    int CurrentRound = 1;

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
            //case TEXTofROUND.Garde:
            //    textRound.text = "En Garde";
            //    break;
            case TEXTofROUND.Attaque:
                textRound.text = "Attaque";
                break;
            case TEXTofROUND.Fin:
                textRound.text = $"Fin du Round {CurrentRound} / {NumberOfRound}";
                break;
        }

        SwitchOfRound();
    }

    void SwitchOfRound()
    {
        switch (StateOfRound)
        {
            case TEXTofROUND.Start:
                //StateOfRound = TEXTofROUND.Garde;
                StateOfRound = TEXTofROUND.Attaque;
                break;
            //case TEXTofROUND.Garde:
                //    StateOfRound = TEXTofROUND.Attaque;
                //animator.SetBool("Continue", false);
                //    break;
            case TEXTofROUND.Attaque:
                //StateOfRound = TEXTofROUND.Fin;
                CurrentRound++;
                animator.SetBool("Continue", false);
                break;
            case TEXTofROUND.Fin:
                StateOfRound = TEXTofROUND.Start;
                break;
        }
    }

    //public void NextRound()
    //{
    //    if(CurrentRound <= NumberOfRound)
    //    {
    //        switch(round)
    //        {
    //            case ROUND.Defense:
    //                round = ROUND.Attaque;
    //                //animator.SetInteger()
    //                break;
    //            case ROUND.Attaque:
    //                round = ROUND.Defense;
    //                CurrentRound++;
    //                //
    //                break;
    //        }

    //        // Lauch Animation Round
    //    }
    //    else
    //    {
    //        // Lauch Animation Stars ??
    //    }
    //}

}
