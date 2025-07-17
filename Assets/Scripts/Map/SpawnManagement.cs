using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO Diego stocker la pos du player avant minigame dans 2 PlayerPref X Y + string Nom de la scene
// Save Player.transform.position

public class SpawnManagement : MonoBehaviour
{
    public LoadNexScene loadNexScene;

    [Header ("\nGameObject off Player or Pnj to position")]
    public GameObject PersoToPosition;

    public int Test = 1;

    Dictionary<int, Vector2> dict = new Dictionary<int, Vector2>();

    void Start()
    {
        // Capitain
        if (PersoToPosition.name.Equals("Capitain"))
        {
            List<GameObject> lstGameObjectPositionCapitain = new List<GameObject>();
            lstGameObjectPositionCapitain = Tools.CreateGameObjectList<Transform>(gameObject.name);

            if(/*DetectionSetOffDialogue*/ Test == 1) //Todo Diego Modif
            {
                GameObject go = new GameObject();

                for(int i = 0; i < lstGameObjectPositionCapitain.Count; i++)
                {
                    if( lstGameObjectPositionCapitain[i].name.Equals("StartOfGame"))
                        go = lstGameObjectPositionCapitain[i];
                }

                PersoToPosition.transform.position = new(go.transform.position.x, go.transform.position.y, 0);
                PersoToPosition.SetActive(true);
            }
            else
            {
                Debug.Log("ee");
            }

            return;
        }


        // Player
        if (SceneManager.GetActiveScene().name == PlayerPrefs.GetString("SceneName")) {
            PositionPLayer();
            return;
        }

        dict = DetectAllSpawnPrefab();

        if (FindPLayerPrefsPosPLayer() == false)
        {
            PersoToPosition.transform.position = dict[((int)SPAWN.FirstStartGame)];
            PersoToPosition.transform.rotation = new(0, 0, 0, 1);
            SavePos();
            PersoToPosition.SetActive(true);

            return;
        }


        PersoToPosition.transform.position = dict[Spawn.GetSpaw(loadNexScene.GetPreviousSceneName())];
        PersoToPosition.transform.rotation = new(0, 0, 0, 1);
        SavePos();
        PersoToPosition.SetActive(true);

    }

    Dictionary<int, Vector2> DetectAllSpawnPrefab()
    {
        return Tools.CreateDictOffSpawn(gameObject.name);
    }

    void PositionPLayer()
    {
        PersoToPosition.transform.position = new(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), 0);
        PersoToPosition.transform.rotation = new(0, PlayerPrefs.GetFloat("RotateY"), 0, 1);
        PersoToPosition.SetActive(true);
        
    }

    bool FindPLayerPrefsPosPLayer()
    {
        return PlayerPrefs.HasKey("PosX") && PlayerPrefs.HasKey("PosY") && PlayerPrefs.HasKey("RotateY");
    }


    private void SavePos()
    {
        PlayerPrefs.SetFloat("PosX", PersoToPosition.transform.position.x);
        PlayerPrefs.SetFloat("PosY", PersoToPosition.transform.position.y);
        PlayerPrefs.SetFloat("RotateY", PersoToPosition.transform.rotation.y);
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
    }
}
