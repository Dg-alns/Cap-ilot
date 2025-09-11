using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnManagement : MonoBehaviour
{
    public LoadNexScene loadNexScene;
    public Port port;

    [Header ("\nGameObject off Player or Pnj to position")]
    public GameObject PersoToPosition;

    Dictionary<int, Vector2> dict = new Dictionary<int, Vector2>();

    static string namePLayerPrefs = "SpawnManagementPrefs";

    void Start()
    {
        if(PlayerPrefs.HasKey(namePLayerPrefs) == false) 
            PlayerPrefs.SetInt(namePLayerPrefs, 0);

        if (PersoToPosition == null)
            return;

        // Player
        if (SceneManager.GetActiveScene().name == PlayerPrefs.GetString("SceneName") && loadNexScene.GetBoatSpawn() == false) {

            PositionPLayer();
            return;
        }


        dict = DetectAllSpawnPrefab();


        if (FindPLayerPrefsPosPLayer() == false || PlayerPrefs.GetInt(namePLayerPrefs) == 1)
        {
            if (PlayerPrefs.GetInt(namePLayerPrefs) == 1)
                PlayerPrefs.SetInt(namePLayerPrefs, 0);

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

        if (port != null)
        {
            if (port.GetIsDiscover() == false)
            {
                port.IsDiscover();
                string text = "Explorer cette nouvelle île.";

                QuestManager.SetTextOffCurrentQuest(text);
            }

            if(SceneManager.GetActiveScene().name == "Port Ile Principale")
            {
                if (QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Phare))
                {
                    string text = "Aller voir le Capitain au Phare.";
                    QuestManager.SetTextOffCurrentQuest(text);
                }
            }
        }

    }

    Dictionary<int, Vector2> DetectAllSpawnPrefab()
    {
        return Tools.CreateDictOffSpawn(gameObject.name);
    }

    void PositionPLayer()
    {
        PersoToPosition.transform.position = new(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"), 0);
        PersoToPosition.transform.rotation = new(0, (PlayerPrefs.GetFloat("RotateY") == 1 ? 180 : 0), 0, 1);
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
        Debug.Log(PlayerPrefs.GetFloat("RotateY"));
    }

    public Vector3 GetPosSpeCapitain(string nameSpawn)
    {
        List<GameObject> lstGameObjectPositionCapitain = new List<GameObject>();
        lstGameObjectPositionCapitain = Tools.CreateGameObjectList<Transform>(gameObject.name);

        for (int i = 0; i < lstGameObjectPositionCapitain.Count; i++)
        {
            if (lstGameObjectPositionCapitain[i].name.Equals(nameSpawn))
                return lstGameObjectPositionCapitain[i].transform.position;
        }

        return Vector3.zero;
    }


    public static void SetFirstInScene(int value)
    {
        PlayerPrefs.SetInt(namePLayerPrefs, value);
    }
}
