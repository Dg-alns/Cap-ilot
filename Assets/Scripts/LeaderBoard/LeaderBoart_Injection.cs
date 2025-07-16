using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Dan.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoart_Injection : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _points;

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
    }

    public void SetLeaderboardEntry()
    {
        LeaderboardCreator.UploadNewEntry(_publicKey, _inputFieldName.text, int.Parse(_inputFieldScore.text), ((msg) =>
        {
            GetLeaderBoard(_publicKey);
        }));
    }

}
