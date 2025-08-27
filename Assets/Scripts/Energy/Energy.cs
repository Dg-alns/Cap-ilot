using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private int _energy;
    private int _maxEnergy;

    [SerializeField] private int _restoreDuration;

    private DateTime _currentTime;    
    private DateTime _nextEnergyTime;
    private TimeSpan _energyDT;

    [SerializeField] private TextMeshProUGUI _textNbEnergy;
    [SerializeField] private TextMeshProUGUI _textRemainingTime;
    [SerializeField] private Slider          _energySlider;

    // Start is called before the first frame update
    void Start()
    { 
        _maxEnergy = 5;
        _currentTime = DateTime.Now;

        // If you have played before -> Load Data
        if (PlayerPrefs.HasKey("CurrentEnergy"))
        {
            _energy = PlayerPrefs.GetInt("CurrentEnergy");
            _nextEnergyTime = StringToDate(PlayerPrefs.GetString("NextEnergyTime"));
        }
        // If you have never played before -> Create Data
        else
        {
            _energy = 5;
            _nextEnergyTime = DateTime.MinValue;
            PlayerPrefs.SetInt("CurrentEnergy",5);
            PlayerPrefs.SetString("NextEnergyTime",DateTime.MinValue.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime = DateTime.Now;

        UpdateEnergyTime();

        UpdateVisualEnergy();

        Save();
    }

    // Update that make you restore your energy when you used some
    private void UpdateEnergyTime()
    {
        if (_energy >= _maxEnergy)
        {
            _textRemainingTime.text = "";
            return;
        }

        // Get the DeltaTime
        _energyDT = _nextEnergyTime - _currentTime;
        string timeValue = String.Format("{0:D2}:{1:D2}", _energyDT.Minutes, _energyDT.Seconds);
        _textRemainingTime.text = timeValue;

        if (_energyDT.Seconds <= 0)
        {
            _energy++;
            Debug.Log(_nextEnergyTime);
            // If you still don't have full energy
            if (_energy < _maxEnergy)
            {

                Debug.Log("Je le restart");
                RestartRestorationTime();
                Debug.Log(_nextEnergyTime);
            }
            // If you have full energy 
            else
            {
                _nextEnergyTime = DateTime.MinValue;
            }
            _textRemainingTime.text = "";
        }
    }

    // Update the energy bar and text
    private void UpdateVisualEnergy()
    {
        _textNbEnergy.text = _energy.ToString() + " / " + _maxEnergy.ToString();

        _energySlider.maxValue = _maxEnergy;
        _energySlider.value = _energy;
    }

    // Save data in the PlayerPrefs
    private void Save()
    {
        PlayerPrefs.SetInt("CurrentEnergy", _energy);
        PlayerPrefs.SetString("NextEnergyTime", _nextEnergyTime.ToString());
    }
    
    // Return a DateTime with a string
    private DateTime StringToDate(string str)
    {
        if(String.IsNullOrEmpty(str)) 
            return DateTime.MinValue;

        return DateTime.Parse(str);
    }

    public bool HaveEnergy()
    {
        return _energy > 0;
    }

    // If you have more than 0 energy you can use 1
    public void UseEnergy() 
    {
        if (_energy > 0)
        {
            Debug.Log("Can Use Energy");
            _energy -= 1;
            if(_nextEnergyTime == DateTime.MinValue)
            {
                StartRestorationTime(); 
            }
        }
        else
        {
            Debug.Log("Can't Use Energy");
        }
    }

    public void AddEnergy()
    {
        _energy += 1;
        if (_energy >= _maxEnergy)
        {
            _energy = _maxEnergy;
            _nextEnergyTime = DateTime.MinValue;
        }
    }

    public void ForceAddEnergy()
    {
        _energy += 1;
        if (_energy >= _maxEnergy)
        {
            _nextEnergyTime = DateTime.MinValue;
        }
    }

    // Start the _nextEnergyTime with Date.Now value + the restoration duration
    void StartRestorationTime()
    {
        _nextEnergyTime = _currentTime;
        _nextEnergyTime = _nextEnergyTime.AddSeconds(_restoreDuration);
    }
    // Restart the _nextEnergyTime with the restoration duration
    void RestartRestorationTime()
    {
        _nextEnergyTime = _nextEnergyTime.AddSeconds(_restoreDuration);
    }


}
