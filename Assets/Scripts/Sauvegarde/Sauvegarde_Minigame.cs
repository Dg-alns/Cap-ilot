using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sauvegarde_Minigame : MonoBehaviour
{
    private SerializableDictionary<string, TemplateSaveMinigame> _statMinigame;
    private string _jsonPath;
    string sceneName;
    public Score score;

    bool _state = true;
    [SerializeField] Sauvegarde _Sauvegarde;
    // Start is called before the first frame update

//     private void Init()
//     {
//         sceneName = SceneManager.GetActiveScene().name;
//         _jsonPath = Application.dataPath + "/Json/Save.json";
//         //_statMinigame = JsonUtility.FromJson<Dictionary<string, TemplateSaveMinigame>>(_jsonPath);

//         _statMinigame = Sauvegarde.StatMinigame;
//     }

    void Start()
    {

        //Init();
        _statMinigame = _Sauvegarde.StatMinigame;
        Debug.Log("Nombre de stat : " + _statMinigame.Count);
        /*try
        {
            _statMinigame = JSON_Manager.LoadData<Dictionary<string, TemplateSaveMinigame>>(_jsonPath);
        }
        catch
        {
            _statMinigame = new Dictionary<string, TemplateSaveMinigame>();
            JSON_Manager.SaveData(_jsonPath, _statMinigame);
        }*/

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
        if (_statMinigame == null)
            return 0;

        if (_statMinigame.ContainsKey(sceneName))
            return _statMinigame[sceneName]._bestScore;


        return 0;
    }

    public int GetnbStars(string sceneName)
    {
        if (_statMinigame == null)
            return 0;

        if(_statMinigame.ContainsKey(sceneName))
            return _statMinigame[sceneName]._nbStar;
        return 0;
    }

    public int GetTotalStars()
    {
        if (_statMinigame == null)
            return 0;

        int total = 0;

        foreach(KeyValuePair<string,TemplateSaveMinigame> s in _statMinigame)
        {
            total += s.Value._nbStar;
        }
        return total;
    }

    public bool GetCanShowInfo(string sceneName)
    {        
        if(_statMinigame == null)
            return false;

        if (_statMinigame.ContainsKey(sceneName))
            return _statMinigame[sceneName]._showInfo;
        return _state;
    }

    public bool HaveMiniGame(string sceneName)
    {        
        if(_statMinigame == null)
            return false;

        if (_statMinigame.ContainsKey(sceneName))
            return true;

        return false;
    }

    public void SetCanShowInfo(string sceneName, bool state)
    {
        if (_statMinigame == null)
            return;

        if (_statMinigame.ContainsKey(sceneName))
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

        if (_statMinigame.ContainsKey(sceneName))
        {
            _statMinigame[sceneName].CheckNewScore(score, reverse);
        }
        else{
            _statMinigame[sceneName] = new TemplateSaveMinigame(score.nbStars, score.MiniGamePoint, _state);
        }
        Saving save = new Saving(_Sauvegarde.journal, _Sauvegarde.profile, _Sauvegarde.questManager, _statMinigame, _Sauvegarde.StatPlayer);
        _Sauvegarde.Save(save);
    }
}
