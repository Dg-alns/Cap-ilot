using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Timer UI")]
    public Text timerText;  // Texte pour afficher le timer (ex: "90")

    [Header("Commande UI")]
    public Text commandeText; // Texte pour afficher la commande (ex: "4 Pommes\n3 Poulets\n3 Cookies")

    [Header("Score UI")]
    public Text scoreText; // Affichage score

    [Header("Vies UI")]
    public Text livesText; // Affichage vies restantes

    [Header("Panels de fin")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Étoiles UI")]
    public Image star1;
    public Image star2;
    public Image star3;

    public Sprite starEmptySprite; // Sprite étoile vide
    public Sprite starFullSprite;  // Sprite étoile pleine

    void Start()
    {
        HideEndPanels();
    }

    public void UpdateTimer(int secondsRemaining)
    {
        timerText.text = secondsRemaining.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Vies : " + lives;
    }

    // Affiche la commande en listant les aliments avec leurs quantités
    public void UpdateCommande(int pommes, int poulets, int cookies)
    {
        commandeText.text = "";
        if (pommes > 0) commandeText.text += pommes + " Pommes\n";
        if (poulets > 0) commandeText.text += poulets + " Poulets\n";
        if (cookies > 0) commandeText.text += cookies + " Cookies\n";
    }

    public void ShowWinPanel(int stars)
    {
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        UpdateStars(stars);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        winPanel.SetActive(false);
        UpdateStars(0);
    }

    public void HideEndPanels()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Affiche les étoiles selon le score (1, 2 ou 3)
    void UpdateStars(int stars)
    {
        star1.sprite = stars >= 1 ? starFullSprite : starEmptySprite;
        star2.sprite = stars >= 2 ? starFullSprite : starEmptySprite;
        star3.sprite = stars >= 3 ? starFullSprite : starEmptySprite;
    }
}
