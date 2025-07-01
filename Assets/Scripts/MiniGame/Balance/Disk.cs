using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Disk : MonoBehaviour
{
    public string colorTag; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(colorTag + "Box"))
        {
            ScoreManager.instance.AddPoint();
            Destroy(gameObject);
        }
        else if (other.CompareTag("RedBox") || other.CompareTag("BlueBox"))
        {
            ScoreManager.instance.LosePoint();
            Destroy(gameObject);
        }
        else if (other.CompareTag("delete"))
        {
            Destroy(gameObject);
        }
    }
}
