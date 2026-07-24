using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetectionObjCachee : Minigame
{
    public Timer timer;
    public Score score;
    public Sauvegarde_Minigame minigame;

    [SerializeField] Dictionary<Objects, TextMeshProUGUI> objects = new();
    [SerializeField] Transform _objNamesParent;
    [SerializeField] GameObject _textPrefab;
    int _objCount;
    
    public Infos_MiniJeux infos;

    public GameObject diabete;

    private Tools _tools;

    private void Awake()
    {
        List<Objects> objs = Tools.CreateList<Objects>("ToFind");

        foreach (var obj in objs)
        {
            obj.OnClick = FoundObject;
            GameObject textObj = Instantiate(_textPrefab, _objNamesParent);;
            TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();
            text.text = obj.name;
            objects.Add(obj, text);
        }
        _objCount = objects.Count;

        _tools = FindAnyObjectByType<Tools>();

        Debug.Log("Tools trouv� : " + _tools);
        Debug.Log("Nombre d'objets : " + objects.Count);
        Debug.Log("Nombre de noms : " + objects.Count);

        timer.stop = true;
    }

    void FoundObject(Objects obj)
    {
        objects[obj].fontStyle = FontStyles.Strikethrough;
        obj.PlayAnimation();
        // obj.gameObject.SetActive(false);
        if (minigame.GetCanShowInfo(SceneManager.GetActiveScene().name) == true)
        {
            timer.stop = true;
            infos.AssociateInfo(obj);
            infos.gameObject.SetActive(true);
        }
        _objCount--;
    }

    private void Update()
    {
        if (_objCount == 0)
            if (infos != null && infos.gameObject.activeSelf == false)
                score.LauchScore();
    }
    public override void PauseMinigame()
    {
        timer.stop = true;
    }

    public override void ResumeMinigame()
    {
        timer.stop = false;
    }

}
