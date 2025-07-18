using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public bool isGamedPause = false;
    public bool isMenuSide = true;

    Animator _Animator;

    [SerializeField] private GameObject _buttonQuitMinigame;

    private void Start()
    {
        _Animator = GetComponent<Animator>();
        if (!SceneManager.GetActiveScene().name.Contains("Menu"))
        {
            _buttonQuitMinigame.SetActive(false);
        }
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
    
    public void MenuSide()
    {
        isMenuSide = true;
        _Animator.SetBool("IsMenuSide",isMenuSide);
    }
    public void LeaderboardSide()
    {
        isMenuSide = false;
        _Animator.SetBool("IsMenuSide", isMenuSide);
    }

    public IEnumerator OneSecondTimeScale()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0.0f;
    }

}
