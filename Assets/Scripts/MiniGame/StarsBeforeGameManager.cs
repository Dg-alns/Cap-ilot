using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarsBeforeGameManager : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI TextScore;

    public void InitText(string text) { TextScore.text = text; }

    public void IsDefeatScore(Color color)
    {
        image.color = color;
        TextScore.fontStyle = FontStyles.Strikethrough;
    }
}
