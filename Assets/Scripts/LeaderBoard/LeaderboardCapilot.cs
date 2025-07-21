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
    Balance = 0,
    Boxe,
    Memory,
    Marathon,
    TirBut,
    Injection, 
    ObjCache,
}

public class LeaderboardCapilot : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _points;
    [SerializeField] private TextMeshProUGUI _LoadingText;

    [SerializeField] private TMP_InputField _inputFieldName;
    [SerializeField] private TMP_InputField _inputFieldScore;

    [SerializeField] private TMP_Dropdown _Dropdown;

    public Dictionary<EnumMinigame, string> keyValuePairs = new Dictionary<EnumMinigame, string>() {
        {EnumMinigame.Balance,"09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b" },
        {EnumMinigame.Boxe,"ef7e955364af8a5a81ed07a0802a4193422dfcfcee3ac6d37ed7659d583d3603" },
        {EnumMinigame.Memory,"06d25c3e2f5d9bb4332fcc18fed122ec8171dfeddaeeb6140c014848f53b972c" },
        {EnumMinigame.Marathon,"46249c1afafd450d2561313085548068dacf68b04588f77c7fd51b9880245617" },
        {EnumMinigame.TirBut,"86b3eb466ec9926735c291d7d1f4ee53221cad564def1311d42ea6f505f0d87f" },
        {EnumMinigame.Injection,"b725e776c9160cde2a443e0397a47173824e7d4ad6480ed015418dc9fb712c99" },
        {EnumMinigame.ObjCache,"53b131a9609c397fc867166b60b58892974598df8cf4986c8797f69c213cb93d" },
    };


    private string _publicKey = "09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b";

    private void Start()
    {
        GetLeaderBoard(_publicKey);
        _LoadingText.gameObject.SetActive(false);
    }

    public void GetLeaderBoard(string publicKey)
    {
        ResetTextLeaderboard();
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
        {
            int loopLenght = (_names.Count < msg.Length) ? _names.Count : msg.Length;
            for(int i = 0; i < loopLenght; i++)
            {
                _names[i].text = msg[i].Username;
                _points[i].text = msg[i].Score.ToString();
            }
        }), ((errorCallBack) =>
        {
        }));
    }

    public void SetLeaderboardEntry()
    {
        LeaderboardCreator.UploadNewEntry(_publicKey, _inputFieldName.text, int.Parse(_inputFieldScore.text), ((msg) =>
        {
            GetLeaderBoard(_publicKey);
        }));
    }

    public void ChangePublicKeyLeaderBoard()
    {
        Debug.Log(_Dropdown.value);

        _publicKey = keyValuePairs[(EnumMinigame)_Dropdown.value];

        GetLeaderBoard(_publicKey);
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
