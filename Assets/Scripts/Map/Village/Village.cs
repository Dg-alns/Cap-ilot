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
        if(QuestManager.GetPlayerPref() > QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
        {
            diabete.SetActive(true);
        }

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
    }
}
