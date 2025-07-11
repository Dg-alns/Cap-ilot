using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accueil_LoopBG : MonoBehaviour
{
    List<Accueil_BGMove> images;

    int activeIndex;

    float showTime = 12.0f;
    float time = 0.0f;

    [SerializeField] private Transform _waveTransition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Screen.height);
        // Initiate Value
        images = new List<Accueil_BGMove>();

        // Fill the images with children
        for (int i = 0; i < transform.childCount; i++)
        {
            images.Add(transform.GetChild(i).GetComponent<Accueil_BGMove>());
        }

        activeIndex = 0;
        images[activeIndex].RestartMove();
        StartCoroutine(DisableFirstWave());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time < showTime - 2)
            return;

        SwitchActiveIndex();
        images[activeIndex].RestartMove();
        time = 0.0f;    
    }

    void SwitchActiveIndex()
    {
        if (activeIndex + 1 == images.Count)
            activeIndex = 0;
        else
            activeIndex++;
    }

    IEnumerator DisableFirstWave()
    {
        for(int i = 0; i < _waveTransition.childCount; i++)
        {
            Color color = _waveTransition.GetChild(i).GetComponent<Image>().color;
            color.a = 0;
            _waveTransition.GetChild(i).GetComponent<Image>().color = color;
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < _waveTransition.childCount; i++)
        {
            Color color = _waveTransition.GetChild(i).GetComponent<Image>().color;
            color.a = 1;
            _waveTransition.GetChild(i).GetComponent<Image>().color = color;
        }
    }
}
