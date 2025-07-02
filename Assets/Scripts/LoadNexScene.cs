using UnityEngine;

public class LoadNexScene : MonoBehaviour
{
    [SerializeField] private NextSceneDestination _NextSceneData;
    public Animator animator;

    bool isLauch = false;

    public void LoadNextScene(string scene)
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.NextScene(scene));
    }

    public void LoadPreviousScene()
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.NextScene(_NextSceneData.GetPreviousScene()));
    }

    public void LoadNewIle(string scene)
    {
        animator.SetTrigger("Transition");
        StartCoroutine(_NextSceneData.MiniGameBoat(scene));
    }

    public void LoadIle()
    {
        if(isLauch == false)
        {
            animator.SetTrigger("Transition");
            StartCoroutine(_NextSceneData.NewIle());
            isLauch = true;
        }
    }
}
