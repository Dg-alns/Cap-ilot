using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        if (Diabete != null)
        {
            if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
                Diabete.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name == "Personalisation")
        {
            if (HaveDateEnter() == false)
                return;

            Corps.GetComponent<SpriteRenderer>().sprite = playerData.Corps;
            Cheveux.GetComponent<SpriteRenderer>().sprite = playerData.Cheveux;
            AccessoirTete.GetComponent<SpriteRenderer>().sprite = playerData.AccessoirTete;
            Haut.GetComponent<SpriteRenderer>().sprite = playerData.Haut;
            Bas.GetComponent<SpriteRenderer>().sprite = playerData.Bas;
            Chaussure.GetComponent<SpriteRenderer>().sprite = playerData.Chaussure;

            Corps.GetComponent<SpriteRenderer>().color = playerData.Color_Corps;
            Cheveux.GetComponent<SpriteRenderer>().color = playerData.Color_Cheveux;
            AccessoirTete.GetComponent<SpriteRenderer>().color = playerData.Color_AccessoirTete;
            Haut.GetComponent<SpriteRenderer>().color = playerData.Color_Haut;
            Bas.GetComponent<SpriteRenderer>().color = playerData.Color_Bas;
            Chaussure.GetComponent<SpriteRenderer>().color = playerData.Color_Chaussure;
        }
    }

    private void Update()
    {
        if (Diabete == null)
            return;

        if (Diabete.activeSelf)
            return;

        if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
            Diabete.SetActive(true);

    }

    public void SavePersonalisation()
    {
        playerData.Corps = Corps.GetComponent<SpriteRenderer>().sprite;
        playerData.Cheveux = Cheveux.GetComponent<SpriteRenderer>().sprite;
        playerData.AccessoirTete = AccessoirTete.GetComponent<SpriteRenderer>().sprite;
        playerData.Haut = Haut.GetComponent<SpriteRenderer>().sprite;
        playerData.Bas = Bas.GetComponent<SpriteRenderer>().sprite;
        playerData.Chaussure = Chaussure.GetComponent<SpriteRenderer>().sprite;

        playerData.Color_Corps = Corps.GetComponent<SpriteRenderer>().color;
        playerData.Color_Cheveux = Cheveux.GetComponent<SpriteRenderer>().color;
        playerData.Color_AccessoirTete = AccessoirTete.GetComponent<SpriteRenderer>().color;
        playerData.Color_Haut = Haut.GetComponent<SpriteRenderer>().color;
        playerData.Color_Bas = Bas.GetComponent<SpriteRenderer>().color;
        playerData.Color_Chaussure = Chaussure.GetComponent<SpriteRenderer>().color;
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
