using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour
{
    [SerializeField] Button Joy;
    [SerializeField] Button Trust;
    [SerializeField] Button Scared;
    [SerializeField] Button Surprise;
    [SerializeField] Button Sadness;
    [SerializeField] Button Boring;
    [SerializeField] Button Anger;
    [SerializeField] Button Waiting;
    public string ActualEmoji;
    public void Awake()
    {
        Joy.onClick.AddListener(() => { OnClick(Joy); });
        Trust.onClick.AddListener(() => { OnClick(Trust); });
        Scared.onClick.AddListener(() => { OnClick(Scared); });
        Surprise.onClick.AddListener(() => { OnClick(Surprise); });
        Sadness.onClick.AddListener(() => { OnClick(Sadness); });
        Boring.onClick.AddListener(() => { OnClick(Boring); });
        Anger.onClick.AddListener(() => { OnClick(Anger); });
        Waiting.onClick.AddListener(() => { OnClick(Waiting); });
    }
    public void OnClick(Button pressed)
    {
        ActualEmoji = pressed.GetComponent<Emotion>().emoji;
    }
}
