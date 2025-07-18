using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// TODO Cédric
// X) faire le responsive du leaderboard
// ?) Ajouter un text "Chargement du leaderboard..." tout en enlevant le précédent leaderboard
// 3) Vérifier ce qui se passe lorsqu'on à pas internet pour avoir le leaderboard active
// ?) Rajouter le meilleur score du joueur en bas du leaderboard
//

public enum EnumMinigame
{
    Minigame1 = 0,
    Minigame2,
}

public class LeaderBoart_Injection : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _points;
    [SerializeField] private TextMeshProUGUI _LoadingText;

    [SerializeField] private TMP_InputField _inputFieldName;
    [SerializeField] private TMP_InputField _inputFieldScore;

    [SerializeField] private TMP_Dropdown _Dropdown;

    public Dictionary<EnumMinigame, string> keyValuePairs = new Dictionary<EnumMinigame, string>() {
        {EnumMinigame.Minigame1,"09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b" },
        {EnumMinigame.Minigame2,"ef7e955364af8a5a81ed07a0802a4193422dfcfcee3ac6d37ed7659d583d3603" }
    };


    private string _publicKey = "09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b";

    private void Start()
    {
        GetLeaderBoard(_publicKey);
        _LoadingText.gameObject.SetActive(false);
    }

    public void GetLeaderBoard(string publicKey)
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
        {
            int loopLenght = (_names.Count < msg.Length) ? _names.Count : msg.Length;
            for(int i = 0; i < loopLenght; i++)
            {
                _names[i].text = msg[i].Username;
                _points[i].text = msg[i].Score.ToString();
            }
        }));
        for (int i = _names.Count - 1; i >= 0; i--)
        {
            if (string.IsNullOrEmpty(_names[i].text))
            {
                _names[i].text = "--- | ---";
                _points[i].text = "--- | ---";
                continue;
            }
            break;
        }
    }

    public void SetLeaderboardEntry()
    {
        LeaderboardCreator.UploadNewEntry(_publicKey, _inputFieldName.text, int.Parse(_inputFieldScore.text), ((msg) =>
        {
            GetLeaderBoard(_publicKey);
        }));
    }

    public string GetNameLeaderBoard(string publicKey)
    {
        switch (publicKey)
        {
            case "09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b": return "Test Scene LeaderBoard";
            default: return "Pas de leaderboard"; 
        }
    }

    public void ChangePublicKeyLeaderBoard()
    {
        Debug.Log(_Dropdown.value);

        //StartLoading();


        _publicKey = keyValuePairs[(EnumMinigame)_Dropdown.value];

        GetLeaderBoard(_publicKey);

        //EndLoading();
        ResetTextLeaderboard();
    }

    void StartLoading()
    {
        _names[0].GetComponentInParent<Transform>().gameObject.SetActive(false);
        _LoadingText.gameObject.SetActive(true);
    }
    void EndLoading()
    {
        _names[0].GetComponentInParent<Transform>().gameObject.SetActive(true);
        _LoadingText.gameObject.SetActive(false);
    }


    private void ResetTextLeaderboard()
    {
        for (int i = 0; i < _names.Count; i++)
        {
            _names[i].text = "--- | ---";
            _points[i].text = "--- | ---";
        }
    }
}
