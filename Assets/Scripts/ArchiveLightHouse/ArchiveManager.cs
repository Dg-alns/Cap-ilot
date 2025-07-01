using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArchiveManager : MonoBehaviour
{

    [SerializeField] private List<ArchiveScriptObj> _archivesList;

    [SerializeField] private GameObject _scrollView;
    [SerializeField] private GameObject _prefabTitle;
    


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _archivesList.Count; i++)
        {
            GameObject Title = Instantiate(_prefabTitle, _scrollView.transform);
            Title.GetComponentInChildren<TextMeshProUGUI>().text = _archivesList[i]._title;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string ShortTitle(string title)
    {


        if (title.Length > 5) { 
            title = title.Substring(0, 5);
            title = title + "...";
        }
        return title;
    }
}
