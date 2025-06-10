using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMemorie : MonoBehaviour
{
    public Memorie mMemorie;
    private TMP_Text mText;
    void Start()
    {
        mText = GetComponent<TMP_Text>();
        Debug.Log(mText.text);
    }

    // Update is called once per frame
    void Update()
    {
        mText.text = "Score : " + mMemorie.Score();
    }
}
