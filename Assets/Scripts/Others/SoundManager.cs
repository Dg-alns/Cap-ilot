using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider _generalSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Master_Volume"))
        {
            _generalSlider.value = PlayerPrefs.GetFloat("Master_Volume");
            ChangeGeneralVolume();
        }

        if (PlayerPrefs.HasKey("Music_Volume"))
        {
            _musicSlider.value = PlayerPrefs.GetFloat("Music_Volume");
            ChangeMusicVolume();
        }
        if (PlayerPrefs.HasKey("SFX_Volume"))
        {
            _soundSlider.value = PlayerPrefs.GetFloat("SFX_Volume");
            ChangeSFXVolume();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeGeneralVolume()
    {
        if (_generalSlider.value == 0)
        {
            _audioMixer.SetFloat("Master_Volume", -80);
        }
        else
        {
            _audioMixer.SetFloat("Master_Volume", Mathf.Log10(_generalSlider.value) * 20);
        }

        PlayerPrefs.SetFloat("Master_Volume", _generalSlider.value);
    }
    public void ChangeMusicVolume()
    {
        if (_musicSlider.value == 0)
        {
            _audioMixer.SetFloat("Music_Volume", -80);
        }
        else
        {
            _audioMixer.SetFloat("Music_Volume", Mathf.Log10(_musicSlider.value) * 20);
        }
        PlayerPrefs.SetFloat("Music_Volume", _musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        if (_soundSlider.value == 0)
        {
            _audioMixer.SetFloat("SFX_Volume", -80);
        }
        else
        {
            _audioMixer.SetFloat("SFX_Volume", Mathf.Log10(_soundSlider.value) * 20);
        }
        PlayerPrefs.SetFloat("SFX_Volume", _soundSlider.value);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
