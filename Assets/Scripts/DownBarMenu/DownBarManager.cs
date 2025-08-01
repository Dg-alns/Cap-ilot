using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


enum PAGE_MARK
{
    JOURNAL = 0,
    GAME,
    PROFIL,
}

public class DownBarManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject LockGame;

    [Header("Page")]
    [SerializeField] private RectTransform PageJournal;
    [SerializeField] private RectTransform PageProfil;

    [Header("Page Mark")]
    [SerializeField] private List<RectTransform> _PageMarks;
    [SerializeField] private float _speedPageMark = 10f;
    private float offSet_PageMark = 30f;
    private PAGE_MARK _activePageMark = PAGE_MARK.GAME;

    private float currentOffSetPage = 0;
    private float offSet_Page = 1800f; // Height for a page
    private float hidePagePosition = -1266f - 900f; // 1266 -> half size screen | 900 -> half offset_Page


    // Start is called before the first frame update
    void Start()
    {
        LockGame.SetActive(false);
        if (_PageMarks.Count != 3)
            Debug.LogWarning("FAILED their is more or less than 3 PageMark");
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetInteger("StateDownBar", (int)_activePageMark);

        //AutoPosition_PageMark();

        //AutoPosition_Page();
    }

    public void AutoPosition_Page()
    {
        Debug.Log(PageProfil.position.y);
        if(Mathf.Abs(PageProfil.position.y + offSet_Page + currentOffSetPage) <= 0.1)
            return;

        float newPositionY = Mathf.MoveTowards(PageProfil.position.y, - offSet_Page + currentOffSetPage, _speedPageMark * Time.deltaTime);

        Vector3 newPosition = new Vector3(PageProfil.position.x, newPositionY, PageProfil.position.z);

        PageProfil.position = newPosition;
        PageJournal.position = newPosition;
    }

    public void AutoPosition_PageMark()
    {
        for (int i = 0; i < _PageMarks.Count; i++)
        {
            float newPositionY;
            float offSet = (currentOffSetPage == 0f) ? 0 : offSet_Page;

            if (i == (int)_activePageMark){
                offSet += offSet_PageMark;
            }

            if (Mathf.Abs(_PageMarks[i].position.y - offSet) < 0.1)
                continue;

            newPositionY = Mathf.MoveTowards(_PageMarks[i].position.y, offSet, _speedPageMark * Time.deltaTime);

            _PageMarks[i].position = new Vector3(_PageMarks[i].position.x, newPositionY, _PageMarks[i].position.z);
        }
    }

    public void ChangeActivePage(int page)
    {
        Debug.Log(page);
        _activePageMark = (PAGE_MARK)page;
        //if(_activePageMark == PAGE_MARK.GAME)
        //{
        //    LockGame.SetActive(false);
        //    currentOffSetPage = 0f;
        //    return;
        //}
        //LockGame.SetActive(true);
        //currentOffSetPage = offSet_Page;

        //bool showPageProfil = _activePageMark == PAGE_MARK.JOURNAL;

        //PageProfil.gameObject.SetActive(showPageProfil);
        //PageJournal.gameObject.SetActive(!showPageProfil);
    }
}
