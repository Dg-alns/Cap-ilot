using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNexScene : MonoBehaviour
{
    [SerializeField] private NextSceneDestination _NextSceneData;
    public Animator animator;

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    public void LoadNextScene(string scene)
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.NextScene(scene));
    }
}
