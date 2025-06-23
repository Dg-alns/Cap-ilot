using Unity.VisualScripting;
using UnityEngine;

public class Ravitalement : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnMouseDown()
    {
        
        if (GetComponent<CircleCollider2D>().bounds.Intersects(player.GetComponent<CircleCollider2D>().bounds))
            Debug.Log("Ravit");
    }
}
