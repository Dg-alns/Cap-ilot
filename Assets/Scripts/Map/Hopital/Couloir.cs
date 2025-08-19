using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couloir : MonoBehaviour
{
    public GameObject Port;

    static string NamePlayerPref = "StateCouloir";

    void Start()
    {
        if (PlayerPrefs.HasKey(NamePlayerPref) == false)
            PlayerPrefs.SetInt(NamePlayerPref, 0);


        if (PlayerPrefs.GetInt(NamePlayerPref) == 1 || QuestManager.GetCurrentQuest() > QuestManager.GetQUESTS(QUESTS.Hopital))
        {
            Port.SetActive(true);
        }
    }


    public static void SetStateCouloir(int value)
    {
        PlayerPrefs.SetInt(NamePlayerPref, value);
    }

    public static int GetStateCouloir() { return PlayerPrefs.GetInt(NamePlayerPref); }
}
