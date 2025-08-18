using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour //TODO Diego ajouter les maison endomager
{
    public GameObject Ecole;
    public GameObject Sport;
    public GameObject Alimentation;
    public GameObject Relation;

    public GameObject MaisonDiabete;


    void Start()
    {
        if(QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.A_Ressource))
        {
            Alimentation.SetActive(true);
        }
        if(QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.S_Ressource))
        {
            Sport.SetActive(true);
        }
        if(QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.E_Ressource))
        {
            Ecole.SetActive(true);
        }
        if(QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.R_Ressoucre))
        {
            Relation.SetActive(true);
        }


        if (Sport.activeSelf)
        {
            Ecole.GetComponent<NPC>().SetPLayerPrefs(1);
        }

        if(QuestManager.GetPlayerPref() >= QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
        {
            MaisonDiabete.GetComponent<Collider2D>().enabled = false;
        }
    }
}
