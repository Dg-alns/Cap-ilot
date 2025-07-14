using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiabèteBoxe : MonoBehaviour
{
    public GameObject WarningLeft;
    public GameObject WarningRight;

    public RectTransform GardeLeft;
    public RectTransform GardeRight;

    RectTransform RectTransform;
    Animator animator;

    int WarnigActif = -1;
    void Awake()
    {
        WarningLeft.SetActive(false);
        WarningRight.SetActive(false);

        animator = GetComponent<Animator>();
        RectTransform = GetComponent<RectTransform>();
    }

    public Animator GetAnimator() { return animator; }

    public int ChooseActionDiabete()
    {
        return Random.Range(1, 3);
    }

    public void LaucheAnimationDetectorAttaque(int nb)
    {
        WarnigActif = nb;

        switch(nb)
        {
            case 1:
                WarningLeft.SetActive(true);
                break;
            case 2:
                WarningRight.SetActive(true);
                break;
        }
    }

    public void ResetWarning()
    {
        switch (WarnigActif)
        {
            case 1:
                WarningLeft.SetActive(false);
                break;
            case 2:
                WarningRight.SetActive(false);
                break;
        }

        WarnigActif = -1;
    }

}
