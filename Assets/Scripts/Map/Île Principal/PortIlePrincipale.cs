using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortIlePrincipale : MonoBehaviour
{
    public GameObject PancarteVillage;
    public GameObject PancartePhare;
    public GameObject Bateau;

    public Port Hopital;
    public Port Alimentation;

    public void ActivePancartePhare()
    {
        PancartePhare.SetActive(true);
    }

    public void ActivePhare()
    {
        PancartePhare.GetComponent<BoxCollider2D>().isTrigger = true;

        if(PancartePhare.activeSelf == false)
            PancartePhare.SetActive(true);
    }

    public void ActiveVillage()
    {
        PancarteVillage.SetActive(true);
    }

    public void ActiveBateau()
    {
        Bateau.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void Start()
    {
        if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.ReparationPhare))
        {
            ActivePhare();
        }
        if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.Maison))
        {
            ActiveVillage();
        }

        if (QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.Hopital))
        {
            ActiveBateau();
        }


        if (QuestManager.GetPlayerPref() == QuestManager.GetQUESTS(QUESTS.Hopital))
        {
            Hopital.CanGotoIle();
        }
        if (QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.Phare))
        {
            Alimentation.CanGotoIle();
        }
    }
}
