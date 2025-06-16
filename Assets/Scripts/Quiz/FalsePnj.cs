using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsePnj : MonoBehaviour
{

    private int id;
    private List<ScriptableQuestion> _questionList = new List<ScriptableQuestion>();
    [SerializeField] private QuizManager _quizManager;

    // Start is called before the first frame update
    void Start()
    {
        if (_quizManager.allQuestions != null)
        {
            for (int i = 0; i < _quizManager.allQuestions.Count; i++) {

                if (_quizManager.allQuestions[i].pnjId == id)
                {
                    _questionList.Add(_quizManager.allQuestions[i]);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AskQuestion()
    {
        _quizManager.CreateQuestion(_questionList[0]);
    }
}
