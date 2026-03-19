using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveManager : MonoBehaviour
{

    [SerializeField] private List<ArchiveScriptObj> _archivesList;

    [SerializeField] private GameObject _scrollView;
    [SerializeField] private GameObject _prefabTitle;

    [Header("UI")]
    [SerializeField] private GameObject _articlePage;
    [SerializeField] private TextMeshProUGUI _titleArticle;
    [SerializeField] private TextMeshProUGUI _linkArticle;
    [SerializeField] private TextMeshProUGUI _contentArticle;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _archivesList.Count; i++)
        {
            GameObject Title = Instantiate(_prefabTitle, _scrollView.transform);
            Title.GetComponentInChildren<TextMeshProUGUI>().text = ShortTitle(_archivesList[i]._title);
            AddArticle(Title.GetComponent<Button>(), _archivesList[i]);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void AddArticle(Button button, ArchiveScriptObj article)
    {
        button.onClick.AddListener(() => OpenArticle(article));
    }

    public string ShortTitle(string title)
    {


        if (title.Length > 10)
        {
            title = title.Substring(0, 10);
            title = title + "...";
        }
        return title;
    }


    private void OpenArticle(ArchiveScriptObj article)
    {
        _articlePage.SetActive(true);
        _titleArticle.text = article._title;
        //_linkArticle.text = article._link;
        _linkArticle.GetComponent<Button>().onClick.RemoveAllListeners();
        _linkArticle.GetComponent<Button>().onClick.AddListener(() => AddUrl(article._link));
        _contentArticle.text = article._content;
    }


    public void AddUrl(string link)
    {
        Application.OpenURL(link);
    }
}
