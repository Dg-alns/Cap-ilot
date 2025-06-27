using UnityEngine;

public class LoadNexScene : MonoBehaviour
{
    [SerializeField] private NextSceneDestination _NextSceneData;
    public Animator animator;

    public void LoadNextScene(string scene)
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.NextScene(scene));
    }

    public void LoadNewIle(string scene)
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.MiniGameBoat(scene));
    }
}
