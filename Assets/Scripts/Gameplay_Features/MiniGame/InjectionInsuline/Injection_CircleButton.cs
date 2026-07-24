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


    public TMP_FontAsset TMProFont;
    public Font TextMeshFont;
    private TMP_Text m_textMeshPro;

    private const string label01 = "Parfait!";
    private const string label02 = "Bien";
    private const string label03 = "Raté";

    private Material m_material01;
    private Material m_material02;

    private void Start()
    {
        _injectionMinigame = GetComponentInParent<InjectionMinigame>();
        m_textMeshPro = gameObject.AddComponent<TextMeshProUGUI>();


        if (TMProFont != null)
            m_textMeshPro.font = TMProFont;

        m_textMeshPro.fontSize = 48;
        m_textMeshPro.alignment = TextAlignmentOptions.Center;
        m_textMeshPro.extraPadding = true;

        m_material01 = m_textMeshPro.font.material;
        m_material02 = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - BEVEL");
    }

    public void ClickCircle()
    {
        float stopScale = _circle.GetComponent<RectTransform>().localScale.x;

        float average = Mathf.Abs(stopScale - _scaleTarget);


        if (average < 0.5f)
        {
            Debug.Log("Parfait : " + average);
            m_textMeshPro.color = Color.black;
            //m_textMeshPro.text = label01;

            // score : 700 -> 1000
            float sup = (1.0f-average / 0.5f) * 300;
            int score = (int) (sup + 1.0f) + 700;

            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            //Destroy(m_textMeshPro);
            return;
        }
        
        if (average <= 3.0f)
        {
            Debug.Log("Bien : " + average);
            //m_textMeshPro.color = Color.yellowGreen;
            //m_textMeshPro.text = label02;

            // score : 400 -> 699
            float sup = 1.0f - (average - 0.5f) / 2.5f * 299;
            int score = (int) (sup+ 1.0f) + 400;
            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            //Destroy(m_textMeshPro);
            return;
        }
        
        if (average > 3.0f)
        {
            Debug.Log("RATE : " + average);
            //m_textMeshPro.color = Color.red;
            //m_textMeshPro.text = label03;

            // score : 0 -> 150
            int score = (int)(1.0f - (average - 3f) / 10f * 150);
            _injectionMinigame.AddScore(score);

            Destroy(_parent);
            //Destroy(m_textMeshPro);
            return;
        }
        
    }
}
