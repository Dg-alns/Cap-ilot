using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Injection_Circle : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    private float _speed;

    [SerializeField] private InjectionMinigame _injectionMinigame;
    [SerializeField] private TMP_Text _feedbackText; // assigner dans l'Inspector
    [SerializeField] private float _feedbackDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _speed = 5f;
        _injectionMinigame = GetComponentInParent<InjectionMinigame>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = Time.deltaTime * _speed;
        transform.localScale = new Vector3(transform.localScale.x - step, transform.localScale.y - step, transform.localScale.z);
        if (transform.localScale.x <= 0f)
        {
            Debug.Log("Raté Noob");
            ShowFeedbackThenDestroy("Raté !", Color.red);
            Destroy(_parent);
        }
    }

    public void Pause()
    {
        _speed = 0f;
    }
    public void Resume()
    {
        _speed = 5f;
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
