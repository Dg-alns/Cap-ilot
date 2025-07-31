using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public GameObject diabete;

    public GameObject Ecole;
    public GameObject Sport;
    public GameObject Alimentation;
    public GameObject Relation;


    void Start()
    {
        // TODO ajout des condition quete pour l'activation des pnj et diabete



        if (Sport.activeSelf)
        {
            Ecole.GetComponent<NPC>().SetPLayerPrefs(1);
        }
    }
}
