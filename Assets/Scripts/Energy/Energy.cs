using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{

    [SerializeField] private int _energy;
    private int _maxEnergy;

    private int _restoreDuration = 5;

    [SerializeField] private DateTime _currentTime;    
    [SerializeField] private DateTime _energyUsedMoment;
    [SerializeField] private TimeSpan _energyDT;

    [SerializeField] private TextMeshProUGUI _textNbEnergy;
    [SerializeField] private TextMeshProUGUI _textRemainingTime;
    [SerializeField] private Slider          _energySlider;

    // Start is called before the first frame update
    void Start()
    { 
        _maxEnergy = 5;
        _energy = 5;
        //_energy = PlayerPrefs.HasKey("EnergyInStock") ? PlayerPrefs.GetInt("EnergyInStock") : _maxEnergy ;
        _energyUsedMoment = DateTime.MinValue;
        Debug.Log(_energyUsedMoment.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnergyTime();
        UpdateVisualEnergy();
    }

    private void UpdateEnergyTime()
    {
        _currentTime = DateTime.Now;

        if (_energy == _maxEnergy)
        {
            Debug.Log("Full");
            _textNbEnergy.text = "FULL";
            return;
        }

        _energyDT = _energyUsedMoment - _currentTime;
        string timeValue = String.Format("{0:D2}:{1:D1}", _energyDT.Minutes, _energyDT.Seconds);
        _textRemainingTime.text = timeValue;

        Debug.Log(_energyDT.ToString());

        if (_energyDT.Seconds <= 0)
        {
            _energy++;
            if (_energy < _maxEnergy)
            {
                RestartRestorationTime();
            }
            else 
            {
                _energyUsedMoment = DateTime.MinValue;
            }
            _textRemainingTime.text = "";
        }
    }

    private void UpdateVisualEnergy()
    {
        _textNbEnergy.text = _energy.ToString() + " / " + _maxEnergy.ToString();

        _energySlider.maxValue = _maxEnergy;
        _energySlider.value = _energy;
    }

    public void UseEnergy() 
    {
        if (_energy > 0)
        {
            _energy -= 1;
            Debug.Log(_energy);
            if(_energyUsedMoment == DateTime.MinValue)
            {
                StartRestorationTime(); 
            }
            //return true;
        }
        //return false;
    }

    void StartRestorationTime()
    {
        _energyUsedMoment = DateTime.Now;
        _energyUsedMoment = _energyUsedMoment.AddSeconds(_restoreDuration);
    }
    void RestartRestorationTime()
    {
        _energyUsedMoment = _energyUsedMoment.AddSeconds(_restoreDuration);
    }
}
