using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;

public class LeaderBoart_Injection : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _points;
    [SerializeField] private TextMeshProUGUI _nameLeaderboard;

    [SerializeField] private TMP_InputField _inputFieldName;
    [SerializeField] private TMP_InputField _inputFieldScore;

    private string _publicKey = "09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b";

    private void Start()
    {
        GetLeaderBoard(_publicKey);
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
}
