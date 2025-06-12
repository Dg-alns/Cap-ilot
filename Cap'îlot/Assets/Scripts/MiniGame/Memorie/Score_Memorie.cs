using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score_Memorie : MonoBehaviour
{
    public Minigame_Memorie mMemorie;
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
