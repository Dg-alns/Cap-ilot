using System.Collections.Generic;
using UnityEngine;

public class DiskSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject bluePotionPrefab; // prefab bleu
    public GameObject[] redFoodPrefabs; // prefabs rouges dans le même ordre que GameController.alimentTypes

    [Header("Spawn")]
    public float spawnInterval = 3f;
    private float timer = 0f;

    private List<string> commandeToSpawn = new List<string>();
    private List<int> alimentsIndicesToSpawn = new List<int>();

    void Start()
    {
        ResetAlimentsToSpawn();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnDisk();
            timer = 0f;
        }
    }

    void ResetAlimentsToSpawn()
    {
        commandeToSpawn = GameController.instance.GetCommandeList();
        alimentsIndicesToSpawn.Clear();

        // Convertir noms aliments en indices de redFoodPrefabs
        for (int i = 0; i < commandeToSpawn.Count; i++)
        {
            string aliment = commandeToSpawn[i];
            int index = GameController.instance.alimentTypes.IndexOf(aliment);
            if (index >= 0)
                alimentsIndicesToSpawn.Add(index);
        }
    }

    void SpawnDisk()
    {
        int rand = Random.Range(0, 3); // 0,1,2
        if (rand == 0)
        {
            // Spawn potion bleue
            Instantiate(bluePotionPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int spawnIndex = -1;
            if (alimentsIndicesToSpawn.Count > 0)
            {
                int randomIndexInList = Random.Range(0, alimentsIndicesToSpawn.Count);
                spawnIndex = alimentsIndicesToSpawn[randomIndexInList];
                alimentsIndicesToSpawn.RemoveAt(randomIndexInList);
            }
            else
            {
                // Aléatoire pur après épuisement
                spawnIndex = Random.Range(0, redFoodPrefabs.Length);
            }
            Instantiate(redFoodPrefabs[spawnIndex], transform.position, Quaternion.identity);
        }
    }
}
