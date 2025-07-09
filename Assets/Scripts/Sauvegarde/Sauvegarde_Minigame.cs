using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class Sauvegarde_Minigame : MonoBehaviour
{
    private Dictionary<string, TemplateSaveMinigame> _statMinigame;
    private string _jsonPath;
    string sceneName;
    public Score score;

    bool _state = true;

    // Start is called before the first frame update
    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        _jsonPath = Application.dataPath + "/Json/SaveMiniGame.json";
        //_statMinigame = JsonUtility.FromJson<Dictionary<string, TemplateSaveMinigame>>(_jsonPath);
        try
        {
            _statMinigame = JSON_Manager.LoadData<Dictionary<string, TemplateSaveMinigame>>(_jsonPath);
        }
        catch
        {
            _statMinigame = new Dictionary<string, TemplateSaveMinigame>();
            JSON_Manager.SaveData(_jsonPath, _statMinigame);
        }

        //_statMinigame = new Dictionary<string, TemplateSaveMinigame>();
        //_statMinigame["TSET"] = new TemplateSaveMinigame(10, 0, true);
        //_statMinigame["TEST"] = new TemplateSaveMinigame(0, 10, false);

        //string json = JsonConvert.SerializeObject(_statMinigame);
        //Debug.Log(json);
        //Debug.Log(_statMinigame["TEST"]._bestScore);
        //Debug.Log(_statMinigame);
        //File.WriteAllText(_jsonPath, json);
        //string a = File.ReadAllText(json);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMiniGameStats(string sceneName, TemplateSaveMinigame tSaveMinigame)
    {
        _statMinigame[sceneName] = tSaveMinigame;
    }

    public int GetBestScore(string sceneName)
    {
        Debug.Log(sceneName);
        Debug.Log(_statMinigame.ContainsKey(sceneName));
        if(_statMinigame.ContainsKey(sceneName))
            return _statMinigame[sceneName]._bestScore;
        return 0;
    }

    public int GetnbStars(string sceneName)
    {
        if(_statMinigame.ContainsKey(sceneName))
            return _statMinigame[sceneName]._nbStar;
        return 0;
    }

    public bool GetCanShowInfo(string sceneName)
    {
        if(_statMinigame.ContainsKey(sceneName))
            return _statMinigame[sceneName]._showInfo;
        return _state;
    }

    public void SetCanShowInfo(bool state)
    {
        if(_statMinigame.ContainsKey(sceneName))
            _statMinigame[sceneName]._showInfo = state;

        else
            _state = state;

    }

    private void SaveData()
    {

    }

    public TemplateSaveMinigame LoadData(string sceneName)
    {
        return null;
    }

    public void SaveMiniGame(bool reverse)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
        if (_statMinigame.ContainsKey(sceneName))
        {
            Debug.Log("LLALA");
            _statMinigame[sceneName].CheckNewScore(score, reverse);
        }
        else{
            _statMinigame[sceneName] = new TemplateSaveMinigame(score.nbStars, score.MiniGamePoint, _state);
        }

        JSON_Manager.SaveData(_jsonPath, _statMinigame);
    }
}
