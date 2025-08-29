using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Minigame
{
    public static GameController instance;

    //[Header("Commande")]
    //public List<string> alimentTypes = new List<string> { "Pomme", "Poulet", "Cookie" };
    //public int totalAliments = 10;
    //private List<string> commandeList = new List<string>();
    [Header("Game Script")]
    [SerializeField]List<BalanceController>   balanceController;
    [SerializeField]DiskSpawner         diskSpawner;
    [SerializeField]Transform           parentOfDisk;

    [Header("UI")]
    //public TextMeshProUGUI commandeUIText;
    public TextMeshProUGUI scoreText;
    //public GameObject victoryPanel;
    //public GameObject defeatPanel;
    //public Image[] stars;

    // [Header("Gameplay")]
    // public float gameDuration = 60f;
    // private float timer;
    // private bool gameOver = false;

    public int redCount = 0; 
    public int blueCount = 0;
    [Header("Score")]
    [SerializeField] Score score;



    // private Dictionary<string, int> redCnt = new Dictionary<string, int>();
    // private Dictionary<string, int> commandeCounts = new Dictionary<string, int>();
    //private int blueCnt = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //GenerateCommande();
    }

    void Start()
    {
        //timer = gameDuration;
        //GenerateCommande();
        //UpdateCommandeUI();
        //victoryPanel.SetActive(false);
        //defeatPanel.SetActive(false);
    }

    void Update()
    {
        scoreText.text = $"Score : {score.mCurrentScore}";

        if (diskSpawner.GetRemainingFood() != 0)
            return;

        if (parentOfDisk.childCount != 0)
            return;

        score.LauchScore();

        /*if (gameOver) return;

        timer -= Time.deltaTime;
        timerText.text = $"Temps : {Mathf.CeilToInt(timer)}";

        if (timer <= 0)
        {
            timer = 0;
            EndGame();
        }*/
    }

    //void GenerateCommande()
    //{
    //    commandeList.Clear();
    //    commandeCounts.Clear();

    //    foreach (string aliment in alimentTypes)
    //    {
    //        commandeCounts[aliment] = 0;
    //        redCnt[aliment] = 0;
    //    }

    //    int total = 0;
    //    while (total < totalAliments)
    //    {
    //        string chosen = alimentTypes[Random.Range(0, alimentTypes.Count)];
    //        if (commandeCounts[chosen] < 4) // max 4 par aliment
    //        {
    //            commandeList.Add(chosen);
    //            commandeCounts[chosen]++;
    //            total++;
    //        }
    //    }
    //}

    //void GenerateCommande()
    //{
    //    commandeList.Clear();

    //    for (int i = 0; i < totalAliments; i++)
    //    {
    //        commandeList.Add(alimentTypes[Random.Range(0, alimentTypes.Count)]);
    //        commandeList.Add("Potion");
    //    }
    //}

    //void UpdateCommandeUI()
    //{
    //    // Format: "4 Pommes ; 3 Poulets ; 3 Cookies"
    //    List<string> parts = new List<string>();
    //    foreach (var kvp in commandeCounts)
    //    {
    //        parts.Add($"{kvp.Value} {kvp.Key}{(kvp.Value > 1 ? "s" : "")}");
    //    }
    //    commandeUIText.text = string.Join("\n", parts);
    //}

    public void AddDiskToScore(string colorTag, string alimentTag, bool correctBox)
    {
        if (!correctBox)
            return;
        if (colorTag == "Red")
        {
            score.AddScore(); // ajout de score
            diskSpawner.RemoveAliment(alimentTag);
        }
        else if (colorTag == "Blue")
        {
            score.AddScore();// ajout de score
        }
    }


    void EndGame()
    {
        //gameOver = true;
        //bool commandeRespectee = true;

        //foreach (var aliment in alimentTypes)
        //{
        //    if (!redCnt.ContainsKey(aliment) || redCnt[aliment] != commandeCounts[aliment])
        //    {
        //        commandeRespectee = false;
        //        break;
        //    }
        //}

        //int diffBlueRed = blueCnt - totalAliments;

        //if (commandeRespectee)
        //{
        //    if (diffBlueRed == 0)
        //        ShowVictory(3);
        //    else
        //        ShowVictory(2);
        //}
        //else
        //{
        //        ShowDefeat();
        //}
    }

    //void ShowVictory(int starCount)
    //{
    //    victoryPanel.SetActive(true);
    //    defeatPanel.SetActive(false);
    //    for (int i = 0; i < stars.Length; i++)
    //        stars[i].enabled = (i < starCount);
    //}

    //void ShowDefeat()
    //{
    //    victoryPanel.SetActive(false);
    //    defeatPanel.SetActive(true);
    //}

    //public List<string> GetCommandeList()
    //{
    //    return new List<string>(commandeList);
    //}

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

    public override void PauseMinigame() 
    {
        foreach (BalanceController bc in balanceController)
        {
            bc.pause = true;
        }
        diskSpawner.pause = true;
        foreach(Disk disk in parentOfDisk.GetComponentsInChildren<Disk>())
        {
            disk.Pause();
        }
    }

    public override void ResumeMinigame()
    {
        foreach(BalanceController bc in balanceController)
        {
            bc.pause = false;
        }
        diskSpawner.pause = false;
        foreach (Disk disk in parentOfDisk.GetComponentsInChildren<Disk>())
        {
            disk.Pause();
        }
    }

    public void StartGame()
    {
        foreach (BalanceController bc in balanceController)
        {
            bc.gameObject.SetActive(true);
        }
        diskSpawner.gameObject.SetActive(true);
        foreach (Disk disk in parentOfDisk.GetComponentsInChildren<Disk>())
        {
            disk.gameObject.SetActive(true);
        }
    }
}

