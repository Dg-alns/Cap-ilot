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
    [SerializeField] Sauvegarde Sauvegarde;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        _jsonPath = Application.dataPath + "/Json/Save.json";
        //_statMinigame = JsonUtility.FromJson<Dictionary<string, TemplateSaveMinigame>>(_jsonPath);
        _statMinigame = Sauvegarde.StatMinigame;
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

    public int GetTotalStars()
    {
        int total = 0;
        foreach(KeyValuePair<string,TemplateSaveMinigame> s in _statMinigame)
        {
            total += s.Value._nbStar;
        }
        return total;
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
        Saving save = new Saving(Sauvegarde.journal, Sauvegarde.profile, Sauvegarde.questManager, _statMinigame);
        Sauvegarde.Save(save);
    }
}
