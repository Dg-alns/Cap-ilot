using UnityEngine;

public class LoadNexScene : MonoBehaviour
{
    [SerializeField] private NextSceneDestination _NextSceneData;

    [SerializeField] private Energy _energy;

    public Animator animator;


    public void LoadNextScene(string scene)
    {
        
        if (_NextSceneData.isLauch == false)
        {
            _NextSceneData.isLauch = true;
            animator.SetTrigger("Transition");
            StartCoroutine(_NextSceneData.NextScene(scene));
        }
    }

    public void LoadMiniGame(string scene)
    {
        if (_NextSceneData.isLauch == false)
        {
            _NextSceneData.isLauch = true;
            if (_energy.HaveEnergy())
            {
                _energy.UseEnergy();
                animator.SetTrigger("Transition");
                StartCoroutine(_NextSceneData.NextScene(scene));
            }
        }
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
        if(_NextSceneData.isLauch == false)
        {
            animator.SetTrigger("Transition");
            StartCoroutine(_NextSceneData.NewIle());
            _NextSceneData.isLauch = true;
        }
    }
}
