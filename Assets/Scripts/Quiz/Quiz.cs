using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private ScriptableQuestion _questionScriptable;
    [SerializeField] private GameObject _questionUI;
    [SerializeField] private GameObject _canva;

    private List<string> _answers;
    void Start()
    {
        GameObject uiQuiz = Instantiate(_questionUI,_canva.transform);
        uiQuiz.GetComponentInChildren<TextMeshProUGUI>().text = _questionScriptable.question;

        _answers = _questionScriptable.wrongAnswer;
        _answers.Add(_questionScriptable.correctAnswer);
        for (int i=1;i< uiQuiz.GetComponentsInChildren<TextMeshProUGUI>().Length; i++)
        {
            int q = Random.Range(0, _answers.Count);
            uiQuiz.GetComponentsInChildren<TextMeshProUGUI>()[i].text = _answers[q];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
