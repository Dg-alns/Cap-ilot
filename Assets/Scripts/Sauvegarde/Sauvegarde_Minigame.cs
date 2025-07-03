using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauvegarde_Minigame : MonoBehaviour
{

    private Dictionary<string, TemplateSaveMinigame> _statMinigame;

    private string _jsonPath = "test.json";

    // Start is called before the first frame update
    void Start()
    {
        _statMinigame = JsonUtility.FromJson<Dictionary<string, TemplateSaveMinigame>>(_jsonPath);
        //string json = JsonUtility.ToJson(_statMinigame, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMiniGameStats(string sceneName, TemplateSaveMinigame tSaveMinigame)
    {
        _statMinigame[sceneName] = tSaveMinigame;
    }

    private void SaveData()
    {

    }

    public TemplateSaveMinigame LoadData(string sceneName)
    {
        return null;
    }
}
