using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Commande")]
    public List<string> alimentTypes = new List<string> { "Pomme", "Poulet", "Cookie" };
    public int totalAliments = 10;
    private List<string> commandeList = new List<string>();

    [Header("UI")]
    public TextMeshProUGUI commandeUIText;
    public TextMeshProUGUI timerText;
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public Image[] stars; // 3 étoiles

    [Header("Gameplay")]
    public int lives = 3;
    public float gameDuration = 60f; // Durée en secondes
    private float timer;
    private bool gameOver = false;

    public int redCount = 0;  // nombre d’aliments dans la RedBox
    public int blueCount = 0; // nombre d’insuline dans la BlueBox

    private Dictionary<string, int> commandeCounts = new Dictionary<string, int>();
    private Dictionary<string, int> redCnt = new Dictionary<string, int>();
    private int blueCnt = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timer = gameDuration;
        GenerateCommande();
        UpdateCommandeUI();
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    void Update()
    {
        /*if (gameOver) return;

        timer -= Time.deltaTime;
        timerText.text = $"Temps : {Mathf.CeilToInt(timer)}";

        if (timer <= 0)
        {
            timer = 0;
            EndGame();
        }*/
    }

    void GenerateCommande()
    {
        commandeList.Clear();
        commandeCounts.Clear();

        // Initialise compteur
        foreach (string aliment in alimentTypes)
        {
            commandeCounts[aliment] = 0;
            redCnt[aliment] = 0; // reset aussi compteur de redCount
        }

        int total = 0;
        while (total < totalAliments)
        {
            string chosen = alimentTypes[Random.Range(0, alimentTypes.Count)];
            if (commandeCounts[chosen] < 4) // max 4 par aliment
            {
                commandeList.Add(chosen);
                commandeCounts[chosen]++;
                total++;
            }
        }
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

    public void AddDiskToScore(string colorTag, string alimentTag, bool correctBox)
    {
        if (gameOver) return;

        if (colorTag == "Red")
        {
            if (correctBox)
            {
                redCnt[alimentTag]++;
            }
            else
            {
                LoseLife();
            }
        }
        else if (colorTag == "Blue")
        {
            if (correctBox)
                blueCnt++;
            else
                LoseLife();
        }
    }

    void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            EndGame();
        }
    }

    void EndGame()
    {
        gameOver = true;
        bool commandeRespectee = true;

        // Vérifie si commande respectée
        foreach (var aliment in alimentTypes)
        {
            if (!redCnt.ContainsKey(aliment) || redCnt[aliment] != commandeCounts[aliment])
            {
                commandeRespectee = false;
                break;
            }
        }

        int diffBlueRed = blueCnt - totalAliments;

        if (commandeRespectee)
        {
            if (diffBlueRed == 0)
                ShowVictory(3);
            else
                ShowVictory(2);
        }
        else
        {
            if (lives > 0)
                ShowVictory(1);
            else
                ShowDefeat();
        }
    }

    void ShowVictory(int starCount)
    {
        victoryPanel.SetActive(true);
        defeatPanel.SetActive(false);
        for (int i = 0; i < stars.Length; i++)
            stars[i].enabled = (i < starCount);
    }

    void ShowDefeat()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(true);
    }

    // Méthode utile pour DiskSpawner
    public List<string> GetCommandeList()
    {
        return new List<string>(commandeList);
    }
    public int DiskDifference()
    {
        return blueCount - redCount;
    }

    public void AddDiskToBox(string diskTag)
    {
        if (diskTag == "Red")
            redCount++;
        else if (diskTag == "Blue")
            blueCount++;
    }
}

