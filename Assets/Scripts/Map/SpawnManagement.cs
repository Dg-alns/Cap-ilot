using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Diego stocker la pos du player avant minigame dans 2 PlayerPref X Y + string Nom de la scene

public class SpawnManagement : MonoBehaviour
{
    public GameObject Player;

    LoadNexScene loadNexScene;

    Dictionary<SPAWN, Vector2> dict = new Dictionary<SPAWN, Vector2>();

    void Start()
    {
        loadNexScene = GetComponent<LoadNexScene>();

        dict = DetectAllSpawnPrefab();

        if(loadNexScene.GetPreviousSceneName().Contains("Accueil") /*Detection Position playerPref detected*/)
        {
            Player.transform.position = new Vector3(dict[SPAWN.FirstStartGame].x, dict[SPAWN.FirstStartGame].y, 0);
            Debug.Log(Player.transform.position);
        }

    }


    Dictionary<SPAWN, Vector2> DetectAllSpawnPrefab()
    {
        return Tools.CreateDictOffNameAndSpawn(gameObject.name);
    }
}
