using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Disk : MonoBehaviour
{
    public string colorTag; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(colorTag + "Box"))
        {
            ScoreManager.instance.AddPoint();
        }
        else if (other.CompareTag("RedBox") || other.CompareTag("BlueBox"))
        {
            ScoreManager.instance.LosePoint();
        }

        Destroy(gameObject);
    }
}
