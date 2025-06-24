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
    private readonly IReadOnlyDictionary<ScoreState, Color> _colorMap = new Dictionary<ScoreState, Color>()
    {
        {ScoreState.None ,      new Color(0.415f,0.415f,0.415f,1f) },
        {ScoreState.Shooting ,  new Color(0.415f,0.415f,0.784f,1f) },
        {ScoreState.Win ,       new Color(0.415f,0.764f,0.415f,1f) },
        {ScoreState.Lose ,      new Color(0.764f,0.415f,0.415f,1f) },
    };

    private int nbShoot = 0;
    private List<ScoreState> scoreStates;
    private List<Image> images;

    public int score = 0;
    // Start is called before the first frame update
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
        if (goal)
        {
            scoreStates[nbShoot] = ScoreState.Win;
            score++;
        }
        else 
        {
            scoreStates[nbShoot] = ScoreState.Lose;
        }
        images[nbShoot].color = _colorMap[scoreStates[nbShoot]];

        nbShoot++;
        if(nbShoot >= images.Count) return;

        scoreStates[nbShoot] = ScoreState.Shooting;
        images[nbShoot].color = _colorMap[scoreStates[nbShoot]];
    }
}
