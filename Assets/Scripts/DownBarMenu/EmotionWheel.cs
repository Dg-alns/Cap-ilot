using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionWheel : MonoBehaviour
{
    [SerializeField] List<Image> imageList = new List<Image>();

    [SerializeField] TextMeshProUGUI visualEmote;

    List<Emotion> emotionList;
    List<Color> colorList;
    public string ActualEmoji = "";

    public void Awake()
    {
        // Fill the two list with their informations (Emotion / Color)
        emotionList = new List<Emotion>();
        colorList = new List<Color>();  
        for (int i = 0; i < imageList.Count; i++)
        {
            colorList.Add(imageList[i].color);
            emotionList.Add(imageList[i].GetComponent<Emotion>());
        }
    }

    // Select or Unselect an emotion (Associate to button) (numButton : value between 0 - 7 for each emotion)
    public void OnClick(int numButton)
    {
        string newEmote = emotionList[numButton].emoji;

        // If the player select another "emotion"
        if (ActualEmoji != newEmote)
        {
            ActualEmoji = newEmote;
            visualEmote.text = ActualEmoji;
            EmotionSelected(numButton);
            return;
        }

        // If the player select the same "emotion"
        ActualEmoji = "";
        visualEmote.text = ActualEmoji;
        ResetWheel();
    }

    private void ResetWheel()
    {
        for (int i = 0; i < imageList.Count;i++)
        {
            imageList[i].color = colorList[i];
        }
    }

    // Make alpha color in 0.5f for each no selected emotion
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

    // Get the associate number with the emotion 
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
