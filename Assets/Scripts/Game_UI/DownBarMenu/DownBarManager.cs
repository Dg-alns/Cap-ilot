using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum PAGE_MARK
{
    JOURNAL = 0,
    GAME,
    PROFIL,
    CALENDRIER,
}

public class DownBarManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private PAGE_MARK _activePageMark;

    // Start is called before the first frame update
    void Start()
    {
        _activePageMark = PAGE_MARK.GAME;
        _animator.SetInteger("StateDownBar", (int)_activePageMark);

    }

    // Change the animator integer to show the appropriate page
    public void ChangeActivePage(int page)
    {
        _activePageMark = (PAGE_MARK)page;
        _animator.SetInteger("StateDownBar", (int)_activePageMark);
    }
}
