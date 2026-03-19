using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Minigame_Memorie : Minigame
{
    private List<GameObject> mShowingCard = new List<GameObject>();
    private int nbSlots;

    public List<int> slots = new List<int>();
    public Particle_Memorie mParticleMemorie;
    public Score score;

    public Sauvegarde_Minigame minigame;
    public Diabete_Memorie diabete;

    public Infos_MiniJeux infos;

    // Start is called before the first frame update
    void Awake()
    {
        nbSlots = 16;

        for (int i = 0; i < nbSlots; i++)
        {
            slots.Add(i);
        }
    }

    private void CheckWin()
    {
        if (diabete.mCards.Count <= 0)
        {
            score.timer.RestartTimer();
            score.LauchScore();
        }
    }

    private bool CheckPair()
    {
        if (mShowingCard.Count == 2)
        {
            score.AddScore();
            Card_Memorie card1 = mShowingCard[0].GetComponent<Card_Memorie>();
            Card_Memorie card2 = mShowingCard[1].GetComponent<Card_Memorie>();

            if(string.Equals(card1.symbol, card2.symbol))
            {
                StartCoroutine(AddScore());
                return true;
            }
            StartCoroutine(ReverseCard());
        }
        return false;
    }

    // Add a symbol in a list after a click on a card
    public void AddShowingList(GameObject showingCard)
    {
        // If you try to return more than 2 card => Return
        if(mShowingCard.Count >= 2) 
            return;

        showingCard.GetComponent<Card_Memorie>().ReverseCard();
        diabete.mCards.Remove(showingCard.GetComponent<Card_Memorie>());
        mShowingCard.Add(showingCard);

        CheckPair();
    }

    public int GetNbAction() { 
        return score.mCurrentScore;
    }



    IEnumerator ReverseCard()
    {
        Debug.Log("Reverse Card after 1 sec");
        yield return new WaitForSeconds(1.0f);
        mShowingCard[0].GetComponent<Card_Memorie>().ReverseCard();
        mShowingCard[1].GetComponent<Card_Memorie>().ReverseCard();

        diabete.mCards.Add(mShowingCard[0].GetComponent<Card_Memorie>());
        diabete.mCards.Add(mShowingCard[1].GetComponent<Card_Memorie>());

        mShowingCard.Clear();
    }
    IEnumerator AddScore()
    {
        Debug.Log("Add Score after 1 sec");
        yield return new WaitForSeconds(0.3f);
        mParticleMemorie.PlayParticle(mShowingCard);

        yield return new WaitForSeconds(0.3f);

        if (minigame.GetCanShowInfo(SceneManager.GetActiveScene().name) == true)
        {
            infos.gameObject.SetActive(true);

            infos.AssociateInfo(mShowingCard[0].GetComponent<Objects>());
        }

        mShowingCard.Clear();
    }

    private void Update()
    {
        if (infos.gameObject.activeSelf == false)
            CheckWin();
    }

    public override void PauseMinigame()
    {
        diabete.DiseableDiabete();
    }

    public override void ResumeMinigame()
    {
        diabete.ActiveDiabete();
    }

}
