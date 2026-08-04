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
    [SerializeField] private TMP_Text _feedbackText; // assigner dans l'Inspector
    [SerializeField] private float _feedbackDuration = 1f;

    private void Start()
    {
        _injectionMinigame = GetComponentInParent<InjectionMinigame>();
    }

    public void ClickCircle()
    {
        float stopScale = _circle.GetComponent<RectTransform>().localScale.x;

        float average = stopScale - _scaleTarget;


        if (average < 0.5f && average > 0)
        {
            Debug.Log("Parfait : " + average);

            int score = 5;

            ShowFeedbackThenDestroy("Parfait +5 !", Color.blue);
            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            return;
        }
        
        if (average <= 3.0f && average > 0)
        {
            Debug.Log("Bien : " + average);

            int score = 3;
 
            ShowFeedbackThenDestroy("Très Bien +3 !", Color.green);
            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            return;
        }
        
        if (average > 3.0f && average > 0)
        {
            Debug.Log("RATE : " + average);

            int score = 1;

            ShowFeedbackThenDestroy("Bien +1 !", Color.yellow);
            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            return;
        }

        ShowFeedbackThenDestroy("Raté !", Color.red);

    }
    
    private void ShowFeedbackThenDestroy(string message, Color color)
    {
        if (_feedbackText != null)
        {
            _feedbackText.transform.SetParent(_injectionMinigame.transform, true);
            _feedbackText.text = message;
            _feedbackText.color = color;
            _feedbackText.gameObject.SetActive(true);
            Destroy(_feedbackText.gameObject, _feedbackDuration); // se détruira tout seul après 1 seconde
        }

        Destroy(_parent); // le script est détruit ici, mais peu importe
    }
}
