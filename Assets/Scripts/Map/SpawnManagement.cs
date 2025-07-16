using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO Diego stocker la pos du player avant minigame dans 2 PlayerPref X Y + string Nom de la scene
// Save Player.transform.position

public class SpawnManagement : MonoBehaviour
{
    public GameObject Player;

    public LoadNexScene loadNexScene;

    Dictionary<int, Vector2> dict = new Dictionary<int, Vector2>();

    void Start()
    {
        dict = DetectAllSpawnPrefab();

        if (loadNexScene.GetPreviousSceneName().Contains("Accueil") /*Detection Position playerPref detected*/)
        {
            Player.transform.position = dict[((int)SPAWN.FirstStartGame)];
            Player.transform.rotation = new(0, 0, 0, 1);
            Player.SetActive(true);

            return;
        }
        

        Player.transform.position = dict[Spawn.GetSpaw(loadNexScene.GetPreviousSceneName())];
        Player.SetActive(true);

    }

    Dictionary<int, Vector2> DetectAllSpawnPrefab()
    {
        return Tools.CreateDictOffNameAndSpawn(gameObject.name);
    }
}
