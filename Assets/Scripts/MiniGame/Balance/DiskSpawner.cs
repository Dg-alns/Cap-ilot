using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

enum Balance_Alimentation
{
    COOKIE,
    POMME,
    POULET,
}

public class DiskSpawner : MonoBehaviour
{
    [Header("Commande")]
    public List<string> alimentTypes = new List<string> { "Pomme", "Poulet", "Cookie" };
    public int totalAliments = 10;
    private List<string> commandeToSpawn = new List<string>();
    private Dictionary<string, int> commandeCounts = new Dictionary<string, int>();

    [Header("Prefabs")]
    public GameObject bluePotionPrefab; 
    public GameObject[] redFoodPrefabs; 

    [Header("Spawn")]
    public float spawnInterval = 3f;
    private float timer = 0f;

    [Header("ParentSpawn")]
    public Transform parentSpawner;
    [Header("UI")]
    public TextMeshProUGUI remainingFood;

    public TextMeshProUGUI commandeUIText;

    //private List<int> alimentsIndicesToSpawn = new List<int>();

    public bool pause = false;

    void Start()
    {
        GenerateCommande();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (pause)
            return;

        remainingFood.text = commandeToSpawn.Count.ToString();

        UpdateCommandeUI();

        if (commandeToSpawn.Count == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnDisk();
            timer = 0f;
        }
    }
    void GenerateCommande()
    {
        commandeToSpawn.Clear();
        commandeCounts.Clear();

        foreach (string aliment in alimentTypes)
        {
            commandeCounts[aliment] = 0;
        }
        for (int i = 0; i < totalAliments; i++)
        {
            string aliment = alimentTypes[Random.Range(0, alimentTypes.Count)];
            commandeToSpawn.Add(aliment);
            commandeCounts[aliment]++;
            commandeToSpawn.Add("Potion");
        }
        UpdateCommandeUI();
    }

    void UpdateCommandeUI()
    {
        // Format: "4 Pommes ; 3 Poulets ; 3 Cookies"
        List<string> parts = new List<string>();
        foreach (var kvp in commandeCounts)
        {
            parts.Add($"{kvp.Value} {kvp.Key}{(kvp.Value > 1 ? "s" : "")}");
        }
        commandeUIText.text = string.Join("\n", parts);
    }

    public void RemoveAliment(string aliment)
    {
        commandeCounts[aliment]--;
    }

    void ResetAlimentsToSpawn()
    {
        //commandeToSpawn = GameController.instance.GetCommandeList();
        //alimentsIndicesToSpawn.Clear();

        //for (int i = 0; i < commandeToSpawn.Count; i++)
        //{
        //    string aliment = commandeToSpawn[i];
        //    int index = GameController.instance.alimentTypes.IndexOf(aliment);
        //    if (index >= 0)
        //        alimentsIndicesToSpawn.Add(index);
        //}
    }

    void SpawnDisk()
    {
        //int rand = Random.Range(0, 3);
        //if (rand == 0)
        //{
        //    Instantiate(bluePotionPrefab, transform.position, Quaternion.identity);
        //}
        //else
        //{
        //    int spawnIndex;
        //    if (alimentsIndicesToSpawn.Count > 0)
        //    {
        //        int randomIndexInList = Random.Range(0, alimentsIndicesToSpawn.Count);
        //        spawnIndex = alimentsIndicesToSpawn[randomIndexInList];
        //        alimentsIndicesToSpawn.RemoveAt(randomIndexInList);
        //    }
        //    else
        //    {
        //        spawnIndex = Random.Range(0, redFoodPrefabs.Length);
        //    }
        //    Instantiate(redFoodPrefabs[spawnIndex], transform.position, Quaternion.identity, parentSpawner);
        //}
        //

        int rand = Random.Range(0, commandeToSpawn.Count);

        switch (commandeToSpawn[rand])
        {
            case "Potion":
                Instantiate(bluePotionPrefab, transform.position, Quaternion.identity, parentSpawner);
                break;
            case "Cookie":
                Instantiate(redFoodPrefabs[(int)Balance_Alimentation.COOKIE], transform.position, Quaternion.identity, parentSpawner);
                break;
            case "Pomme":
                Instantiate(redFoodPrefabs[(int)Balance_Alimentation.POMME], transform.position, Quaternion.identity, parentSpawner);
                break;
            case "Poulet":
                Instantiate(redFoodPrefabs[(int)Balance_Alimentation.POULET], transform.position, Quaternion.identity, parentSpawner);
                break;
        }

        commandeToSpawn.RemoveAt(rand);
    }

    public int GetRemainingFood()
    {
        return commandeToSpawn.Count;
    }
}
