using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Diabete_Memorie : MonoBehaviour
{
    private float mTargetTime;
    private float mCurrentTime;

    private bool isSwitchingTime;
    private bool haveChoiceCard;

    private Animator animator;

    public List<Card_Memorie> mCards;

    private float[] mLimitSwitchTime;
    private bool NotActive = true;

    private void Start()
    {
        mLimitSwitchTime = new float[2];

        mLimitSwitchTime[0] = 5.0f;
        mLimitSwitchTime[1] = 10.0f;

        ResetTime();

        animator = GetComponent<Animator>();

        Debug.Log(animator.GetAnimatorTransitionInfo(0).duration);

        haveChoiceCard = false;
        isSwitchingTime = false;

        mCards = new List<Card_Memorie>(GameObject.Find("Cards").GetComponentsInChildren<Card_Memorie>());

        Debug.Log("Nombre de cartes : " + mCards.Count);
    }

    void Update()
    {
        if (NotActive)
            return;


        if(animator.GetBool("FindingPair")) return;

        // Increase the time
        mCurrentTime += Time.deltaTime;

        // If he is on the screen
        if (isSwitchingTime)
        {
            if (haveChoiceCard == false && mCurrentTime > 1.5f)
            {
                SwapRandomCards();
                haveChoiceCard = true;
                return;
            }
            if (mCurrentTime > 3f)
            {
                isSwitchingTime = false;
                haveChoiceCard = false;
                SwapStateDiabete();
                ResetTime();
            }
            return;
        }
        // Wait for x seconds to active the diabete
        if (mCurrentTime > mTargetTime)
        {
            if (mCards.Count <= 1) {
                ResetTime();
                return;
            }

            SwapStateDiabete();

            isSwitchingTime = true;

            mCurrentTime = 0.0f;
            //ResetTime();
        }
    }

    private void ResetTime()
    {
        mCurrentTime = 0;
        mTargetTime = Random.Range(mLimitSwitchTime[0], mLimitSwitchTime[1]);
        Debug.Log(mTargetTime);
    }
    private void SwapRandomCards()
    {
        int r1 = Random.Range(0, mCards.Count);
        int r2;
        do
        {
            if (mCards.Count <= 1)
            {
                ResetTime();
                return;
            }
            r2 = Random.Range(0, mCards.Count);
        } while (r1 == r2);
        Card_Memorie card1 = mCards[r1];
        Card_Memorie card2 = mCards[r2];
        card1.SwitchCard(card2.gameObject.GetComponent<Transform>().position);
        card2.SwitchCard(card1.gameObject.GetComponent<Transform>().position);
    }
    public void SwapStateDiabete()
    {
        animator.SetBool("DiabeteTime", !animator.GetBool("DiabeteTime"));
    }
    public void ActiveDiab�te()
    {
        NotActive = false;
    }
}
