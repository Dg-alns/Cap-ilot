using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InjectionMinigame : Minigame
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private Score _score;

    [SerializeField] private TextMeshProUGUI _textScore;

    [SerializeField] private List<float> _spawnTiming;

    private float _time;

    [SerializeField] private Injection_Body _body;

    [SerializeField] private Transform _partentTimingCircle;

    private bool _swappingTime = false;

    private float _defaultCircleSize;

    private float _heightMenuButton = -(50 + 150); // Y position + height

    private int _gameScore;

    private bool isPause = false;


    private void Start()
    {
        _time = 0;
        _gameScore = 0;
        string visualScore = String.Format("{0:D5}", _gameScore);
        _textScore.text = "Score : " + visualScore;
        _spawnTiming = new List<float>();
        GameObject circle = GameObject.Find("Reference_W_Circle");
        _defaultCircleSize = circle.GetComponent<RectTransform>().rect.width * circle.GetComponent<RectTransform>().localScale.x;
    }
    private void Update()
    {
        if (isPause) return;

        if (_swappingTime) return;

        if (_spawnTiming.Count > 0)
        {
            _time += Time.deltaTime;
            if (_spawnTiming[0] < _time)
            {
                CreateRandomTimingCircle();
            }
            return;
        }

        if (!IsRemaingCircle() && !_body.IsFinish())
        {
            StartCoroutine(SwapBodyPart());
            return;
        }

    }

    public void GenerateFuturCircle(int nbCircle = 3)
    {
        _spawnTiming.Clear();

        _spawnTiming.Add(UnityEngine.Random.Range(0.5f, 1.5f));

        for (int i = 1; i < nbCircle; i++)
        {
            _spawnTiming.Add(UnityEngine.Random.Range(_spawnTiming[i - 1] + 1.0f, _spawnTiming[i - 1] + 1.5f));
        }
        _time = 0;
    }

    public void CreateRandomTimingCircle()
    {
        // Create an instance of the TimingCircle prefab
        GameObject timingCircle = GameObject.Instantiate(_prefab, _partentTimingCircle);

        // Set a random position
        timingCircle.transform.position = new Vector2(UnityEngine.Random.Range(_defaultCircleSize, Screen.width - _defaultCircleSize), UnityEngine.Random.Range(_defaultCircleSize, Screen.height - _defaultCircleSize - _heightMenuButton));
        
        // Put the instance behind the other create before
        timingCircle.transform.SetAsFirstSibling();

        // Remove the creation time of the list
        _spawnTiming.RemoveAt(0);
    }

    public bool IsRemaingCircle()
    {
        return _partentTimingCircle.childCount > 0;
    }

    public void AddScore(int score)
    {
        _gameScore += score;
        string visualScore = String.Format("{0:D5}", _gameScore);
        _textScore.text = "Score : " + visualScore;
    }

    IEnumerator SwapBodyPart()
    {
        _swappingTime = true;
        yield return new WaitForSeconds(1);
        _body.ReturnToIdle();

        yield return new WaitForSeconds(3);
        _body.NextBodyPart();

        yield return new WaitForSeconds(1);

        if(!_body.IsFinish())
        {
            GenerateFuturCircle();
        }
        else
        {
            Debug.Log(_gameScore);
            _score.SetScore(_gameScore);
            _score.LauchScore();
        }
        _swappingTime = false;
    }
    public override void PauseMinigame()
    {
        isPause = true;
        if(IsRemaingCircle())
        {
            foreach(Injection_Circle circle in _partentTimingCircle.GetComponentsInChildren<Injection_Circle>())
            {
                circle.Pause();
            }
        }
    }

    public override void ResumeMinigame()
    {
        isPause = false;
        if (IsRemaingCircle())
        {
            foreach (Injection_Circle circle in _partentTimingCircle.GetComponentsInChildren<Injection_Circle>())
            {
                circle.Resume();
            }
        }
    }
}
