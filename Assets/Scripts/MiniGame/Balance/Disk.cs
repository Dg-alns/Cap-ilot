using UnityEngine;

public class Disk : MonoBehaviour
{
    public string colorTag; // "Red" ou "Blue"
    public string alimentTag; // ex : "Pomme", "Poulet" ou "" pour potion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RedBox"))
        {
            bool correct = (colorTag == "Red");
            if (correct == true)
                GameController.instance.AddDiskToBox("Red");
            GameController.instance.AddDiskToScore(colorTag, alimentTag, correct);
            Destroy(gameObject);
        }
        else if (other.CompareTag("BlueBox"))
        {
            bool correct = (colorTag == "Blue");
            if (correct == true)
                GameController.instance.AddDiskToBox("Blue");
            GameController.instance.AddDiskToScore(colorTag, alimentTag, correct);
            Destroy(gameObject);
        }
        else if (other.CompareTag("delete"))
        {
            Destroy(gameObject);
        }
    }
}
