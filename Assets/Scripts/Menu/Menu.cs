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

        // Unactive the button "Quit minigame" if we are'n in a minigame
        if (!SceneManager.GetActiveScene().name.Contains("MiniGame"))
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
                break;
            case false: 
                Pause();
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
    
    // Swap Between the Menu Side and the LeaderBoard Side
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
}
