using Unity.VisualScripting;
using UnityEngine;

public class Ravitalement : MonoBehaviour
{
    GameObject player;
    GestionEndurance endurance;

    bool haveRavito = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        endurance = GameObject.Find("Endurance").GetComponent<GestionEndurance>();
    }

    void OnMouseDown()
    {
        if (haveRavito)
            return;

        if (GetComponent<BoxCollider2D>().bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds))
        {
            haveRavito = true;
            endurance.Ravitallement();
        }
    }
}
