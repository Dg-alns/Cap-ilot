using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ecole_Map : MonoBehaviour
{
    [SerializeField] private GameObject Panneau_Etage2;


    // Start is called before the first frame update
    void Awake()
    {
        Panneau_Etage2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivePanneau()
    {
        Panneau_Etage2.SetActive(true);
    }
}
