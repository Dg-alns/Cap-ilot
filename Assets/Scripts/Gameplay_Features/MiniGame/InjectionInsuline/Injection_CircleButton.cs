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

    public Font TextMeshFont;
    private TextMesh m_textMesh;

    private const string label01 = "Parfait!";
    private const string label02 = "Bien";
    private const string label03 = "Raté";

    private void Start()
    {
        _injectionMinigame = GetComponentInParent<InjectionMinigame>();
        m_textMesh = gameObject.AddComponent<TextMesh>();

        if (TextMeshFont != null)
        {
            m_textMesh.font = TextMeshFont;
            m_textMesh.GetComponent<Renderer>().sharedMaterial = m_textMesh.font.material;
        }
        else
        {
            m_textMesh.font = Resources.Load("Fonts/ARIAL", typeof(Font)) as Font;
            m_textMesh.GetComponent<Renderer>().sharedMaterial = m_textMesh.font.material;
        }

        m_textMesh.fontSize = 48;
        m_textMesh.anchor = TextAnchor.MiddleCenter;
    }

    public void ClickCircle()
    {
        float stopScale = _circle.GetComponent<RectTransform>().localScale.x;

        float average = Mathf.Abs(stopScale - _scaleTarget);


        if (average < 0.5f)
        {
            Debug.Log("Parfait : " + average);
            m_textMesh.color = Color.green;
            m_textMesh.text = label01;

            // score : 700 -> 1000
            float sup = (1.0f-average / 0.5f) * 300;
            int score = (int) (sup + 1.0f) + 700;

            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            Destroy(m_textMesh);
            return;
        }
        
        if (average <= 3.0f)
        {
            Debug.Log("Bien : " + average);
            m_textMesh.color = Color.yellowGreen;
            m_textMesh.text = label02;

            // score : 400 -> 699
            float sup = 1.0f - (average - 0.5f) / 2.5f * 299;
            int score = (int) (sup+ 1.0f) + 400;
            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            Destroy(m_textMesh);
            return;
        }
        
        if (average > 3.0f)
        {
            Debug.Log("RATE : " + average);
            m_textMesh.color = Color.red;
            m_textMesh.text = label03;

            // score : 0 -> 150
            int score = (int)(1.0f - (average - 3f) / 10f * 150);
            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            Destroy(m_textMesh);
            return;
        }
        
    }
}
