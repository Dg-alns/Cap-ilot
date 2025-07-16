using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTION
{
    Idle,
    GardeGauche,
    GardeDroite,
    AttaqueDroite,
    AttaqueGauche
}

public class ActionOfPLayer : MonoBehaviour
{
    Animator animator;
    ACTION action = ACTION.Idle;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Animator GetAnimator() {  return animator; }

    public void ChangeActionOfPLayer(ACTION actionPlayer)
    {
        action = actionPlayer;
        StartAnimation();
    }

    public void StartAnimation()
    {
        animator.SetInteger("Action", (int)action);
    }

    public void ActiveIdle()
    {
        action = 0;
        animator.SetInteger("Action", (int)action);
    }
}
