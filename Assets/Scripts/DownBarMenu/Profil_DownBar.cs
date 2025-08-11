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
    [SerializeField] private TextMeshProUGUI _NbQuête;

    private void Start()
    {
        string jsonstring = File.ReadAllText("save.json");
        Saving save = JsonUtility.FromJson<Saving>(jsonstring);

        _NicknameText.text = save.profile.Username;

        _NbStars.text = sauvegarde_Minigame.GetTotalStars().ToString();

        _NbJournal.text = save.GetCompletedJournal().ToString();
    }
}
