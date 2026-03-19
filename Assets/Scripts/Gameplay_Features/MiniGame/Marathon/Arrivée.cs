using Unity.VisualScripting;
using UnityEngine;

public class Arrivée : MonoBehaviour
{
    public GameObject FinaleLine;

    GameObject player;
    Vector3 finalDetsination;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public bool ToucheTheLine()
    {
        if (FinaleLine.GetComponent<BoxCollider2D>().bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds))
        {
            finalDetsination = GameObject.Find("PosFinal").transform.position;
            return true;
        }

        return false;
    }

    public Vector3 GetFinalPos()
    {
        return finalDetsination;
    }
}
