using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chronometre : MonoBehaviour
{
    [SerializeField] private float _chrono;

    [SerializeField] private float _secondAssociate;
    [SerializeField] private float _miliSecondAssociate;

    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private Animator _animator;

    private bool _running;

    // Start is called before the first frame update
    void Start()
    {
        _running = false;
        _chrono = 10.0f;
        SetAssociateTimer();

        //_animator = GetComponent<Animator>();
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowChrono();

        if (!_running) return;

        _chrono -= Time.deltaTime;
        SetAssociateTimer();

        if (_chrono < 0)
        {

            // Joue une animation en rouge pour echec => 0 points
            _running = false;
            return;
        }
    }

    void SetAssociateTimer()
    {
        _secondAssociate = (int) _chrono;  
        _miliSecondAssociate = (int)((_chrono - _secondAssociate) * 1000);
        if (_miliSecondAssociate < 0)
        {
            _miliSecondAssociate = 0;
        }
    }

    void ShowChrono()
    {
        string ajustSecond = (_secondAssociate < 10.0f) ? "0" : "";
        string ajustMiliSecond = (_miliSecondAssociate < 10.0f) ? "00" : (_miliSecondAssociate < 100.0f) ? "0" : "";

        string textChrono = ajustSecond + _secondAssociate + " : " + _miliSecondAssociate + ajustMiliSecond;
        _textMeshPro.text = textChrono;
    }

    public void SetChrono(float value)
    {
        _chrono = value;
        SetAssociateTimer();
    }

    public void StartChrono()
    {
        _running = true;

    }

}
