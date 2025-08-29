using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Profil_DownBar : MonoBehaviour
{
    [SerializeField] private Sauvegarde_Minigame sauvegarde_Minigame;

    [Header("Character Values")]
    [SerializeField] private TextMeshProUGUI _NicknameText;
    // Miss the characters skin 
    

    [Header("Statistique Values")]
    [SerializeField] private TextMeshProUGUI _NbStars;
    [SerializeField] private TextMeshProUGUI _NbHours;
    [SerializeField] private TextMeshProUGUI _NbJournal;
    [SerializeField] private TextMeshProUGUI _NbQuest;
    [SerializeField] private TextMeshProUGUI _CurrentQuest;

    private void Start()
    {
        
        Saving save = JSON_Manager.LoadData("Save");

        _NicknameText.text = save.profile.Username;

        _NbStars.text = sauvegarde_Minigame.GetTotalStars().ToString();

        _NbJournal.text = save.GetCompletedJournal().ToString();

        int numQuest = QuestManager.GetCurrentQuest();
        int totalQuest = (int)QUESTS.Count;

        _NbQuest.text = numQuest < 0 ? "0/" + totalQuest.ToString() : numQuest.ToString() + "/" + totalQuest.ToString();

        _CurrentQuest.text = QuestManager.GetTextOffCurrentQuest();
    }
}
