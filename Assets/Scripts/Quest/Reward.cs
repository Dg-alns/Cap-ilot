using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Reward
{
    public Sprite sprite;
    public string name;
    public GameObject GO;
    public Reward(Sprite sprite, string name)
    {
        this.sprite = sprite;
        this.name = name;
    }
    virtual public void Obtain(Saving Data)
    {
        GO = new GameObject();
        GO.name = "Reward Canva";
        GO.AddComponent<Canvas>();
        Canvas canva = GO.GetComponent<Canvas>();
        canva.renderMode = RenderMode.ScreenSpaceOverlay;
        GO.AddComponent<CanvasScaler>();
        GO.AddComponent<GraphicRaycaster>();

        GameObject myText = new GameObject();
        myText.transform.parent = GO.transform;
        myText.name = "reward name";

        TextMeshProUGUI text = myText.AddComponent<TextMeshProUGUI>();
        text.text = "You obtained " + name;
        text.fontSize = 25;
        text.alignment = TextAlignmentOptions.Center;

        // Text position
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 121, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
    }
}

public class ExampleReward : Reward
{
    public ExampleReward(Sprite sprite, string name) : base(sprite, name)
    {
    }

    public override void Obtain(Saving Data)
    {
        if (sprite == null)
            return;

        base.Obtain(Data);
        Data.profile.Username += " *";
    }
}