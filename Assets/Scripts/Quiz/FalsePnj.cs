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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AskQuestion()
    {
        
        for (int i = _questionList.Count-1; i >= 0; i--)
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

    //Permets de recommencer le quizz à chaque interaction avec le pnj à mettre dans le start si non souhaité
    public void FirstQuestion()
    {
        if (_quizManager.allQuestions != null)
        {
            _questionList.Clear();
            for (int i = 0; i < _quizManager.allQuestions.Count; i++)
            {
                _quizManager.allQuestions[i].isAnswered = false;
                if (_quizManager.allQuestions[i].pnjId == id )
                {
                    _questionList.Add(_quizManager.allQuestions[i]);
                }

            }
        }
        AskQuestion();
    }
}
