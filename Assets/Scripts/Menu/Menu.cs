using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isGamedPause = false;

    Animator _Animator;

    private void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    public void MenuButton()
    {
        switch (isGamedPause)
        {
            case true: 
                Resume(); 
                //Time.timeScale = 1.0f; 
                break;
            case false: 
                Pause();
                //StartCoroutine(OneSecondTimeScale());
                break;
        }
    }

    public void Pause()
    {
        isGamedPause = true;
        _Animator.SetBool("IsPause",isGamedPause);
    }
    public void Resume()
    {
        isGamedPause = false;
        _Animator.SetBool("IsPause", isGamedPause);
    }

    public IEnumerator OneSecondTimeScale()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0.0f;
    }

}
