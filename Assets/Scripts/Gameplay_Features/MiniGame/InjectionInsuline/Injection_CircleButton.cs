using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Injection_CircleButton : MonoBehaviour
{
    [SerializeField] private float _scaleTarget = 1.6f;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _parent;
    [SerializeField] private InjectionMinigame _injectionMinigame;

    [SerializeField] private TextMeshProUGUI _textSFX;

    private void Start()
    {
        _injectionMinigame = GetComponentInParent<InjectionMinigame>();
        _textSFX.alpha = 0.0f;
    }

    public void ClickCircle()
    {
        float stopScale = _circle.GetComponent<RectTransform>().localScale.x;

        float average = Mathf.Abs(stopScale - _scaleTarget);

        if (average < 0.5f)
        {
            Debug.Log("Parfait : " + average);
            _textSFX.alpha = 1.0f;
            _textSFX.text = "Parfait!";

            // score : 700 -> 1000
            float sup = (1.0f-average / 0.5f) * 300;
            int score = (int) (sup + 1.0f) + 700;

            _injectionMinigame.AddScore(score);

            Destroy(_parent);

            new WaitForSeconds(1);
            _textSFX.alpha = 0.0f;
            return;
        }
        
        if (average <= 3.0f)
        {
            Debug.Log("Bien : " + average);
            _textSFX.alpha = 1.0f;
            _textSFX.text = "Bien!";

            // score : 400 -> 699
            float sup = 1.0f - (average - 0.5f) / 2.5f * 299;
            int score = (int) (sup+ 1.0f) + 400;
            _injectionMinigame.AddScore(score);

            Destroy(_parent);

            new WaitForSeconds(1);
            _textSFX.alpha = 0.0f;
            return;
        }
        
        if (average > 3.0f)
        {
            Debug.Log("RATE : " + average);
            _textSFX.alpha = 1.0f;
            _textSFX.text = "Raté!";

            // score : 0 -> 150
            int score = (int)(1.0f - (average - 3f) / 10f * 150);
            _injectionMinigame.AddScore(score);

            Destroy(_parent);

            new WaitForSeconds(1);
            _textSFX.alpha = 0.0f;
            return;
        }
        
    }
}
