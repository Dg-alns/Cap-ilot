using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    public PlayerData playerData;

    public GameObject Corps;
    public GameObject Cheveux;
    public GameObject AccessoirTete;
    public GameObject Haut;
    public GameObject Bas;
    public GameObject Chaussure;

    public GameObject Diabete;

    bool discoverDiabete = false;

    private void Start()
    {
        if (Diabete != null)
        {
            if (discoverDiabete)
                Diabete.SetActive(true);
        }

        if (HaveDateEnter() == false)
            return;


        Corps.GetComponent<SpriteRenderer>().sprite = playerData.Corps;
        Cheveux.GetComponent<SpriteRenderer>().sprite = playerData.Cheveux;
        AccessoirTete.GetComponent<SpriteRenderer>().sprite = playerData.AccessoirTete;
        Haut.GetComponent<SpriteRenderer>().sprite = playerData.Haut;
        Bas.GetComponent<SpriteRenderer>().sprite = playerData.Bas;
        Chaussure.GetComponent<SpriteRenderer>().sprite = playerData.Chaussure;
    }

    public void SavePersonalisation()
    {
        playerData.Corps = Corps.GetComponent<SpriteRenderer>().sprite;
        playerData.Cheveux = Cheveux.GetComponent<SpriteRenderer>().sprite;
        playerData.AccessoirTete = AccessoirTete.GetComponent<SpriteRenderer>().sprite;
        playerData.Haut = Haut.GetComponent<SpriteRenderer>().sprite;
        playerData.Bas = Bas.GetComponent<SpriteRenderer>().sprite;
        playerData.Chaussure = Chaussure.GetComponent<SpriteRenderer>().sprite;
    }

    bool HaveDateEnter()
    {
        return playerData.Corps != null ||
                playerData.Cheveux != null ||
                playerData.AccessoirTete != null ||
                playerData.Haut != null ||
                playerData.Bas != null ||
                playerData.Chaussure != null;
    }
}
