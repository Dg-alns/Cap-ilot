using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{

    public List<ScriptableQuestion> allQuestions;
    [SerializeField] private GameObject _questionUI;
    //[SerializeField] private GameObject _canva;

    private List<string> _answers = new List<string>();

    public void CreateQuestion(ScriptableQuestion _questionScriptable)
    {
        GameObject uiQuiz = Instantiate(_questionUI);
        uiQuiz.GetComponentInChildren<TextMeshProUGUI>().text = _questionScriptable.question;

        _answers.Add(_questionScriptable.wrongAnswer[0]);
        _answers.Add(_questionScriptable.wrongAnswer[1]);
        _answers.Add(_questionScriptable.wrongAnswer[2]);
        _answers.Add(_questionScriptable.correctAnswer);

        for (int i = 1; i < uiQuiz.GetComponentsInChildren<TextMeshProUGUI>().Length; i++)
        {
            int q = Random.Range(0, _answers.Count);
            uiQuiz.GetComponentsInChildren<TextMeshProUGUI>()[i].text = _answers[q];
            _answers.RemoveAt(q);
        }



        for (int i = 0; i < uiQuiz.GetComponentsInChildren<Button>().Length; i++)
        {
            SetButtonOnClickAnswer(uiQuiz.GetComponentsInChildren<Button>()[i], _questionScriptable);
        }
    }

    void SetButtonOnClickAnswer(Button button, ScriptableQuestion _questionScriptable)
    {
        button.onClick.AddListener(() => CheckAnswer(button, _questionScriptable));
    }

    public void CheckAnswer(Button answerChose, ScriptableQuestion _questionScriptable)
    {
        if (answerChose.GetComponentInChildren<TextMeshProUGUI>().text == _questionScriptable.correctAnswer)
        {
            answerChose.GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {

            answerChose.GetComponentInChildren<Image>().color = Color.red;
        }
    }
}
