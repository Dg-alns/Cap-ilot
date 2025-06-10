using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Memorie : MonoBehaviour
{
    private List<GameObject> mShowingCard = new List<GameObject>();
    private int mScore;

    public int nbSlotsAvailable = 16;
    public List<int> slots = new List<int>();
    public ParticleMemorie mParticleMemorie;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < nbSlotsAvailable; i++)
        {
            slots.Add(i);
        }
    }

    public bool CheckPair()
    {
        if (mShowingCard.Count == 2)
        {
            Card card1 = mShowingCard[0].GetComponent<Card>();
            Card card2 = mShowingCard[1].GetComponent<Card>();

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

        showingCard.GetComponent<Card>().ReverseCard();
        mShowingCard.Add(showingCard);

        CheckPair();
    }

    public int Score() { 
        return mScore;
    }

    IEnumerator ReverseCard()
    {
        Debug.Log("Reverse Card after 1 sec");
        yield return new WaitForSeconds(1.0f);
        mShowingCard[0].GetComponent<Card>().ReverseCard();
        mShowingCard[1].GetComponent<Card>().ReverseCard();
        mShowingCard.Clear();
    }
    IEnumerator AddScore()
    {
        Debug.Log("Add Score after 1 sec");
        yield return new WaitForSeconds(0.3f);
        mParticleMemorie.PlayParticle(mShowingCard);
        mScore += 200;
        mShowingCard.Clear();
    }

}
