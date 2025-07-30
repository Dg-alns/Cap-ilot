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

    [SerializeField] private GameObject LockGame; 

    [Header("Page Mark")]
    [SerializeField] private List<RectTransform> _PageMarks;
    [SerializeField] private float _speedPageMark = 10f;
    private float offSet_PageMark = 30f;
    private PAGE_MARK _activePageMark = PAGE_MARK.GAME;




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
        AutoPosition_PageMark();
    }

    public void AutoPosition_PageMark()
    {
        for (int i = 0; i < _PageMarks.Count; i++)
        {
            float newPositionY;
            float offSet = 0f;

            if (i == (int)_activePageMark)
                offSet = offSet_PageMark;

            if (Mathf.Abs(_PageMarks[i].position.y - offSet) < 0.1)
                continue;

            newPositionY = Mathf.Lerp(_PageMarks[i].position.y, offSet, _speedPageMark * Time.deltaTime);

            _PageMarks[i].position = new Vector3(_PageMarks[i].position.x, newPositionY, _PageMarks[i].position.z);
        }
    }

    public void ChangeActivePage(int page)
    {
        _activePageMark = (PAGE_MARK)page;
        if(_activePageMark == PAGE_MARK.GAME)
        {
            LockGame.SetActive(false);
            return;
        }
        LockGame.SetActive(true);
    }

}
