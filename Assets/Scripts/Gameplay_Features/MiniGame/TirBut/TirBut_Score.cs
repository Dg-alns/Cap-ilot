using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ScoreState
{
    None,
    Shooting,
    Win,
    Lose,
}
public class TirBut_Score : MonoBehaviour
{
    // List different color associate to the score
    private readonly IReadOnlyDictionary<ScoreState, Color> _colorMap = new Dictionary<ScoreState, Color>()
    {
        {ScoreState.None ,      new Color(0.415f,0.415f,0.415f,1f) }, // LightGrey
        {ScoreState.Shooting ,  new Color(0.415f,0.415f,0.784f,1f) }, // Blue
        {ScoreState.Win ,       new Color(0.415f,0.764f,0.415f,1f) }, // Green
        {ScoreState.Lose ,      new Color(0.764f,0.415f,0.415f,1f) }, // Red
    };

    private int nbShoot = 0;
    private List<ScoreState> scoreStates;
    private List<Image> images;

    //public int score = 0;

    public bool end = false;

    [SerializeField] private Score score; 

    void Start()
    {
        scoreStates = new List<ScoreState>() { ScoreState.Shooting, ScoreState.None, ScoreState.None, ScoreState.None, ScoreState.None, };
        images = new List<Image>(GetComponentsInChildren<Image>());
        images[nbShoot].color = _colorMap[ScoreState.Shooting];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(bool goal)
    {   
        // Change color (and add score)
        if (goal)
        {
            scoreStates[nbShoot] = ScoreState.Win;
            score.AddScore();
        }
        else 
        {
            scoreStates[nbShoot] = ScoreState.Lose;
        }
        images[nbShoot].color = _colorMap[scoreStates[nbShoot]];

        // Check if it's the end
        nbShoot++;
        if(nbShoot >= images.Count)
        {
            EndGame();
            return;
        }

        // Change the color to the next one
        scoreStates[nbShoot] = ScoreState.Shooting;
        images[nbShoot].color = _colorMap[scoreStates[nbShoot]];
    }

    private void EndGame()
    {
        // Active the visual winning
        end = true;
        score.LauchScore();

    }
}
