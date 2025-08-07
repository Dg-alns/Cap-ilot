using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionWheel : MonoBehaviour
{
    [SerializeField] List<Image> imageList = new List<Image>();

    List<Emotion> emotionList;
    List<Color> colorList;
    public string ActualEmoji = "";
    public void Awake()
    {
        emotionList = new List<Emotion>();
        colorList = new List<Color>();  
        for (int i = 0; i < imageList.Count; i++)
        {
            colorList.Add(imageList[i].color);
            emotionList.Add(imageList[i].GetComponent<Emotion>());
        }
    }
    public void OnClick(int numButton)
    {
        string newEmote = emotionList[numButton].emoji;
        if (ActualEmoji != newEmote)
        {
            ActualEmoji = newEmote;
            EmotionSelected(numButton);
            return;
        }
        ActualEmoji = "";
        ResetWheel();
    }

    private void ResetWheel()
    {
        for (int i = 0; i < imageList.Count;i++)
        {
            imageList[i].color = colorList[i];
        }
    }

    void EmotionSelected(int numButton)
    {
        for(int i = 0; i < imageList.Count; i++)
        {
            Color noSelectedColor = imageList[i].color;
            noSelectedColor.a = 0.5f;
            imageList[i].color = noSelectedColor;
        }
        imageList[numButton].color = colorList[numButton]; 
    }

    public int GetNumeroEmotion(string emotionName)
    {
        for( int i = 0;i < imageList.Count;i++)
        {
            if (emotionList[i].emoji == emotionName)
            {
                return i;
            }
        }
        return -1;
    }
}
