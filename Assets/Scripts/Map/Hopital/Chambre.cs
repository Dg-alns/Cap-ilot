using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chambre : MonoBehaviour
{
    public GameObject Couloir;

    static string NamePlayerPref = "StateChambre";

    void Start()
    {
        if (PlayerPrefs.HasKey(NamePlayerPref) == false)
            PlayerPrefs.SetInt(NamePlayerPref, 0);


        if(PlayerPrefs.GetInt(NamePlayerPref) == 1)
            Couloir.SetActive(true);
    }


    public static void SetStateChambre(int value)
    {
        PlayerPrefs.SetInt(NamePlayerPref, value);
    }
}
