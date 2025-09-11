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
    public Port Relation;
    public Port Sport;
    public Port Tentation;
    public Port Ecole;

    public void ActivePancartePhare()
    {
        PancartePhare.SetActive(true);
    }

    public void ActivePhare()
    {
        PancartePhare.GetComponent<BoxCollider2D>().isTrigger = true;

        if(PancartePhare.activeSelf == false)
            PancartePhare.SetActive(true);


        if (QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.Introduction))
        {
            string text = "Retrouver le Capitaine au Phare.";
            QuestManager.SetTextOffCurrentQuest(text);
        }
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
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.ReparationPhare))
        {
            ActivePhare();
        }
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Maison))
        {
            ActiveVillage();
        }

        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Hopital))
        {
            Hopital.CanGotoIle();
            ActiveBateau();

        }
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Alimentation))
        {
            Alimentation.CanGotoIle();
            string text = "Naviguer vers la prochaine île.";
            QuestManager.SetTextOffCurrentQuest(text);
        }
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Relation))
        {
            Relation.CanGotoIle();
            //string text = "Naviguer vers la prochaine île.";

            //QuestManager.SetTextOffCurrentQuest(text);
        }
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Sport))
        {
            Sport.CanGotoIle();
            //string text = "Naviguer vers la prochaine île.";

            //QuestManager.SetTextOffCurrentQuest(text);
        }
        //if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Tentation))
        //{
        //    Tentation.CanGotoIle();
            //string text = "Naviguer vers la prochaine île.";

            //QuestManager.SetTextOffCurrentQuest(text);
        //}
        if (QuestManager.GetCurrentQuest() >= QuestManager.GetQUESTS(QUESTS.Ecole))
        {
            Ecole.CanGotoIle();
            //string text = "Naviguer vers la prochaine île.";

            //QuestManager.SetTextOffCurrentQuest(text);
        }
    }
}
