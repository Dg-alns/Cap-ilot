using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsePnj : MonoBehaviour
{

    public  int id;
    private List<ScriptableQuestion> _questionList = new List<ScriptableQuestion>();
    [SerializeField] private QuizManager _quizManager;

    // Start is called before the first frame update
    void Start()
    {
        if (_quizManager.allQuestions != null)
        {
            for (int i = 0; i < _quizManager.allQuestions.Count; i++) {

                if (_quizManager.allQuestions[i].pnjId == id && !_quizManager.allQuestions[i].isAnswered)
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
        
        for (int i = 0; i < _questionList.Count; i++)//don't replay if ask again
        {
            if (_questionList[i].isAnswered == true)
            {
                _questionList.RemoveAt(i);
            }

        }
        _quizManager.EndQuiz();
        if (_questionList.Count>0)
        {
            _quizManager.CreateQuestion(_questionList[0]); 
        }
            
        
        //_questionList.RemoveAt(0);
    }
}
