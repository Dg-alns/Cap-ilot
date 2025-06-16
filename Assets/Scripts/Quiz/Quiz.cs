using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private ScriptableQuestion _questionScriptable;
    [SerializeField] private GameObject _questionUI;

    private List<string> _answers = new List<string>();
    void Start()
    {
        GameObject uiQuiz = Instantiate(_questionUI);
        uiQuiz.GetComponentInChildren<TextMeshProUGUI>().text = _questionScriptable.question;

        _answers.Add(_questionScriptable.wrongAnswer[0]);
        _answers.Add(_questionScriptable.wrongAnswer[1]);
        _answers.Add(_questionScriptable.wrongAnswer[2]);
        _answers.Add(_questionScriptable.correctAnswer);

        for (int i=1;i< uiQuiz.GetComponentsInChildren<TextMeshProUGUI>().Length; i++)
        {
            int q = Random.Range(0, _answers.Count);
            uiQuiz.GetComponentsInChildren<TextMeshProUGUI>()[i].text = _answers[q];
            _answers.RemoveAt(q);
        }



        for (int i = 0; i < uiQuiz.GetComponentsInChildren<Button>().Length; i++)
        {
            SetButtonOnClickAnswer(uiQuiz.GetComponentsInChildren<Button>()[i]);
        }
    }

    void SetButtonOnClickAnswer(Button button)
    {
        button.onClick.AddListener(() => CheckAnswer(button));
    }

    public void CheckAnswer(Button answerChose)
    {
        if(answerChose.GetComponentInChildren<TextMeshProUGUI>().text == _questionScriptable.correctAnswer)
        {
            answerChose.GetComponentInChildren<Image>().color = Color.green;
        }
        else
        {

            answerChose.GetComponentInChildren<Image>().color = Color.red;
        }
    }
}
