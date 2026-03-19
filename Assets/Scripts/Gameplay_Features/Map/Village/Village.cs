using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour 
{
    public GameObject Ecole;
    public GameObject Sport;
    public GameObject Alimentation;
    public GameObject Relation;

    public GameObject MaisonDiabete;

    public SpriteRenderer MaisonAlimentation;
    public SpriteRenderer MaisonRelation;
    public SpriteRenderer MaisonFrere;

    public Sprite MaisonCleanAlimentation;
    public Sprite MaisonCleanRelation;
    public Sprite MaisonCleanFrere;


    void Start()
    {
        if(QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(QUESTS.A_Ressource))
        {
            Alimentation.SetActive(true);
            MaisonAlimentation.sprite = MaisonCleanAlimentation;
        }
        if(QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(QUESTS.S_Ressource))
        {
            Sport.SetActive(true);
        }
        if(QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(QUESTS.E_Ressource))
        {
            Ecole.SetActive(true);
            MaisonFrere.sprite = MaisonCleanFrere;
        }
        if(QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(QUESTS.R_Ressoucre))
        {
            Relation.SetActive(true);
            MaisonRelation.sprite = MaisonCleanRelation;
        }


        if (Sport.activeSelf)
        {
            Ecole.GetComponent<NPC>().SetPLayerPrefs(1);
        }
    }

    private void Update()
    {
        if (QuestManager.GetCurrentQuest() == QuestManager.GetQUESTS(QUESTS.DemandeCapitaine))
        {
            Debug.Log("Confrontation");
            MaisonDiabete.GetComponent<Collider2D>().enabled = false;
            string text = "Confronter le Capitain au sujet du monstre.";
            QuestManager.SetTextOffCurrentQuest(text);
        }
    }
}
