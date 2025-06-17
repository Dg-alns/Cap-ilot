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
    [SerializeField] private GameObject _panelNextQuestion;
    //[SerializeField] private GameObject _canva;

    private List<string> _answers = new List<string>();

    private GameObject _uiQuiz;

    public void CreateQuestion(ScriptableQuestion _questionScriptable)
    {
        _uiQuiz = Instantiate(_questionUI);
        _uiQuiz.GetComponentInChildren<TextMeshProUGUI>().text = _questionScriptable.question;

        _answers.Add(_questionScriptable.wrongAnswer[0]);
        _answers.Add(_questionScriptable.wrongAnswer[1]);
        _answers.Add(_questionScriptable.wrongAnswer[2]);
        _answers.Add(_questionScriptable.correctAnswer);

        for (int i = 1; i < _uiQuiz.GetComponentsInChildren<TextMeshProUGUI>().Length; i++)
        {
            int q = Random.Range(0, _answers.Count);
            _uiQuiz.GetComponentsInChildren<TextMeshProUGUI>()[i].text = _answers[q];
            _answers.RemoveAt(q);
        }



        for (int i = 0; i < _uiQuiz.GetComponentsInChildren<Button>().Length; i++)
        {
            SetButtonOnClickAnswer(_uiQuiz.GetComponentsInChildren<Button>()[i], _questionScriptable);
        }

        _panelNextQuestion.SetActive(false);
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
            _questionScriptable.isAnswered =true;
            _uiQuiz.GetComponentInChildren<TextMeshProUGUI>().text = _questionScriptable.afterCorrect;
            
        }
        else
        {

            answerChose.GetComponentInChildren<Image>().color = Color.red;
            _uiQuiz.GetComponentInChildren<TextMeshProUGUI>().text = _questionScriptable.afterWrong;
        }

        for (int i = 0; i < _uiQuiz.GetComponentsInChildren<Button>().Length; i++)
        {
            _uiQuiz.GetComponentsInChildren<Button>()[i].onClick.RemoveAllListeners();
        }
        /*if (_uiQuiz)
        {
            Destroy(_uiQuiz,1);
        }*/
        //Destroy(_uiQuiz,5.0f);
        _panelNextQuestion.SetActive(true);
        _panelNextQuestion.transform.SetAsLastSibling();
    }

    public void EndQuiz() {
        Debug.Log("cc");
        if(_uiQuiz)
        {
            Destroy(_uiQuiz);
        }
    }
}
