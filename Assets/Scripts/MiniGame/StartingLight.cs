using UnityEngine;

public class StartingLight : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LauchLight()
    {
        animator.SetBool("GoLight", true);
    }

    void LauchHide()
    {
        animator.SetBool("GoLight", false);
        animator.SetBool("GoHide", true);
    }
}
