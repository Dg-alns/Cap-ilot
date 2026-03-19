using System.Collections.Generic;
using System.IO;
using Dan.Main;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public enum EnumMinigame
{
    Balance = 0,
    Boxe,
    Memory,
    Marathon,
    TirBut,
    Injection, 
    ObjCache,
    Funambule,
}

public class LeaderboardCapilot : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _points;
    [SerializeField] private TextMeshProUGUI _LoadingText;

    [SerializeField] private TMP_InputField _inputFieldName;
    [SerializeField] private TMP_InputField _inputFieldScore;

    [SerializeField] private TMP_Dropdown _Dropdown;

    private string _username;

    public Dictionary<string, EnumMinigame> nameScene_EnumMinigame = new Dictionary<string, EnumMinigame>() {
        { "MiniGame_Balance",EnumMinigame.Balance},
        { "MiniGame_Boxe",EnumMinigame.Boxe},
        { "MiniGame_Card",EnumMinigame.Memory},
        { "MiniGame_Marathon",EnumMinigame.Marathon},
        { "MiniGame_TirBut",EnumMinigame.TirBut},
        { "MiniGame_InjectionInsuline",EnumMinigame.Injection},
        { "MiniGame_ObjCachee",EnumMinigame.ObjCache},
        { "MiniGame_Funambule",EnumMinigame.Funambule},
    };

    // Public Key associate to the minigame
    public Dictionary<EnumMinigame, string> keyValuePairs = new Dictionary<EnumMinigame, string>() {
        {EnumMinigame.Balance,"09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b" },
        {EnumMinigame.Boxe,"ef7e955364af8a5a81ed07a0802a4193422dfcfcee3ac6d37ed7659d583d3603" },
        {EnumMinigame.Memory,"06d25c3e2f5d9bb4332fcc18fed122ec8171dfeddaeeb6140c014848f53b972c" },
        {EnumMinigame.Marathon,"46249c1afafd450d2561313085548068dacf68b04588f77c7fd51b9880245617" },
        {EnumMinigame.TirBut,"86b3eb466ec9926735c291d7d1f4ee53221cad564def1311d42ea6f505f0d87f" },
        {EnumMinigame.Injection,"b725e776c9160cde2a443e0397a47173824e7d4ad6480ed015418dc9fb712c99" },
        {EnumMinigame.ObjCache,"53b131a9609c397fc867166b60b58892974598df8cf4986c8797f69c213cb93d" },
        {EnumMinigame.Funambule,"01d02aa91f5c606511acc626dbb4eaa24fb4f7076eca2823e1fc4e077d3cbfc2" },
    };

    // Default Key
    private string _publicKey = "09af4957d862953d2f6c4cfe207c202aa761cdb476e9ba4431f28b802bdc7d7b";

    private void Start()
    {
        Saving save = JSON_Manager.LoadData("Save");
        _username = save.profile.Username;

        GetLeaderBoard(_publicKey);
    }

    public void GetLeaderBoard(string publicKey)
    {
        // First Reset the leaderboard Text (names + points)  
        ResetTextLeaderboard();

        // Get and assign the leaderboard associate with the key
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

    // Set in the leaderboard a score (if it's lower than the previous one, nothing change apart the nickname of the player)
    public void SetLeaderboardEntry()
    {
        LeaderboardCreator.UploadNewEntry(_publicKey, _inputFieldName.text, int.Parse(_inputFieldScore.text), ((msg) =>
        {
            GetLeaderBoard(_publicKey);
        }));
    }

    // Set in the leaderboard a score (if it's lower than the previous one, nothing change apart the nickname of the player)
    public void AddScoreInLeaderboard(int score)
    {
        string gameKey = keyValuePairs[nameScene_EnumMinigame[SceneManager.GetActiveScene().name]];
        LeaderboardCreator.UploadNewEntry(gameKey, _username, score, ((msg) =>
        {
            GetLeaderBoard(_publicKey);
        }));
    }

    // Get the new public key associate with the dropdown in the menu leaderboard
    public void ChangePublicKeyLeaderBoard()
    {
        Debug.Log(_Dropdown.value);

        _publicKey = keyValuePairs[(EnumMinigame)_Dropdown.value];

        GetLeaderBoard(_publicKey);
    }

    // Reset the Text of the leaderboard (names + points)
    private void ResetTextLeaderboard()
    {
        for (int i = 0; i < _names.Count; i++)
        {
            _names[i].text = "--- | ---";
            _points[i].text = "--- | ---";
        }
    }
}
