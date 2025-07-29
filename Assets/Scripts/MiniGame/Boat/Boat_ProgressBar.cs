using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boat_ProgressBar : MonoBehaviour
{
    private Image _progressBar;
    [SerializeField] private float _targetTime;

    [SerializeField] private float _progressTime;

    public bool isRunning = false;

    private void Start()
    {
        _progressBar = GetComponent<Image>();
        _progressBar.fillAmount = 0;
    }
    private void Update()
    {
        if (isRunning == false)
            return;

        _progressTime += Time.deltaTime;

        _progressBar.fillAmount = _progressTime/_targetTime;
    }

    public float GetTime()
    {
        return _progressTime;
    }
    public void AddTime(float time)
    {
        _progressTime += time;
        if(_progressTime < 0.0f) _progressTime = 0.0f;
    }

    public void Run()
    {
        isRunning = true;
    }
}
