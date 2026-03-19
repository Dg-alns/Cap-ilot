using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizNPC : NPC
{
    private List<ScriptableQuestion> _questionList = new List<ScriptableQuestion>();
    [SerializeField] private QuizManager _quizManager;
    [SerializeField] private Button _nextQuestion;
    public int idxQuizSet;



    public void AskQuestion()
    {

        for (int i = _questionList.Count - 1; i >= 0; i--)
        {
            if (_questionList[i].isAnswered == true)
            {
                _questionList.RemoveAt(i);
            }

        }
        _quizManager.EndQuiz();
        if (_questionList.Count > 0)
        {
            _quizManager.npcName = npcName;
            _quizManager.CreateQuestion(_questionList[0]);
        }
        else
        {
            QuestManager.ValidateQuest(QUESTS.Ecole);
            idxOffSetDialogue++;
            FindAnyObjectByType<Movement>().dialogueNpc = gameObject;
            GetComponent<Trigger>().IsTrigger();
            FindAnyObjectByType<Movement>().activeDialogueUI = GetComponent<Trigger>().activeUI;
        }


        //_questionList.RemoveAt(0);
    }

    //Permets de recommencer le quizz à chaque interaction avec le pnj à mettre dans le start si non souhaité
    public void FirstQuestion()
    {
        _nextQuestion.onClick.RemoveAllListeners();
        _nextQuestion.onClick.AddListener(AskQuestion);
        if (_quizManager.allQuestions != null)
        {
            _questionList.Clear();
            for (int i = 0; i < _quizManager.allQuestions.Count; i++)
            {
                _quizManager.allQuestions[i].isAnswered = false;
                if (_quizManager.allQuestions[i].pnjId == npcId)
                {
                    _questionList.Add(_quizManager.allQuestions[i]);
                }

            }
        }
        AskQuestion();
    }
}
